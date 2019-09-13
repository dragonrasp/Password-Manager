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
using Newtonsoft.Json;
using static PassManager.TranslationTools;
using static PassManager.CryptographyTools;


namespace PassManager
{
    public partial class Form1 : Form
    {
       

        void CreateSettings()
        {
            Dictionary<string, string> settings = new Dictionary<string, string>();
            string towrite = JsonConvert.SerializeObject(settings, Formatting.Indented);
            using (StreamWriter SW = new StreamWriter(SETTINGS_FILE_NAME))
            {
                SW.Write(towrite);
                SW.Close();
            }

        }

        public Form1()
        {
            InitializeComponent();
        }

        public Form1(string[] args)
        {
            InitializeComponent();

            DataChanged += SetUnsavedFlag;
            DataChanged += ShowSaveButton;

            SaveTableButton.BringToFront();
            CopyButton.BringToFront();

            Read_Settings_File();
            Apply_Settings();

            FormTranslationDictionary = ReadTranslationFile(Settings[TRANSLATION_DIRECTORY_PARAMETR]);
            ApplyTranslation(this, FormTranslationDictionary[this.GetType().Name]);
            
            programname = Text;

            string tmp;
            try
            {
                StreamReader SR = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/" + DICTIONARY_FILE_NAME);
                tmp = SR.ReadToEnd();
                SR.Close();
            }
            catch
            {
                tmp = Properties.Resources.defaultDictionary;
                StreamWriter SW = new StreamWriter("dictionary.json");
                SW.Write(tmp);
                SW.Close();
            }
            
            GeneratorDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(tmp);

            if (args != null && args.Length > 0)
            {
                Open_File(args[0]);
            }
        }

        const string SETTINGS_FILE_NAME = "settings.json";
        const string DICTIONARY_FILE_NAME = "dictionary.json";
        const string HIDE_PASSWORDS_PARAMETER = "Hide passwords";
        const string TRANSLATION_DIRECTORY_PARAMETR = "Translation directory";
        const string CLIPBOARD_TIMER_PARAMETER = "Clipboard timer";
        Dictionary<string, string> GeneratorDictionary = new Dictionary<string, string>();
        Dictionary<string, Dictionary<string, TranslationPair>> FormTranslationDictionary;
        Dictionary<string, string> Settings = new Dictionary<string, string>();

        string programname;
        string basename;
        byte[] key;
        byte[] currentiv;
        delegate void data_changed();
        event data_changed DataChanged;

        int saveanimationtime = 0;
        
        int bufferanimationdirection = 10;
        int urlbufferanimationdirection = 10;
        int loginbufferanimationdirection = 10;
        int animationdirection = 10;
        const int startcolor = 50;
        const int maxcolor = 350;
        int bufferanimationtime = startcolor;
        int urlbufferanimationtime = startcolor;
        int loginbufferanimationtime = startcolor;

        bool HidePasswords;
        string TranslationDirectory;
        int ClipboardTimer;

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


        

        List<string> CreateNameList()
        {
            List<string> tmp = new List<string>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                tmp.Add(dataGridView1[0, i].Value.ToString());
            }
            return tmp;
        }


        void Read_Settings_File()
        {
            string stngs;
            if (File.Exists(SETTINGS_FILE_NAME))
            { 
                using (StreamReader SR = new StreamReader(SETTINGS_FILE_NAME))
                {
                    stngs = SR.ReadToEnd();
                    SR.Close();
                }
                Settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(stngs);
            }
            else
            {
                Settings = new Dictionary<string, string>();
                Settings.Add(HIDE_PASSWORDS_PARAMETER, "true");
                Settings.Add(TRANSLATION_DIRECTORY_PARAMETR, "EN.json");
                Settings.Add(CLIPBOARD_TIMER_PARAMETER, "0");
                Write_Settings();
            }
        }

        void Write_Settings()
        {
            using (StreamWriter SW = new StreamWriter(SETTINGS_FILE_NAME))
            {
                string stngs = JsonConvert.SerializeObject(Settings, Formatting.Indented);
                SW.Write(stngs);
                SW.Close();
            }
        }

