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
        public KeyData res;
        List<string> nameList;
        bool renaming;
        Dictionary<string, string> generatorDictionary;
        
       

        public CreateKeyForm(List<string> keynames, Dictionary<string, string> dict, Dictionary<string, TranslationPair> translation)
        {             
            InitializeComponent();
            nameList = keynames;
            renaming = false;
            generatorDictionary = dict;


            if (translation != null)
                Apply_Translation(this, translation);
        }

        public CreateKeyForm()
        {
            InitializeComponent();
        }

        
        public CreateKeyForm(List<string> keynames, string name, string login, string url, string password, Dictionary<string, string> dict, Dictionary<string, TranslationPair> translation)
        {
            InitializeComponent();
            if (translation != null)
                Apply_Translation(this, translation);
            PNameBox.Text = name;
            PasswordBox.Text = password;
            LoginBox.Text = login;
            URLBox.Text = url;
            nameList = keynames;
            renaming = true;
            generatorDictionary = dict;

            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            KeyData KP = new KeyData(PNameBox.Text, PasswordBox.Text, LoginBox.Text, URLBox.Text);
            if (KP.IsValid)
            {
                
                if (nameList.IndexOf(KP.Name) == -1 || renaming)
                {
                    res = KP;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(PasswordExistsLabel.Text, ErrorLabel.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(NotValidLabel.Text, ErrorLabel.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        char Generate_Cymbol(Random rnd)
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
                        pick = rnd.Next(0, generatorDictionary.Keys.Count);
                    } while (generatorDictionary.ElementAt(pick).Key.Length > (int)numericUpDown1.Value - currentlength);
                    currentlength += generatorDictionary.ElementAt(pick).Key.Length;
                    pass += generatorDictionary.ElementAt(pick).Key;
                }

                for (int i = currentlength; i < (int)numericUpDown1.Value; i++)
                {
                    pass += Generate_Cymbol(rnd);
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

    }
}
