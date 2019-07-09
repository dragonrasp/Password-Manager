using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;
using static PassManager.TranslationTools;


namespace PassManager
{
    public partial class Form1 : Form
    {

        public class Palette
        {
            public Color First_Color;
            public Color Second_Color;
            public Color Third_Color;
            public string Name;
            public Palette(Color c1, Color c2, Color c3, string n)
            {
                First_Color = c1;
                Second_Color = c2;
                Third_Color = c3;
                Name = n;
            }
        }

        public Form1()
        {
            InitializeComponent();
            DataChanged += SetUnsavedFlag;
            DataChanged += ShowSaveButton;

            SaveTableButton.BringToFront();
            CopyButton.BringToFront();

            FormTranslationDictionary = ReadTranslationFile("RU.json");
            ApplyTranslation(this, FormTranslationDictionary[this.GetType().Name]);
            programname = Text;

            StreamReader SR = new StreamReader(DICTIONARY_FILE_NAME);
            string tmp = SR.ReadToEnd();
            SR.Close();
            GeneratorDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(tmp);
        }

        const string DICTIONARY_FILE_NAME = "dictionary.json";
        Dictionary<string, string> GeneratorDictionary = new Dictionary<string, string>();
        Dictionary<string, Dictionary<string, TranslationPair>> FormTranslationDictionary;

        string programname;
        string basename;
        byte[] key;
        byte[] currentiv;
        const int HASH_SIZE = 32;
        const int ITERATIONS = 1000;
        delegate void data_changed();
        event data_changed DataChanged;

        int saveanimationtime = 0;
        
        int bufferanimationdirection = 10;
        int animationdirection = 10;
        const int startcolor = 50;
        const int maxcolor = 350;
        int bufferanimationtime = startcolor;

        bool AscSort = true;

        bool HasUnsavedData = false;

        void SetUnsavedFlag()
        {
            HasUnsavedData = true;
        }

        void ShowSaveButton()
        {
            SaveTableButton.Visible = true;
        }


        void GenerateIV()
        {
            using (Aes aes = Aes.Create())
            {
                currentiv = aes.IV;
            }
        }

        List<string> CreateNameList()
        {
            List<string> tmp = new List<string>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                tmp.Add(dataGridView1[0, i].Value.ToString());
            }
            return tmp;
        }

        byte[] GenerateSalt(string s)
        {
            
            byte[] tmp = Encoding.Unicode.GetBytes(s);
            Array.Resize(ref tmp, 8);
            return tmp;
        }

