using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using static PassManager.TranslationTools;
namespace PassManager
{
    public partial class CreateKeyForm : Form
    {
        public KeyPair res;
        List<string> NameList;
        bool renaming;
        Dictionary<string, string> GeneratorDictionary;
        Dictionary<string, string> replacementdict;
        const string REPLACEMENT_DICTIONARY = "replacement_table.json";

        public CreateKeyForm()
        {
            InitializeComponent();
        }

        public CreateKeyForm(List<string> keynames, Dictionary<string, string> dict, Dictionary<string, TranslationPair> translation)
        {             
            InitializeComponent();
            NameList = keynames;
            renaming = false;
            GeneratorDictionary = dict;

            StreamReader SR = new StreamReader(REPLACEMENT_DICTIONARY);
            string tmp = SR.ReadToEnd();
            SR.Close();
            replacementdict = JsonConvert.DeserializeObject<Dictionary<string, string>>(tmp);

            ApplyTranslation(this, translation);
        }

        public CreateKeyForm(List<string> keynames, string name, string password, Dictionary<string, string> dict, Dictionary<string, TranslationPair> translation)
        {
            InitializeComponent();
            PNameBox.Text = name;
            PasswordBox.Text = password;
            NameList = keynames;
            renaming = true;
            GeneratorDictionary = dict;

            StreamReader SR = new StreamReader(REPLACEMENT_DICTIONARY);
            string tmp = SR.ReadToEnd();
            SR.Close();
            replacementdict = JsonConvert.DeserializeObject<Dictionary<string, string>>(tmp);
            ApplyTranslation(this, translation);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            KeyPair KP = new KeyPair(PNameBox.Text, PasswordBox.Text);
            if (KP.IsValid)
            {
                
                if (NameList.IndexOf(KP.Name) == -1 || renaming)
                {
                    res = KP;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("This password name already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Not a valid name or/and password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        char GenerateCymbol(Random rnd)
        {
            char c = '_';
            while (!Char.IsLetterOrDigit(c))
                c = Convert.ToChar(rnd.Next(0, 126));
            return c;
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            string pass = "";
            byte generatedcymbol;
            Random rnd = new Random();
            PasswordBox.Text = "";
            if (checkBox1.Checked)
            {
                int currentlength = 0;
                int pick = -1;

                while ((int)numericUpDown1.Value - currentlength > 3)
                { 
                    pick = -1;
                    do
                    {
                        pick = rnd.Next(0, GeneratorDictionary.Keys.Count);
                    } while (GeneratorDictionary.ElementAt(pick).Key.Length > (int)numericUpDown1.Value - currentlength);
                    currentlength += GeneratorDictionary.ElementAt(pick).Key.Length;
                    pass += GeneratorDictionary.ElementAt(pick).Key;
                }

                for (int i = currentlength; i < (int)numericUpDown1.Value; i++)
                {
                    pass += GenerateCymbol(rnd);
                }
                if (checkBox2.Checked)
                {

                    string replacedpass = "";
                    for (int i = 0; i < pass.Length; i++)
                    {
                        if (Char.IsLetter(pass[i]))
                            if (rnd.Next(0, 100) > 70)
                                replacedpass += replacementdict[Char.ToLower(pass[i]).ToString()][rnd.Next(0, replacementdict[Char.ToLower(pass[i]).ToString()].Length)];
                            else
                                replacedpass += pass[i];
                        else
                            replacedpass += pass[i];
                    }
                    pass = replacedpass;
                }
                PasswordBox.Text = pass;

            }
            else
            {
                for (int i = 0; i < (int)numericUpDown1.Value; i++)
                {
                    generatedcymbol = 0;
                    while (!Char.IsSymbol(Convert.ToChar(generatedcymbol)) && !Char.IsLetterOrDigit(Convert.ToChar(generatedcymbol)))
                        generatedcymbol = (byte)rnd.Next(0, 126);

                    pass += Convert.ToChar(generatedcymbol);
                }
                PasswordBox.Text = pass;
            }
        }

        private void RollButton_Click(object sender, EventArgs e)
        {
            

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Visible = true;
            }
            else
            {
                checkBox2.Visible = false;
            }
        }
    }
}
