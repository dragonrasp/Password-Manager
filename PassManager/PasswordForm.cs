﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PassManager.TranslationTools;
namespace PassManager
{
    public partial class PasswordForm : Form
    {
        public string pass;

        public PasswordForm()
        {
            InitializeComponent();
        }

        public PasswordForm(Dictionary<string, TranslationPair> translation)
        {
            InitializeComponent();
            if (translation != null)
                Apply_Translation(this, translation);
            button1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text?.Length > 0)
            {
                DialogResult = DialogResult.OK;
                pass = textBox1.Text;
                Close();
            }
            else
                MessageBox.Show(EmptyLabel.Text, ErrorLabel.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.UseSystemPasswordChar = !checkBox1.Checked;
        }
    }
}