        void Apply_Settings()
        {
            HidePasswords = Convert.ToBoolean(Settings[HIDE_PASSWORDS_PARAMETER]);
            if (HidePasswords)
                dataGridView1.Columns[3].Visible = false;
            else
                dataGridView1.Columns[3].Visible = true;

            TranslationDirectory = Settings[TRANSLATION_DIRECTORY_PARAMETR];
            ClipboardTimer = Convert.ToInt32(Settings[CLIPBOARD_TIMER_PARAMETER]);

            FormTranslationDictionary = ReadTranslationFile(TranslationDirectory);
            ApplyTranslation(this, FormTranslationDictionary[this.GetType().Name]);
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
                        currentiv = GenerateIV();
                        dataGridView1.Rows.Clear();
                        try
                        {
                            using (SQLiteConnection SC = new SQLiteConnection("DataSource=" + basename))
                            {
                                SC.Open();
                                using (SQLiteCommand cmd = new SQLiteCommand(SC))
                                {
                                    cmd.CommandText = @"create table if not exists Keys (KeyName blob not null primary key, Key blob not null, Login blob not null, URL blob)";
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
                            MessageBox.Show(CouldntCreateLabel.Text, ErrorLabel.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    dataGridView1[1, dataGridView1.Rows.Count - 1].Value = CK.res.Login;
                    dataGridView1[2, dataGridView1.Rows.Count - 1].Value = CK.res.URL;
                    dataGridView1[3, dataGridView1.Rows.Count - 1].Value = CK.res.Password;
                    DataChanged();


                }
            }
        }

        private void EditSelectedButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                using (CreateKeyForm CK = new CreateKeyForm(CreateNameList(), dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString(), dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString(), dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString(), dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString(), GeneratorDictionary, FormTranslationDictionary["CreateKeyForm"]))
                {
                    if (CK.ShowDialog() == DialogResult.OK)
                    {
                        dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value = CK.res.Name;
                        dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value = CK.res.Login;
                        dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value = CK.res.URL;
                        dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value = CK.res.Password;
                        DataChanged();
                    }

                }
            }
        }

