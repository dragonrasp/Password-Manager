namespace PassManager
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.KeyNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PasswordColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AnimationTimer = new System.Windows.Forms.Timer(this.components);
            this.UpButton = new System.Windows.Forms.Button();
            this.DownButton = new System.Windows.Forms.Button();
            this.SortButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.SortPic = new System.Windows.Forms.PictureBox();
            this.SortRevPic = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.BufferAnimationTimer = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.DeleteSelectedButton = new System.Windows.Forms.Button();
            this.EditSelectedButton = new System.Windows.Forms.Button();
            this.AddToTableButton = new System.Windows.Forms.Button();
            this.SaveTableButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditTablePanel = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SortPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SortRevPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.EditTablePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gainsboro;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(9, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 60);
            this.button1.TabIndex = 0;
            this.toolTip1.SetToolTip(this.button1, "Open password storage");
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Gainsboro;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(74, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(60, 60);
            this.button2.TabIndex = 1;
            this.toolTip1.SetToolTip(this.button2, "Create password storage");
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KeyNameColumn,
            this.PasswordColumn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(9, 93);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(386, 308);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            // 
            // KeyNameColumn
            // 
            this.KeyNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.KeyNameColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.KeyNameColumn.HeaderText = "Name";
            this.KeyNameColumn.Name = "KeyNameColumn";
            this.KeyNameColumn.ReadOnly = true;
            this.KeyNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // PasswordColumn
            // 
            this.PasswordColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PasswordColumn.HeaderText = "Password";
            this.PasswordColumn.Name = "PasswordColumn";
            this.PasswordColumn.ReadOnly = true;
            this.PasswordColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PasswordColumn.Visible = false;
            // 
            // AnimationTimer
            // 
            this.AnimationTimer.Interval = 1;
            this.AnimationTimer.Tick += new System.EventHandler(this.AnimationTimer_Tick);
            // 
            // UpButton
            // 
            this.UpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UpButton.BackColor = System.Drawing.Color.Gainsboro;
            this.UpButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("UpButton.BackgroundImage")));
            this.UpButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.UpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpButton.Location = new System.Drawing.Point(9, 407);
            this.UpButton.Name = "UpButton";
            this.UpButton.Size = new System.Drawing.Size(60, 30);
            this.UpButton.TabIndex = 7;
            this.toolTip1.SetToolTip(this.UpButton, "Move up");
            this.UpButton.UseVisualStyleBackColor = false;
            this.UpButton.Visible = false;
            this.UpButton.Click += new System.EventHandler(this.UpButton_Click);
            // 
            // DownButton
            // 
            this.DownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DownButton.BackColor = System.Drawing.Color.Gainsboro;
            this.DownButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DownButton.BackgroundImage")));
            this.DownButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.DownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DownButton.Location = new System.Drawing.Point(9, 437);
            this.DownButton.Name = "DownButton";
            this.DownButton.Size = new System.Drawing.Size(60, 30);
            this.DownButton.TabIndex = 8;
            this.toolTip1.SetToolTip(this.DownButton, "Move down");
            this.DownButton.UseVisualStyleBackColor = false;
            this.DownButton.Visible = false;
            this.DownButton.Click += new System.EventHandler(this.DownButton_Click);
            // 
            // SortButton
            // 
            this.SortButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SortButton.BackColor = System.Drawing.Color.Gainsboro;
            this.SortButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SortButton.BackgroundImage")));
            this.SortButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.SortButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SortButton.Location = new System.Drawing.Point(75, 407);
            this.SortButton.Name = "SortButton";
            this.SortButton.Size = new System.Drawing.Size(60, 60);
            this.SortButton.TabIndex = 9;
            this.toolTip1.SetToolTip(this.SortButton, "Sort");
            this.SortButton.UseVisualStyleBackColor = false;
            this.SortButton.Visible = false;
            this.SortButton.Click += new System.EventHandler(this.SortButton_Click);
            // 
            // CopyButton
            // 
            this.CopyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyButton.BackColor = System.Drawing.Color.Gainsboro;
            this.CopyButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CopyButton.BackgroundImage")));
            this.CopyButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CopyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CopyButton.Location = new System.Drawing.Point(336, 407);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(60, 60);
            this.CopyButton.TabIndex = 10;
            this.toolTip1.SetToolTip(this.CopyButton, "Copy to clipboard");
            this.CopyButton.UseVisualStyleBackColor = false;
            this.CopyButton.Visible = false;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // SortPic
            // 
            this.SortPic.Image = ((System.Drawing.Image)(resources.GetObject("SortPic.Image")));
            this.SortPic.Location = new System.Drawing.Point(186, 420);
            this.SortPic.Name = "SortPic";
            this.SortPic.Size = new System.Drawing.Size(29, 34);
            this.SortPic.TabIndex = 11;
            this.SortPic.TabStop = false;
            this.SortPic.Visible = false;
            // 
            // SortRevPic
            // 
            this.SortRevPic.Image = ((System.Drawing.Image)(resources.GetObject("SortRevPic.Image")));
            this.SortRevPic.Location = new System.Drawing.Point(234, 418);
            this.SortRevPic.Name = "SortRevPic";
            this.SortRevPic.Size = new System.Drawing.Size(42, 39);
            this.SortRevPic.TabIndex = 12;
            this.SortRevPic.TabStop = false;
            this.SortRevPic.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(336, 407);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(60, 60);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // BufferAnimationTimer
            // 
            this.BufferAnimationTimer.Interval = 1;
            this.BufferAnimationTimer.Tick += new System.EventHandler(this.BufferAnimationTimer_Tick);
            // 
            // DeleteSelectedButton
            // 
            this.DeleteSelectedButton.BackColor = System.Drawing.Color.Gainsboro;
            this.DeleteSelectedButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DeleteSelectedButton.BackgroundImage")));
            this.DeleteSelectedButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.DeleteSelectedButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteSelectedButton.Location = new System.Drawing.Point(130, 0);
            this.DeleteSelectedButton.Name = "DeleteSelectedButton";
            this.DeleteSelectedButton.Size = new System.Drawing.Size(60, 60);
            this.DeleteSelectedButton.TabIndex = 4;
            this.toolTip1.SetToolTip(this.DeleteSelectedButton, "Delete password");
            this.DeleteSelectedButton.UseVisualStyleBackColor = false;
            this.DeleteSelectedButton.Click += new System.EventHandler(this.DeleteSelectedButton_Click);
            // 
            // EditSelectedButton
            // 
            this.EditSelectedButton.BackColor = System.Drawing.Color.Gainsboro;
            this.EditSelectedButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("EditSelectedButton.BackgroundImage")));
            this.EditSelectedButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.EditSelectedButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EditSelectedButton.Location = new System.Drawing.Point(65, 0);
            this.EditSelectedButton.Name = "EditSelectedButton";
            this.EditSelectedButton.Size = new System.Drawing.Size(60, 60);
            this.EditSelectedButton.TabIndex = 5;
            this.toolTip1.SetToolTip(this.EditSelectedButton, "Edit password");
            this.EditSelectedButton.UseVisualStyleBackColor = false;
            this.EditSelectedButton.Click += new System.EventHandler(this.EditSelectedButton_Click);
            // 
            // AddToTableButton
            // 
            this.AddToTableButton.BackColor = System.Drawing.Color.Gainsboro;
            this.AddToTableButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AddToTableButton.BackgroundImage")));
            this.AddToTableButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.AddToTableButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddToTableButton.Location = new System.Drawing.Point(0, 0);
            this.AddToTableButton.Name = "AddToTableButton";
            this.AddToTableButton.Size = new System.Drawing.Size(60, 60);
            this.AddToTableButton.TabIndex = 3;
            this.toolTip1.SetToolTip(this.AddToTableButton, "Add password");
            this.AddToTableButton.UseVisualStyleBackColor = false;
            this.AddToTableButton.Click += new System.EventHandler(this.AddToTableButton_Click);
            // 
            // SaveTableButton
            // 
            this.SaveTableButton.BackColor = System.Drawing.Color.Gainsboro;
            this.SaveTableButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SaveTableButton.BackgroundImage")));
            this.SaveTableButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.SaveTableButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveTableButton.Location = new System.Drawing.Point(195, 0);
            this.SaveTableButton.Name = "SaveTableButton";
            this.SaveTableButton.Size = new System.Drawing.Size(60, 60);
            this.SaveTableButton.TabIndex = 6;
            this.toolTip1.SetToolTip(this.SaveTableButton, "Save changes");
            this.SaveTableButton.UseVisualStyleBackColor = false;
            this.SaveTableButton.Visible = false;
            this.SaveTableButton.Click += new System.EventHandler(this.SaveTableButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(195, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(60, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.programToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(405, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.createToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.createToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.createToolStripMenuItem.Text = "Create";
            this.createToolStripMenuItem.Click += new System.EventHandler(this.createToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(148, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.aboutToolStripMenuItem});
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.programToolStripMenuItem.Text = "Program";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(113, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // EditTablePanel
            // 
            this.EditTablePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EditTablePanel.Controls.Add(this.pictureBox1);
            this.EditTablePanel.Controls.Add(this.SaveTableButton);
            this.EditTablePanel.Controls.Add(this.AddToTableButton);
            this.EditTablePanel.Controls.Add(this.EditSelectedButton);
            this.EditTablePanel.Controls.Add(this.DeleteSelectedButton);
            this.EditTablePanel.Location = new System.Drawing.Point(139, 27);
            this.EditTablePanel.Name = "EditTablePanel";
            this.EditTablePanel.Size = new System.Drawing.Size(257, 60);
            this.EditTablePanel.TabIndex = 6;
            this.EditTablePanel.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(154, 425);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(26, 28);
            this.button3.TabIndex = 15;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.ClientSize = new System.Drawing.Size(405, 474);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.SortRevPic);
            this.Controls.Add(this.SortPic);
            this.Controls.Add(this.CopyButton);
            this.Controls.Add(this.SortButton);
            this.Controls.Add(this.DownButton);
            this.Controls.Add(this.UpButton);
            this.Controls.Add(this.EditTablePanel);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(421, 300);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Password Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SortPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SortRevPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.EditTablePanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Timer AnimationTimer;
        private System.Windows.Forms.Button UpButton;
        private System.Windows.Forms.Button DownButton;
        private System.Windows.Forms.Button SortButton;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.PictureBox SortPic;
        private System.Windows.Forms.PictureBox SortRevPic;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Timer BufferAnimationTimer;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button DeleteSelectedButton;
        private System.Windows.Forms.Button EditSelectedButton;
        private System.Windows.Forms.Button AddToTableButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button SaveTableButton;
        private System.Windows.Forms.Panel EditTablePanel;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeyNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PasswordColumn;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.Button button3;
    }
}