        byte[] ConvertToKey(string pass)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(pass, GenerateSalt(pass), ITERATIONS);
            byte[] res = pbkdf2.GetBytes(HASH_SIZE);
            return res;
        }

        string GetStringFromByte(byte[] b)
        {
            return Encoding.Unicode.GetString(b);
        }

        byte[] GetBytesFromString(string s)
        {
            return Encoding.Unicode.GetBytes(s);
        }

        byte[] EncryptString(string s, byte[] k, byte[] IV)
        {
            byte[] enc;
            
            using (Aes aes = Aes.Create())
            {
                aes.Padding = PaddingMode.ISO10126;
                ICryptoTransform ICT = aes.CreateEncryptor(k, IV);
                
                using (MemoryStream MS = new MemoryStream())
                {
                    using (CryptoStream CS = new CryptoStream(MS, ICT, CryptoStreamMode.Write))
                    {
                        using (StreamWriter SW = new StreamWriter(CS))
                        {
                            SW.Write(s);
                        }
                    }
                    enc = MS.ToArray();
                }
            }
            
            return enc;
        }

        string DecryptString(byte[] enc, byte[] k, byte[] IV)
        {
            byte[] encstring = enc;
            string res = null;
            using (Aes aes = Aes.Create())
            {
                aes.Padding = PaddingMode.ISO10126;
                ICryptoTransform ICT = aes.CreateDecryptor(k, IV);
                using (MemoryStream MS = new MemoryStream(encstring))
                {
                    using (CryptoStream CS = new CryptoStream(MS, ICT, CryptoStreamMode.Read))
                    {
                        using (StreamReader SR = new StreamReader(CS))
                        {
                            
                            res = SR.ReadToEnd();
                        }
                    }
                }
            }
            return res;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            
                saveFileDialog1.Filter = "Encrypted Password Manager Files (*.EPM)|*.EPM|" + "All files (*.*)|*.*";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                using (PasswordForm PF = new PasswordForm(FormTranslationDictionary["PasswordForm"]))
                {
                    if (PF.ShowDialog() == DialogResult.OK)
                    {
                        basename = saveFileDialog1.FileName;
                        key = ConvertToKey(PF.pass);
                        GenerateIV();
                        dataGridView1.Rows.Clear();
                        try
                        {
                            using (SQLiteConnection SC = new SQLiteConnection("DataSource=" + basename))
                            {
                                SC.Open();
                                using (SQLiteCommand cmd = new SQLiteCommand(SC))
                                {
                                    cmd.CommandText = @"create table if not exists Keys (KeyName blob not null primary key, Key blob not null)";
                                    cmd.CommandType = CommandType.Text;
                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText = @"create table if not exists IV (Vector blob not null primary key)";
                                    cmd.ExecuteNonQuery();


                                    cmd.Parameters.Add("@iv", DbType.Binary).Value = currentiv;
                                    cmd.CommandText = @"insert into IV values (@iv)";
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            EditTablePanel.Visible = true;
                            SortButton.Visible = true;
                            AscSort = true;
                            SortButton.BackgroundImage = SortPic.Image;
                            SaveTableButton.Visible = false;
                            HasUnsavedData = false;
                            SortButton.BackgroundImage = SortPic.Image;
                            Text = programname  +": " + Path.GetFileName(basename);
                        }
                        catch
                        {
                            MessageBox.Show("Couldn't create data base", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void AddToTableButton_Click(object sender, EventArgs e)
        {
            using (CreateKeyForm CK = new CreateKeyForm(CreateNameList(), GeneratorDictionary, FormTranslationDictionary["CreateKeyForm"]))
            {
                if (CK.ShowDialog() == DialogResult.OK)
                {
                    
                    dataGridView1.Rows.Add();
                    


                    dataGridView1[0, dataGridView1.Rows.Count - 1].Value = CK.res.Name;
                    dataGridView1[1, dataGridView1.Rows.Count - 1].Value = CK.res.Password;
                    DataChanged();


                }
            }
        }

        private void EditSelectedButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                using (CreateKeyForm CK = new CreateKeyForm(CreateNameList(), dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString(), dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString(), GeneratorDictionary, FormTranslationDictionary["CreateKeyForm"]))
                {
                    if (CK.ShowDialog() == DialogResult.OK)
                    {
                        dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value = CK.res.Name;
                        dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value = CK.res.Password;
                        DataChanged();
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                bool unsaved = false;
                Text = programname;
                if (HasUnsavedData)
                {
                    
                    DialogResult DR = MessageBox.Show("Do you want to save changes?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (DR == DialogResult.Yes)
                    {
                        SaveTableButton.PerformClick();
                        unsaved = false;
                    }
                    else
                    {
                        if (DR == DialogResult.No)
                        {
                            unsaved = false;
                        }
                        else
                        {
                            unsaved = true;
                        }
                    }
                }

                if (!unsaved)
                { 
                openFileDialog1.Filter = "Encrypted Password Manager Files (*.EPM)|*.EPM|" + "All files (*.*)|*.*";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {

                        using (PasswordForm PF = new PasswordForm(FormTranslationDictionary["PasswordForm"]))
                        {

                            if (PF.ShowDialog() == DialogResult.OK)
                            {
                                basename = openFileDialog1.FileName;
                                key = ConvertToKey(PF.pass);
                                dataGridView1.Rows.Clear();
                                using (SQLiteConnection SC = new SQLiteConnection("Data Source=" + basename))
                                {
                                    SC.Open();
                                    using (SQLiteCommand cmd = new SQLiteCommand(SC))
                                    {
                                        cmd.CommandType = CommandType.Text;

                                        cmd.CommandText = @"select Vector from IV";
                                        using (SQLiteDataReader r = cmd.ExecuteReader())
                                        {
                                            while (r.Read())
                                            {
                                                currentiv = (byte[])r[0];
                                            }

                                        }

                                        cmd.CommandText = @"select KeyName, Key from Keys";
                                        using (SQLiteDataReader r = cmd.ExecuteReader())
                                        {
                                            while (r.Read())
                                            {
                                                dataGridView1.Rows.Add();
                                                byte[] encstring = (byte[])r["KeyName"];
                                                dataGridView1[0, dataGridView1.Rows.Count - 1].Value = DecryptString(encstring, key, currentiv);

                                                encstring = (byte[])r["Key"];
                                                dataGridView1[1, dataGridView1.Rows.Count - 1].Value = DecryptString(encstring, key, currentiv);

                                            }
                                        }

                                    }
                                    EditTablePanel.Visible = true;
                                    SortButton.Visible = true;
                                    AscSort = true;
                                    SortButton.BackgroundImage = SortPic.Image;
                                    SaveTableButton.Visible = false;
                                    HasUnsavedData = false;
                                    Text = programname + ": " + Path.GetFileName(basename);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Couldn't open databse", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void DeleteSelectedButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                if (MessageBox.Show("Confirm deletion of password: " + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString(), "Deletion", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                    DataChanged();
                }
            }
        }

        private void SaveTableButton_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection SC = new SQLiteConnection("Data Source=" + basename))
            {
                SC.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(SC))
                {
                    cmd.CommandText = @"delete from Keys";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"delete from IV";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"insert into Keys values(@name, @pass)";
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        cmd.Parameters.Add("@name", DbType.Binary).Value = EncryptString(dataGridView1[0, i].Value.ToString(), key, currentiv);
                        cmd.Parameters.Add("@pass", DbType.Binary).Value = EncryptString(dataGridView1[1, i].Value.ToString(), key, currentiv);
                        cmd.ExecuteNonQuery();
                    }
                    
                    cmd.Parameters.Add("@iv", DbType.Binary).Value = currentiv;
                    cmd.CommandText = @"insert into IV values (@iv)";
                    cmd.ExecuteNonQuery();
                }

            }

            SaveTableButton.Visible = false;
            HasUnsavedData = false;

            pictureBox1.Visible = true;
            pictureBox1.BackColor = Color.FromArgb(0,0,0);
            AnimationTimer.Enabled = true;

            animationdirection = Math.Abs(animationdirection);
            saveanimationtime = startcolor;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (HasUnsavedData)
            {
                DialogResult DR = MessageBox.Show("All unsaved changes will be lost. Do you want to save before quit?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (DR == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    if (DR == DialogResult.Yes)
                    {
                        SaveTableButton.PerformClick();
                    }
                    
                }
            }
        }


        int ValidateColor(int value)
        {
            if (value < 0)
                value = 0;
            if (value > 255)
                value = 255;
            return value;
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (saveanimationtime >= startcolor)
            {
                saveanimationtime += animationdirection;
                
                pictureBox1.BackColor = Color.FromArgb(ValidateColor(saveanimationtime), ValidateColor(saveanimationtime),ValidateColor(saveanimationtime));
                if (saveanimationtime > maxcolor)
                    animationdirection *= -1;


            }
            else
            {
                saveanimationtime = startcolor;
                AnimationTimer.Enabled = false;
                pictureBox1.Visible = false;
            }

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                UpButton.Visible = true;
                DownButton.Visible = true;
                CopyButton.Visible = true;
            }
            else
            {
                UpButton.Visible = false;
                DownButton.Visible = false;
                CopyButton.Visible = false;
            }
        }

        private void UpButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex > 0)
            {
                KeyPair KP = new KeyPair(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString(), dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString());
                dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value = dataGridView1[0, dataGridView1.CurrentCell.RowIndex - 1].Value;
                dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value = dataGridView1[1, dataGridView1.CurrentCell.RowIndex - 1].Value;

                dataGridView1[0, dataGridView1.CurrentCell.RowIndex - 1].Value = KP.Name;
                dataGridView1[1, dataGridView1.CurrentCell.RowIndex - 1].Value = KP.Password;

                dataGridView1.CurrentCell = dataGridView1[dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex - 1];

                KP.Name = null;
                KP.Password = null;
                DataChanged();
            }
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex < dataGridView1.Rows.Count - 1)
            {
                KeyPair KP = new KeyPair(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString(), dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString());
                dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value = dataGridView1[0, dataGridView1.CurrentCell.RowIndex + 1].Value;
                dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value = dataGridView1[1, dataGridView1.CurrentCell.RowIndex + 1].Value;

                dataGridView1[0, dataGridView1.CurrentCell.RowIndex + 1].Value = KP.Name;
                dataGridView1[1, dataGridView1.CurrentCell.RowIndex + 1].Value = KP.Password;

                dataGridView1.CurrentCell = dataGridView1[dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex + 1];

                KP.Name = null;
                KP.Password = null;
                DataChanged();
            }
        }

        private void SortButton_Click(object sender, EventArgs e)
        {
            if (AscSort)
            {
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
                SortButton.BackgroundImage = SortRevPic.Image;
                AscSort = false;
            }
            else
            {
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
                SortButton.BackgroundImage = SortPic.Image;
                AscSort = true;
            }
            DataChanged();

        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString());
            CopyButton.Visible = false;
            BufferAnimationTimer.Enabled = true;
            pictureBox2.Visible = true;

        }

        private void BufferAnimationTimer_Tick(object sender, EventArgs e)
        {
            if (bufferanimationtime >= startcolor)
            {
                bufferanimationtime += bufferanimationdirection;

                pictureBox2.BackColor = Color.FromArgb(ValidateColor(bufferanimationtime), ValidateColor(bufferanimationtime), ValidateColor(bufferanimationtime));
                if (bufferanimationtime > maxcolor)
                    bufferanimationdirection *= -1;


            }
            else
            {
                bufferanimationtime = startcolor;
                BufferAnimationTimer.Enabled = false;
                pictureBox2.Visible = false;
                CopyButton.Visible = true;
                bufferanimationdirection = Math.Abs(bufferanimationdirection);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2.PerformClick();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            dataGridView1.Columns[1].Visible = !dataGridView1.Columns[1].Visible;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CreateTranslationFile("EN.json", false);
        }
    }
}
