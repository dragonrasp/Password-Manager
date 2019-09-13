using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PassManager.TranslationTools;

namespace PassManager
{
    public partial class SettingsForm : Form
    {
        const string HIDE_PASSWORDS_PARAMETER = "Hide passwords";
        const string TRANSLATION_DIRECTORY_PARAMETR = "Translation directory";
        const string CLIPBOARD_TIMER_PARAMETER = "Clipboard timer";

        public Dictionary<string, string> Settings = new Dictionary<string, string>();

        public SettingsForm()
        {
            InitializeComponent();
        }

        public SettingsForm(Dictionary<string, string> stngs, Dictionary<string, TranslationPair> translation)
        {
            InitializeComponent();
            if (translation != null)
                ApplyTranslation(this, translation);
            if (Convert.ToBoolean(stngs[HIDE_PASSWORDS_PARAMETER]))
                HidePasswordCheckbox.Checked = true;
            else
                HidePasswordCheckbox.Checked = false;

            TranslationBox.Text = stngs[TRANSLATION_DIRECTORY_PARAMETR];

            numericUpDown1.Value = Convert.ToInt32(stngs[CLIPBOARD_TIMER_PARAMETER]);

            
        }

        
        private void Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TranslationBox.Text = openFileDialog1.FileName;
            }
        }

        private void CreateTranslationButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                CreateTranslationFile(saveFileDialog1.FileName, true);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Settings.Add(HIDE_PASSWORDS_PARAMETER, HidePasswordCheckbox.Checked.ToString());
            Settings.Add(TRANSLATION_DIRECTORY_PARAMETR, TranslationBox.Text);
            Settings.Add(CLIPBOARD_TIMER_PARAMETER, numericUpDown1.Value.ToString());
            DialogResult = DialogResult.OK;
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(numericUpDown1.Value) == 0)
            {
                label2.Enabled = false;
                label3.Enabled = false;
            }
            else
            {
                label2.Enabled = true;
                label3.Enabled = true;
            }

        }
    }
}
