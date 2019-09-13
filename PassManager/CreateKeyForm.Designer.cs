namespace PassManager
{
    partial class CreateKeyForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateKeyForm));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PNameBox = new System.Windows.Forms.TextBox();
            this.PasswordBox = new System.Windows.Forms.TextBox();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.PasswordExistsLabel = new System.Windows.Forms.Label();
            this.NotValidLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LoginBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.URLBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(12, 335);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 36);
            this.button1.TabIndex = 8;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(271, 335);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 36);
            this.button2.TabIndex = 7;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(142, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Password Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // PNameBox
            // 
            this.PNameBox.Location = new System.Drawing.Point(32, 33);
            this.PNameBox.Name = "PNameBox";
            this.PNameBox.Size = new System.Drawing.Size(316, 20);
            this.PNameBox.TabIndex = 0;
            // 
            // PasswordBox
            // 
            this.PasswordBox.Location = new System.Drawing.Point(32, 148);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.Size = new System.Drawing.Size(316, 20);
            this.PasswordBox.TabIndex = 2;
            // 
            // GenerateButton
            // 
            this.GenerateButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("GenerateButton.BackgroundImage")));
            this.GenerateButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.GenerateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GenerateButton.Location = new System.Drawing.Point(81, 263);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(45, 45);
            this.GenerateButton.TabIndex = 6;
            this.toolTip1.SetToolTip(this.GenerateButton, "Generate password");
            this.GenerateButton.UseVisualStyleBackColor = true;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(145, 263);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(128, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 247);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Length";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(145, 291);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(128, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Mnemonic generation";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Location = new System.Drawing.Point(339, 283);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(29, 13);
            this.ErrorLabel.TabIndex = 11;
            this.ErrorLabel.Text = "Error";
            this.ErrorLabel.Visible = false;
            // 
            // PasswordExistsLabel
            // 
            this.PasswordExistsLabel.AutoSize = true;
            this.PasswordExistsLabel.Location = new System.Drawing.Point(325, 312);
            this.PasswordExistsLabel.Name = "PasswordExistsLabel";
            this.PasswordExistsLabel.Size = new System.Drawing.Size(170, 13);
            this.PasswordExistsLabel.TabIndex = 12;
            this.PasswordExistsLabel.Text = "This password name already exists";
            this.PasswordExistsLabel.Visible = false;
            // 
            // NotValidLabel
            // 
            this.NotValidLabel.AutoSize = true;
            this.NotValidLabel.Location = new System.Drawing.Point(327, 335);
            this.NotValidLabel.Name = "NotValidLabel";
            this.NotValidLabel.Size = new System.Drawing.Size(147, 13);
            this.NotValidLabel.TabIndex = 13;
            this.NotValidLabel.Text = "Not a valid name or password";
            this.NotValidLabel.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(166, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Login";
            // 
            // LoginBox
            // 
            this.LoginBox.Location = new System.Drawing.Point(32, 90);
            this.LoginBox.Name = "LoginBox";
            this.LoginBox.Size = new System.Drawing.Size(316, 20);
            this.LoginBox.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(170, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "URL";
            // 
            // URLBox
            // 
            this.URLBox.Location = new System.Drawing.Point(32, 205);
            this.URLBox.Name = "URLBox";
            this.URLBox.Size = new System.Drawing.Size(316, 20);
            this.URLBox.TabIndex = 3;
            // 
            // CreateKeyForm
            // 
            this.AcceptButton = this.button2;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(388, 386);
            this.Controls.Add(this.URLBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LoginBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.NotValidLabel);
            this.Controls.Add(this.PasswordExistsLabel);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.GenerateButton);
            this.Controls.Add(this.PasswordBox);
            this.Controls.Add(this.PNameBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateKeyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Password";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PNameBox;
        private System.Windows.Forms.TextBox PasswordBox;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.Label PasswordExistsLabel;
        private System.Windows.Forms.Label NotValidLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox LoginBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox URLBox;
    }
}