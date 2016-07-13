namespace RLMapLoader
{
    partial class Form1
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
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMapButton = new System.Windows.Forms.Button();
            this.replacePauseTextCheckBox = new System.Windows.Forms.CheckBox();
            this.rlDirTextBox = new System.Windows.Forms.TextBox();
            this.mapSelectComboBox = new System.Windows.Forms.ComboBox();
            this.modsDirTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.selectRLButton = new System.Windows.Forms.Button();
            this.selectModsButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.deleteParkPBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadOnStartCheckBox = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Current Freeplay Map:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(604, 35);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteParkPBackupToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadMapButton
            // 
            this.loadMapButton.Location = new System.Drawing.Point(508, 45);
            this.loadMapButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loadMapButton.Name = "loadMapButton";
            this.loadMapButton.Size = new System.Drawing.Size(70, 38);
            this.loadMapButton.TabIndex = 6;
            this.loadMapButton.Text = "Load";
            this.loadMapButton.UseVisualStyleBackColor = true;
            this.loadMapButton.Click += new System.EventHandler(this.loadMapButton_Click);
            // 
            // replacePauseTextCheckBox
            // 
            this.replacePauseTextCheckBox.AutoSize = true;
            this.replacePauseTextCheckBox.Enabled = false;
            this.replacePauseTextCheckBox.Location = new System.Drawing.Point(400, 86);
            this.replacePauseTextCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.replacePauseTextCheckBox.Name = "replacePauseTextCheckBox";
            this.replacePauseTextCheckBox.Size = new System.Drawing.Size(177, 24);
            this.replacePauseTextCheckBox.TabIndex = 8;
            this.replacePauseTextCheckBox.Text = "Replace Pause Text";
            this.replacePauseTextCheckBox.UseVisualStyleBackColor = true;
            this.replacePauseTextCheckBox.CheckedChanged += new System.EventHandler(this.replacePauseTextCheckBox_CheckedChanged);
            // 
            // rlDirTextBox
            // 
            this.rlDirTextBox.Location = new System.Drawing.Point(115, 123);
            this.rlDirTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rlDirTextBox.Name = "rlDirTextBox";
            this.rlDirTextBox.Size = new System.Drawing.Size(385, 26);
            this.rlDirTextBox.TabIndex = 10;
            // 
            // mapSelectComboBox
            // 
            this.mapSelectComboBox.FormattingEnabled = true;
            this.mapSelectComboBox.Location = new System.Drawing.Point(205, 50);
            this.mapSelectComboBox.Name = "mapSelectComboBox";
            this.mapSelectComboBox.Size = new System.Drawing.Size(295, 28);
            this.mapSelectComboBox.TabIndex = 11;
            this.mapSelectComboBox.Text = "Park_P.upk";
            this.mapSelectComboBox.SelectedIndexChanged += new System.EventHandler(this.mapSelectComboBox_SelectedIndexChanged);
            // 
            // modsDirTextBox
            // 
            this.modsDirTextBox.Location = new System.Drawing.Point(115, 159);
            this.modsDirTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.modsDirTextBox.Name = "modsDirTextBox";
            this.modsDirTextBox.Size = new System.Drawing.Size(385, 26);
            this.modsDirTextBox.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "RL Dir:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "Mods Dir:";
            // 
            // selectRLButton
            // 
            this.selectRLButton.Location = new System.Drawing.Point(507, 119);
            this.selectRLButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.selectRLButton.Name = "selectRLButton";
            this.selectRLButton.Size = new System.Drawing.Size(70, 35);
            this.selectRLButton.TabIndex = 15;
            this.selectRLButton.Text = "...";
            this.selectRLButton.UseVisualStyleBackColor = true;
            this.selectRLButton.Click += new System.EventHandler(this.selectRLButton_Click);
            // 
            // selectModsButton
            // 
            this.selectModsButton.Location = new System.Drawing.Point(507, 155);
            this.selectModsButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.selectModsButton.Name = "selectModsButton";
            this.selectModsButton.Size = new System.Drawing.Size(70, 34);
            this.selectModsButton.TabIndex = 16;
            this.selectModsButton.Text = "...";
            this.selectModsButton.UseVisualStyleBackColor = true;
            this.selectModsButton.Click += new System.EventHandler(this.selectModsButton_Click);
            // 
            // deleteParkPBackupToolStripMenuItem
            // 
            this.deleteParkPBackupToolStripMenuItem.Name = "deleteParkPBackupToolStripMenuItem";
            this.deleteParkPBackupToolStripMenuItem.Size = new System.Drawing.Size(264, 30);
            this.deleteParkPBackupToolStripMenuItem.Text = "Delete Park_P Backup";
            this.deleteParkPBackupToolStripMenuItem.Click += new System.EventHandler(this.deleteParkPBackupToolStripMenuItem_Click);
            // 
            // loadOnStartCheckBox
            // 
            this.loadOnStartCheckBox.AutoSize = true;
            this.loadOnStartCheckBox.Location = new System.Drawing.Point(205, 86);
            this.loadOnStartCheckBox.Name = "loadOnStartCheckBox";
            this.loadOnStartCheckBox.Size = new System.Drawing.Size(167, 24);
            this.loadOnStartCheckBox.TabIndex = 17;
            this.loadOnStartCheckBox.Text = "Load Map on Start";
            this.loadOnStartCheckBox.UseVisualStyleBackColor = true;
            this.loadOnStartCheckBox.CheckedChanged += new System.EventHandler(this.loadOnStartCheckBox_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 215);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(604, 28);
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 23);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 243);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.loadOnStartCheckBox);
            this.Controls.Add(this.selectModsButton);
            this.Controls.Add(this.selectRLButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.modsDirTextBox);
            this.Controls.Add(this.mapSelectComboBox);
            this.Controls.Add(this.rlDirTextBox);
            this.Controls.Add(this.replacePauseTextCheckBox);
            this.Controls.Add(this.loadMapButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "RL Map Loader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Button loadMapButton;
        private System.Windows.Forms.CheckBox replacePauseTextCheckBox;
        private System.Windows.Forms.TextBox rlDirTextBox;
        private System.Windows.Forms.ComboBox mapSelectComboBox;
        private System.Windows.Forms.TextBox modsDirTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button selectRLButton;
        private System.Windows.Forms.Button selectModsButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem deleteParkPBackupToolStripMenuItem;
        private System.Windows.Forms.CheckBox loadOnStartCheckBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
    }
}

