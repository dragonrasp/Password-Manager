namespace PassManager
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.HidePasswordCheckbox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.TranslationBox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.CreateTranslationButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelSettingsButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // HidePasswordCheckbox
            // 
            this.HidePasswordCheckbox.AutoSize = true;
            this.HidePasswordCheckbox.Checked = true;
            this.HidePasswordCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HidePasswordCheckbox.Location = new System.Drawing.Point(70, 25);
            this.HidePasswordCheckbox.Name = "HidePasswordCheckbox";
            this.HidePasswordCheckbox.Size = new System.Drawing.Size(133, 17);
            this.HidePasswordCheckbox.TabIndex = 0;
            this.HidePasswordCheckbox.Text = "Hide password column";
            this.HidePasswordCheckbox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Translation file";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(12, 157);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(243, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Select translation file";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TranslationBox
            // 
            this.TranslationBox.Location = new System.Drawing.Point(12, 131);
            this.TranslationBox.Name = "TranslationBox";
            this.TranslationBox.ReadOnly = true;
            this.TranslationBox.Size = new System.Drawing.Size(243, 20);
            this.TranslationBox.TabIndex = 4;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "JSON Files (*.json)|*.json";
            // 
            // CreateTranslationButton
            // 
            this.CreateTranslationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateTranslationButton.Location = new System.Drawing.Point(12, 266);
            this.CreateTranslationButton.Name = "CreateTranslationButton";
            this.CreateTranslationButton.Size = new System.Drawing.Size(243, 27);
            this.CreateTranslationButton.TabIndex = 5;
            this.CreateTranslationButton.Text = "Create default translation file";
            this.CreateTranslationButton.UseVisualStyleBackColor = true;
            this.CreateTranslationButton.Click += new System.EventHandler(this.CreateTranslationButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OKButton.Location = new System.Drawing.Point(139, 330);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(116, 36);
            this.OKButton.TabIndex = 6;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelSettingsButton
            // 
            this.CancelSettingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelSettingsButton.Location = new System.Drawing.Point(12, 330);
            this.CancelSettingsButton.Name = "CancelSettingsButton";
            this.CancelSettingsButton.Size = new System.Drawing.Size(116, 36);
            this.CancelSettingsButton.TabIndex = 7;
            this.CancelSettingsButton.Text = "Cancel";
            this.CancelSettingsButton.UseVisualStyleBackColor = true;
            this.CancelSettingsButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "JSON Files (*.json)|*.json";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Clear clipboard after";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(134, 214);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(49, 20);
            this.numericUpDown1.TabIndex = 9;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.NumericUpDown1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(194, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "seconds";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 378);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CancelSettingsButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.CreateTranslationButton);
            this.Controls.Add(this.TranslationBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HidePasswordCheckbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox HidePasswordCheckbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TranslationBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button CreateTranslationButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelSettingsButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
    }
}