        void Open_File(string path)
        {
            using (PasswordForm PF = new PasswordForm(FormTranslationDictionary["PasswordForm"]))
            {

                if (PF.ShowDialog() == DialogResult.OK)
                {
                    basename = path;
                    key = ConvertToKey(PF.pass);
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

                            cmd.CommandText = @"select KeyName, Login, URL, Key from Keys";

                            bool decrypted = false;

                            using (SQLiteDataReader r = cmd.ExecuteReader())
                            {
                                while (r.Read())
                                {
                                    byte[] encstring = (byte[])r["KeyName"];
                                    string keyname = DecryptString(encstring, key, currentiv);
                                    if (!decrypted)
                                    {
                                        dataGridView1.Rows.Clear();

                                        decrypted = true;
                                    }
                                    dataGridView1.Rows.Add();
                                    dataGridView1[0, dataGridView1.Rows.Count - 1].Value = keyname;

                                    encstring = (byte[])r["Login"];
                                    dataGridView1[1, dataGridView1.Rows.Count - 1].Value = DecryptString(encstring, key, currentiv);

                                    encstring = (byte[])r["URL"];
                                    dataGridView1[2, dataGridView1.Rows.Count - 1].Value = DecryptString(encstring, key, currentiv);

                                    encstring = (byte[])r["Key"];
                                    dataGridView1[3, dataGridView1.Rows.Count - 1].Value = DecryptString(encstring, key, currentiv);
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                bool unsaved = false;

                if (HasUnsavedData)
                {

                    DialogResult DR = MessageBox.Show(SaveChangesLabel.Text, WarningLabel.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
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
                        Open_File(openFileDialog1.FileName);
                        
                    }
                }
            }
            catch
            {
                MessageBox.Show(CouldntOpenLabel.Text, ErrorLabel.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void DeleteSelectedButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                if (MessageBox.Show(ConfirmPasswordDeletionLabel.Text + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString(), WarningLabel.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
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

                    cmd.CommandText = @"insert into Keys values(@name, @pass, @log, @url)";
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        cmd.Parameters.Add("@name", DbType.Binary).Value = EncryptString(dataGridView1[0, i].Value.ToString(), key, currentiv);
                        cmd.Parameters.Add("@log", DbType.Binary).Value = EncryptString(dataGridView1[1,i].Value.ToString(), key, currentiv);
                        cmd.Parameters.Add("@url", DbType.Binary).Value = EncryptString(dataGridView1[2, i].Value.ToString(), key, currentiv);
                        cmd.Parameters.Add("@pass", DbType.Binary).Value = EncryptString(dataGridView1[3, i].Value.ToString(), key, currentiv);
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
                DialogResult DR = MessageBox.Show(UnsavedChangesLabel.Text, WarningLabel.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
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
            Write_Settings();
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
                URLButton.Visible = true;
                LoginButton.Visible = true;
            }
            else
            {
                UpButton.Visible = false;
                DownButton.Visible = false;
                CopyButton.Visible = false;
                URLButton.Visible = false;
                LoginButton.Visible = false;
            }
        }

        private void UpButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex > 0)
            {
                KeyData KP = new KeyData(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString(), dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString(), dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString(), dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString());
                dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value = dataGridView1[0, dataGridView1.CurrentCell.RowIndex - 1].Value;
                dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value = dataGridView1[1, dataGridView1.CurrentCell.RowIndex - 1].Value;
                dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value = dataGridView1[2, dataGridView1.CurrentCell.RowIndex - 1].Value;
                dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value = dataGridView1[3, dataGridView1.CurrentCell.RowIndex - 1].Value;

                dataGridView1[0, dataGridView1.CurrentCell.RowIndex - 1].Value = KP.Name;
                dataGridView1[1, dataGridView1.CurrentCell.RowIndex - 1].Value = KP.Password;
                dataGridView1[2, dataGridView1.CurrentCell.RowIndex - 1].Value = KP.Login;
                dataGridView1[3, dataGridView1.CurrentCell.RowIndex - 1].Value = KP.URL;

                dataGridView1.CurrentCell = dataGridView1[dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex - 1];

                KP.Name = null;
                KP.Password = null;
                KP.Login = null;
                KP.URL = null;
                DataChanged();
            }
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex < dataGridView1.Rows.Count - 1)
            {
                KeyData KP = new KeyData(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString(), dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString(), dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString(), dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString());
                dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value = dataGridView1[0, dataGridView1.CurrentCell.RowIndex + 1].Value;
                dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value = dataGridView1[1, dataGridView1.CurrentCell.RowIndex + 1].Value;
                dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value = dataGridView1[2, dataGridView1.CurrentCell.RowIndex + 1].Value;
                dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value = dataGridView1[3, dataGridView1.CurrentCell.RowIndex + 1].Value;

                dataGridView1[0, dataGridView1.CurrentCell.RowIndex + 1].Value = KP.Name;
                dataGridView1[1, dataGridView1.CurrentCell.RowIndex + 1].Value = KP.Password;
                dataGridView1[2, dataGridView1.CurrentCell.RowIndex + 1].Value = KP.Login;
                dataGridView1[3, dataGridView1.CurrentCell.RowIndex + 1].Value = KP.URL;

                dataGridView1.CurrentCell = dataGridView1[dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex + 1];

                KP.Name = null;
                KP.Password = null;
                KP.Login = null;
                KP.URL = null;
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
            Clipboard.SetText(dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString());
            CopyButton.Visible = false;
            BufferAnimationTimer.Enabled = true;
            pictureBox2.Visible = true;
            Start_Buffer_Countdown();
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
            dataGridView1.Columns[3].Visible = !dataGridView1.Columns[3].Visible;
            Settings[HIDE_PASSWORDS_PARAMETER] = (!dataGridView1.Columns[3].Visible).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CreateTranslationFile("EN(new).json", true);
        }

        private void URLButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString());
            URLButton.Visible = false;
            URLBufferAnimationTimer.Enabled = true;
            pictureBox4.Visible = true;
            Start_Buffer_Countdown();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString());
            LoginButton.Visible = false;
            LoginBufferAnimationTimer.Enabled = true;
            pictureBox3.Visible = true;
            Start_Buffer_Countdown();
        }

        private void URLBufferAnimationTimer_Tick(object sender, EventArgs e)
        {
            if (urlbufferanimationtime >= startcolor)
            {
                urlbufferanimationtime += urlbufferanimationdirection;

                pictureBox4.BackColor = Color.FromArgb(ValidateColor(urlbufferanimationtime), ValidateColor(urlbufferanimationtime), ValidateColor(urlbufferanimationtime));
                if (urlbufferanimationtime > maxcolor)
                    urlbufferanimationdirection *= -1;


            }
            else
            {
                urlbufferanimationtime = startcolor;
                URLBufferAnimationTimer.Enabled = false;
                pictureBox4.Visible = false;
                URLButton.Visible = true;
                urlbufferanimationdirection = Math.Abs(urlbufferanimationdirection);
            }
        }

        private void LoginBufferAnimationTimer_Tick(object sender, EventArgs e)
        {
            if (loginbufferanimationtime >= startcolor)
            {
                loginbufferanimationtime += loginbufferanimationdirection;

                pictureBox3.BackColor = Color.FromArgb(ValidateColor(loginbufferanimationtime), ValidateColor(loginbufferanimationtime), ValidateColor(loginbufferanimationtime));
                if (loginbufferanimationtime > maxcolor)
                    loginbufferanimationdirection *= -1;


            }
            else
            {
                loginbufferanimationtime = startcolor;
                LoginBufferAnimationTimer.Enabled = false;
                pictureBox3.Visible = false;
                LoginButton.Visible = true;
                loginbufferanimationdirection = Math.Abs(loginbufferanimationdirection);
            }
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SettingsForm SF = new SettingsForm(Settings, FormTranslationDictionary["SettingsForm"]))
            {
                if (SF.ShowDialog() == DialogResult.OK)
                {
                    Settings = SF.Settings;
                    Apply_Settings();
                }
            }
        }

        void Start_Buffer_Countdown()
        {
            if (Convert.ToInt32(Settings[CLIPBOARD_TIMER_PARAMETER]) > 0)
            {
                ClipboardTimer = Convert.ToInt32(Settings[CLIPBOARD_TIMER_PARAMETER]);
                ClipboardCountdown.Enabled = true;
            }
        }

        private void ClipboardCountdown_Tick(object sender, EventArgs e)
        {
            if (ClipboardTimer > 0)
            {
                ClipboardTimer--;
            }
            else
            {
                Clipboard.Clear();
                ClipboardCountdown.Enabled = false;
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Password Manager\rDragonRasp raspbd@gmail.com\r2019\rGNU General Public License v3.0", aboutToolStripMenuItem.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
