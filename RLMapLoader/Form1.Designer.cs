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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentFreeplayMapTextBox = new System.Windows.Forms.TextBox();
            this.loadMapButton = new System.Windows.Forms.Button();
            this.saveMapNameButton = new System.Windows.Forms.Button();
            this.replacePauseTextCheckBox = new System.Windows.Forms.CheckBox();
            this.reloadGameButton = new System.Windows.Forms.Button();
            this.CookedPCConsoleDir = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(36, 130);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(534, 271);
            this.dataGridView1.TabIndex = 1;
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 105);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Presets";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(609, 35);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // currentFreeplayMapTextBox
            // 
            this.currentFreeplayMapTextBox.Location = new System.Drawing.Point(207, 49);
            this.currentFreeplayMapTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.currentFreeplayMapTextBox.MaxLength = 6;
            this.currentFreeplayMapTextBox.Name = "currentFreeplayMapTextBox";
            this.currentFreeplayMapTextBox.Size = new System.Drawing.Size(148, 26);
            this.currentFreeplayMapTextBox.TabIndex = 5;
            this.currentFreeplayMapTextBox.Text = "Loading...";
            // 
            // loadMapButton
            // 
            this.loadMapButton.Location = new System.Drawing.Point(366, 46);
            this.loadMapButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loadMapButton.Name = "loadMapButton";
            this.loadMapButton.Size = new System.Drawing.Size(70, 35);
            this.loadMapButton.TabIndex = 6;
            this.loadMapButton.Text = "Load";
            this.loadMapButton.UseVisualStyleBackColor = true;
            this.loadMapButton.Click += new System.EventHandler(this.loadMapButton_Click);
            // 
            // saveMapNameButton
            // 
            this.saveMapNameButton.Location = new System.Drawing.Point(446, 46);
            this.saveMapNameButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.saveMapNameButton.Name = "saveMapNameButton";
            this.saveMapNameButton.Size = new System.Drawing.Size(94, 34);
            this.saveMapNameButton.TabIndex = 7;
            this.saveMapNameButton.Text = "Save";
            this.saveMapNameButton.UseVisualStyleBackColor = true;
            this.saveMapNameButton.Click += new System.EventHandler(this.saveMapNameButton_Click);
            // 
            // replacePauseTextCheckBox
            // 
            this.replacePauseTextCheckBox.AutoSize = true;
            this.replacePauseTextCheckBox.Location = new System.Drawing.Point(366, 90);
            this.replacePauseTextCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.replacePauseTextCheckBox.Name = "replacePauseTextCheckBox";
            this.replacePauseTextCheckBox.Size = new System.Drawing.Size(177, 24);
            this.replacePauseTextCheckBox.TabIndex = 8;
            this.replacePauseTextCheckBox.Text = "Replace Pause Text";
            this.replacePauseTextCheckBox.UseVisualStyleBackColor = true;
            this.replacePauseTextCheckBox.CheckedChanged += new System.EventHandler(this.replacePauseTextCheckBox_CheckedChanged);
            // 
            // reloadGameButton
            // 
            this.reloadGameButton.Location = new System.Drawing.Point(406, 424);
            this.reloadGameButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.reloadGameButton.Name = "reloadGameButton";
            this.reloadGameButton.Size = new System.Drawing.Size(164, 48);
            this.reloadGameButton.TabIndex = 9;
            this.reloadGameButton.Text = "Reload Game";
            this.reloadGameButton.UseVisualStyleBackColor = true;
            this.reloadGameButton.Click += new System.EventHandler(this.reloadGameButton_Click);
            // 
            // CookedPCConsoleDir
            // 
            this.CookedPCConsoleDir.Location = new System.Drawing.Point(36, 435);
            this.CookedPCConsoleDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CookedPCConsoleDir.Name = "CookedPCConsoleDir";
            this.CookedPCConsoleDir.Size = new System.Drawing.Size(362, 26);
            this.CookedPCConsoleDir.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 494);
            this.Controls.Add(this.CookedPCConsoleDir);
            this.Controls.Add(this.reloadGameButton);
            this.Controls.Add(this.replacePauseTextCheckBox);
            this.Controls.Add(this.saveMapNameButton);
            this.Controls.Add(this.loadMapButton);
            this.Controls.Add(this.currentFreeplayMapTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "RL Map Loader";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.TextBox currentFreeplayMapTextBox;
        private System.Windows.Forms.Button loadMapButton;
        private System.Windows.Forms.Button saveMapNameButton;
        private System.Windows.Forms.CheckBox replacePauseTextCheckBox;
        private System.Windows.Forms.Button reloadGameButton;
        private System.Windows.Forms.TextBox CookedPCConsoleDir;
    }
}

