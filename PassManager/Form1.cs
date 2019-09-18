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

            DataChanged += Set_Unsaved_Flag;
            DataChanged += Enable_Save_Button;

            SaveTableButton.BringToFront();
            CopyButton.BringToFront();

            Read_Settings_File();
            Apply_Settings();

            try
            {
                FormTranslationDictionary = Read_Translation_File(Settings[TRANSLATION_DIRECTORY_PARAMETR]);
                Apply_Translation(this, FormTranslationDictionary[this.GetType().Name]);
            }
            catch
            { }
            
            programName = Text;

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

            AddToTableButton.Enabled = false;
            EditSelectedButton.Enabled = false;
            DeleteSelectedButton.Enabled = false;
            SaveTableButton.Enabled = false;
            UpButton.Enabled = false;
            DownButton.Enabled = false;
            SortButton.Enabled = false;
            URLButton.Enabled = false;
            LoginButton.Enabled = false;
            CopyButton.Enabled = false;
        }

        const string SETTINGS_FILE_NAME = "settings.json";
        const string DICTIONARY_FILE_NAME = "dictionary.json";
        const string HIDE_PASSWORDS_PARAMETER = "Hide passwords";
        const string TRANSLATION_DIRECTORY_PARAMETR = "Translation directory";
        const string CLIPBOARD_TIMER_PARAMETER = "Clipboard timer";
        Dictionary<string, string> GeneratorDictionary = new Dictionary<string, string>();
        Dictionary<string, Dictionary<string, TranslationPair>> FormTranslationDictionary;
        Dictionary<string, string> Settings = new Dictionary<string, string>();

        string programName;
        string baseName;
        byte[] key;
        byte[] currentIV;
        delegate void data_changed();
        event data_changed DataChanged;

        int saveAnimationTime = 0;
        
        int bufferAnimationDirection = 10;
        int urlBufferAnimationDirection = 10;
        int loginBufferAnimationDirection = 10;
        int animationDirection = 10;
        const int startColor = 50;
        const int maxColor = 350;
        int bufferAnimationTime = startColor;
        int urlBufferAnimationTime = startColor;
        int loginBufferAnimationTime = startColor;

        bool hidePasswords;
        string translationDirectory;
        int clipboardTimer;

        bool ascSort = true;

        bool hasUnsavedData = false;

        

        void Set_Unsaved_Flag()
        {
            hasUnsavedData = true;
        }

        void Enable_Save_Button()
        {
            SaveTableButton.Enabled = true;
        }


        

        List<string> Create_Name_List()
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
            try
            { 
                using (StreamReader SR = new StreamReader(SETTINGS_FILE_NAME))
                {
                    stngs = SR.ReadToEnd();
                    SR.Close();
                }
                Settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(stngs);
            }
            catch
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
            hidePasswords = Convert.ToBoolean(Settings[HIDE_PASSWORDS_PARAMETER]);
            if (hidePasswords)
                dataGridView1.Columns[3].Visible = false;
            else
                dataGridView1.Columns[3].Visible = true;

            translationDirectory = Settings[TRANSLATION_DIRECTORY_PARAMETR];
            clipboardTimer = Convert.ToInt32(Settings[CLIPBOARD_TIMER_PARAMETER]);

            try
            {
                FormTranslationDictionary = Read_Translation_File(translationDirectory);
                Apply_Translation(this, FormTranslationDictionary[this.GetType().Name]);
            }
            catch { }
        }
        
        
        private void button2_Click(object sender, EventArgs e)
        {
            
                saveFileDialog1.Filter = "Encrypted Password Manager Files (*.EPM)|*.EPM|" + "All files (*.*)|*.*";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                using (PasswordForm PF = new PasswordForm(FormTranslationDictionary?["PasswordForm"]))
                {
                    if (PF.ShowDialog() == DialogResult.OK)
                    {
                        baseName = saveFileDialog1.FileName;
                        key = Convert_To_Key(PF.pass);
                        currentIV = Generate_IV();
                        dataGridView1.Rows.Clear();
                        try
                        {
                            using (SQLiteConnection SC = new SQLiteConnection("DataSource=" + baseName))
                            {
                                SC.Open();
                                using (SQLiteCommand cmd = new SQLiteCommand(SC))
                                {
                                    cmd.CommandText = @"create table if not exists Keys (KeyName blob not null primary key, Key blob not null, Login blob not null, URL blob)";
                                    cmd.CommandType = CommandType.Text;
                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText = @"create table if not exists IV (Vector blob not null primary key)";
                                    cmd.ExecuteNonQuery();


                                    cmd.Parameters.Add("@iv", DbType.Binary).Value = currentIV;
                                    cmd.CommandText = @"insert into IV values (@iv)";
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            
                            SortButton.Enabled = true;
                            ascSort = true;
                            SortButton.BackgroundImage = SortPic.Image;
                            SaveTableButton.Enabled = false;
                            hasUnsavedData = false;
                            SortButton.BackgroundImage = SortPic.Image;
                            Text = programName  +": " + Path.GetFileName(baseName);
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
            using (CreateKeyForm CK = new CreateKeyForm(Create_Name_List(), GeneratorDictionary, FormTranslationDictionary?["CreateKeyForm"]))
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
                using (CreateKeyForm CK = new CreateKeyForm(Create_Name_List(), dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString(), dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString(), dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString(), dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString(), GeneratorDictionary, FormTranslationDictionary?["CreateKeyForm"]))
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
            using (PasswordForm PF = new PasswordForm(FormTranslationDictionary?["PasswordForm"]))
            {

                if (PF.ShowDialog() == DialogResult.OK)
                {
                    baseName = path;
                    key = Convert_To_Key(PF.pass);
                    using (SQLiteConnection SC = new SQLiteConnection("Data Source=" + baseName))
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
                                    currentIV = (byte[])r[0];
                                }

                            }

                            cmd.CommandText = @"select KeyName, Login, URL, Key from Keys";

                            bool decrypted = false;

                            using (SQLiteDataReader r = cmd.ExecuteReader())
                            {
                                while (r.Read())
                                {
                                    byte[] encstring = (byte[])r["KeyName"];
                                    string keyname = Decrypt_String(encstring, key, currentIV);
                                    if (!decrypted)
                                    {
                                        dataGridView1.Rows.Clear();

                                        decrypted = true;
                                    }
                                    dataGridView1.Rows.Add();
                                    dataGridView1[0, dataGridView1.Rows.Count - 1].Value = keyname;

                                    encstring = (byte[])r["Login"];
                                    dataGridView1[1, dataGridView1.Rows.Count - 1].Value = Decrypt_String(encstring, key, currentIV);

                                    encstring = (byte[])r["URL"];
                                    dataGridView1[2, dataGridView1.Rows.Count - 1].Value = Decrypt_String(encstring, key, currentIV);

                                    encstring = (byte[])r["Key"];
                                    dataGridView1[3, dataGridView1.Rows.Count - 1].Value = Decrypt_String(encstring, key, currentIV);
                                }
                            }

                        }
                        
                        SortButton.Enabled = true;
                        AddToTableButton.Enabled = true;
                        ascSort = true;
                        SortButton.BackgroundImage = SortPic.Image;
                        SaveTableButton.Enabled = false;
                        hasUnsavedData = false;
                        Text = programName + ": " + Path.GetFileName(baseName);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                bool unsaved = false;

                if (hasUnsavedData)
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
            using (SQLiteConnection SC = new SQLiteConnection("Data Source=" + baseName))
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
                        cmd.Parameters.Add("@name", DbType.Binary).Value = Encrypt_String(dataGridView1[0, i].Value.ToString(), key, currentIV);
                        cmd.Parameters.Add("@log", DbType.Binary).Value = Encrypt_String(dataGridView1[1,i].Value.ToString(), key, currentIV);
                        cmd.Parameters.Add("@url", DbType.Binary).Value = Encrypt_String(dataGridView1[2, i].Value.ToString(), key, currentIV);
                        cmd.Parameters.Add("@pass", DbType.Binary).Value = Encrypt_String(dataGridView1[3, i].Value.ToString(), key, currentIV);
                        cmd.ExecuteNonQuery();
                    }
                    
                    cmd.Parameters.Add("@iv", DbType.Binary).Value = currentIV;
                    cmd.CommandText = @"insert into IV values (@iv)";
                    cmd.ExecuteNonQuery();
                }

            }

            SaveTableButton.Enabled = false;
            SaveTableButton.Visible = false;
            hasUnsavedData = false;

            pictureBox1.Visible = true;
            pictureBox1.BackColor = Color.FromArgb(0,0,0);
            AnimationTimer.Enabled = true;

            animationDirection = Math.Abs(animationDirection);
            saveAnimationTime = startColor;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hasUnsavedData)
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


        int Validate_Color(int value)
        {
            if (value < 0)
                value = 0;
            if (value > 255)
                value = 255;
            return value;
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (saveAnimationTime >= startColor)
            {
                saveAnimationTime += animationDirection;
                
                pictureBox1.BackColor = Color.FromArgb(Validate_Color(saveAnimationTime), Validate_Color(saveAnimationTime),Validate_Color(saveAnimationTime));
                if (saveAnimationTime > maxColor)
                    animationDirection *= -1;


            }
            else
            {
                saveAnimationTime = startColor;
                AnimationTimer.Enabled = false;
                pictureBox1.Visible = false;
                SaveTableButton.Visible = true;
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
                UpButton.Enabled = true;
                DownButton.Enabled = true;
                CopyButton.Enabled = true;
                URLButton.Enabled = true;
                LoginButton.Enabled = true;
                EditSelectedButton.Enabled = true;
                DeleteSelectedButton.Enabled = true;
            }
            else
            {
                UpButton.Enabled = false;
                DownButton.Enabled = false;
                CopyButton.Enabled = false;
                URLButton.Enabled = false;
                LoginButton.Enabled = false;
                EditSelectedButton.Enabled = false;
                DeleteSelectedButton.Enabled = false;
            }
        }
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            dataGridView1.Columns[3].Visible = !dataGridView1.Columns[3].Visible;
            Settings[HIDE_PASSWORDS_PARAMETER] = (!dataGridView1.Columns[3].Visible).ToString();
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
            if (ascSort)
            {
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
                SortButton.BackgroundImage = SortRevPic.Image;
                ascSort = false;
            }
            else
            {
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
                SortButton.BackgroundImage = SortPic.Image;
                ascSort = true;
            }
            DataChanged();

        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString());
            CopyButton.Visible = false;
            CopyButton.Enabled = false;
            BufferAnimationTimer.Enabled = true;
            pictureBox2.Visible = true;
            Start_Buffer_Countdown();
        }

        private void BufferAnimationTimer_Tick(object sender, EventArgs e)
        {
            if (bufferAnimationTime >= startColor)
            {
                bufferAnimationTime += bufferAnimationDirection;

                pictureBox2.BackColor = Color.FromArgb(Validate_Color(bufferAnimationTime), Validate_Color(bufferAnimationTime), Validate_Color(bufferAnimationTime));
                if (bufferAnimationTime > maxColor)
                    bufferAnimationDirection *= -1;


            }
            else
            {
                bufferAnimationTime = startColor;
                BufferAnimationTimer.Enabled = false;
                pictureBox2.Visible = false;
                CopyButton.Enabled = true;
                CopyButton.Visible = true;
                bufferAnimationDirection = Math.Abs(bufferAnimationDirection);
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

        private void button3_Click(object sender, EventArgs e)
        {
            Create_Translation_File("EN(new).json", true);
        }

        private void URLButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString());
            URLButton.Enabled = false;
            URLButton.Visible = false;
            URLBufferAnimationTimer.Enabled = true;
            pictureBox4.Visible = true;
            Start_Buffer_Countdown();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString());
            LoginButton.Enabled = false;
            LoginButton.Visible = false;
            LoginBufferAnimationTimer.Enabled = true;
            pictureBox3.Visible = true;
            Start_Buffer_Countdown();
        }

        private void URLBufferAnimationTimer_Tick(object sender, EventArgs e)
        {
            if (urlBufferAnimationTime >= startColor)
            {
                urlBufferAnimationTime += urlBufferAnimationDirection;

                pictureBox4.BackColor = Color.FromArgb(Validate_Color(urlBufferAnimationTime), Validate_Color(urlBufferAnimationTime), Validate_Color(urlBufferAnimationTime));
                if (urlBufferAnimationTime > maxColor)
                    urlBufferAnimationDirection *= -1;


            }
            else
            {
                urlBufferAnimationTime = startColor;
                URLBufferAnimationTimer.Enabled = false;
                pictureBox4.Visible = false;
                URLButton.Enabled = true;
                URLButton.Visible = true;
                urlBufferAnimationDirection = Math.Abs(urlBufferAnimationDirection);
            }
        }

        private void LoginBufferAnimationTimer_Tick(object sender, EventArgs e)
        {
            if (loginBufferAnimationTime >= startColor)
            {
                loginBufferAnimationTime += loginBufferAnimationDirection;

                pictureBox3.BackColor = Color.FromArgb(Validate_Color(loginBufferAnimationTime), Validate_Color(loginBufferAnimationTime), Validate_Color(loginBufferAnimationTime));
                if (loginBufferAnimationTime > maxColor)
                    loginBufferAnimationDirection *= -1;


            }
            else
            {
                loginBufferAnimationTime = startColor;
                LoginBufferAnimationTimer.Enabled = false;
                pictureBox3.Visible = false;
                LoginButton.Enabled = true;
                LoginButton.Visible = true;
                loginBufferAnimationDirection = Math.Abs(loginBufferAnimationDirection);
            }
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SettingsForm SF = new SettingsForm(Settings, FormTranslationDictionary?["SettingsForm"]))
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
                clipboardTimer = Convert.ToInt32(Settings[CLIPBOARD_TIMER_PARAMETER]);
                ClipboardCountdown.Enabled = true;
            }
        }

        private void ClipboardCountdown_Tick(object sender, EventArgs e)
        {
            if (clipboardTimer > 0)
            {
                clipboardTimer--;
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

        void Enabled_Control_Color_Switch(object c)
        {
            Control C = c as Control;
            if (C.Enabled)
                C.BackColor = Color.Orange;
            else
                C.BackColor = Color.Gray;
        }
        private void AddToTableButton_EnabledChanged(object sender, EventArgs e)
        {
            Enabled_Control_Color_Switch(sender);
        }

        private void EditSelectedButton_EnabledChanged(object sender, EventArgs e)
        {
            Enabled_Control_Color_Switch(sender);
        }

        private void DeleteSelectedButton_EnabledChanged(object sender, EventArgs e)
        {
            Enabled_Control_Color_Switch(sender);
        }

        private void SaveTableButton_EnabledChanged(object sender, EventArgs e)
        {
            Enabled_Control_Color_Switch(sender);
        }

        private void UpButton_EnabledChanged(object sender, EventArgs e)
        {
            Enabled_Control_Color_Switch(sender);
        }

        private void DownButton_EnabledChanged(object sender, EventArgs e)
        {
            Enabled_Control_Color_Switch(sender);
        }

        private void SortButton_EnabledChanged(object sender, EventArgs e)
        {
            Enabled_Control_Color_Switch(sender);
        }

        private void URLButton_EnabledChanged(object sender, EventArgs e)
        {
            Enabled_Control_Color_Switch(sender);
        }

        private void LoginButton_EnabledChanged(object sender, EventArgs e)
        {
            Enabled_Control_Color_Switch(sender);
        }

        private void CopyButton_EnabledChanged(object sender, EventArgs e)
        {
            Enabled_Control_Color_Switch(sender);
        }
    }
}
