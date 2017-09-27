namespace autowpp
{
    partial class MainForm
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
            this.MSaveFIleDialog = new System.Windows.Forms.SaveFileDialog();
            this.MOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.MDataGridView = new System.Windows.Forms.DataGridView();
            this.MMenuBar = new System.Windows.Forms.MenuStrip();
            this.FileMMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenMMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveMMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMMenuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditMMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BoundsMMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OptioinsMMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CurrentTaskComboBoxMMenuItem = new System.Windows.Forms.ToolStripComboBox();
            this.ShowChartsMMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MStatusBar = new System.Windows.Forms.StatusStrip();
            this.MStatusBarLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.CheckBoundsButton = new System.Windows.Forms.Button();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.MDataGridView)).BeginInit();
            this.MMenuBar.SuspendLayout();
            this.MStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // MOpenFileDialog
            // 
            this.MOpenFileDialog.FileName = "openFileDialog1";
            // 
            // MDataGridView
            // 
            this.MDataGridView.AllowUserToAddRows = false;
            this.MDataGridView.AllowUserToDeleteRows = false;
            this.MDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MDataGridView.Location = new System.Drawing.Point(12, 27);
            this.MDataGridView.Name = "MDataGridView";
            this.MDataGridView.Size = new System.Drawing.Size(1110, 280);
            this.MDataGridView.TabIndex = 0;
            this.MDataGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.MDataGridView_CellMouseClick);
            this.MDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.MDataGridView_ColumnHeaderMouseClick);
            this.MDataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MDataGridView_MouseDown);
            // 
            // MMenuBar
            // 
            this.MMenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMMenuItem,
            this.EditMMenuItem,
            this.OptioinsMMenuItem,
            this.CurrentTaskComboBoxMMenuItem,
            this.ShowChartsMMenuItem,
            this.оПрограммеToolStripMenuItem});
            this.MMenuBar.Location = new System.Drawing.Point(0, 0);
            this.MMenuBar.Name = "MMenuBar";
            this.MMenuBar.Size = new System.Drawing.Size(1134, 27);
            this.MMenuBar.TabIndex = 1;
            this.MMenuBar.Text = "menuStrip1";
            // 
            // FileMMenuItem
            // 
            this.FileMMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenMMenuItem,
            this.SaveMMenuItem,
            this.FileMMenuSeparator1,
            this.ExitMMenuItem});
            this.FileMMenuItem.Name = "FileMMenuItem";
            this.FileMMenuItem.Size = new System.Drawing.Size(48, 23);
            this.FileMMenuItem.Text = "Файл";
            // 
            // OpenMMenuItem
            // 
            this.OpenMMenuItem.Name = "OpenMMenuItem";
            this.OpenMMenuItem.Size = new System.Drawing.Size(302, 22);
            this.OpenMMenuItem.Text = "Открыть и импортировать новые данные";
            this.OpenMMenuItem.Click += new System.EventHandler(this.OpenMMenuItem_Click);
            // 
            // SaveMMenuItem
            // 
            this.SaveMMenuItem.Name = "SaveMMenuItem";
            this.SaveMMenuItem.Size = new System.Drawing.Size(302, 22);
            this.SaveMMenuItem.Text = "Сохранить данные";
            this.SaveMMenuItem.Click += new System.EventHandler(this.SaveMMenuItem_Click);
            // 
            // FileMMenuSeparator1
            // 
            this.FileMMenuSeparator1.Name = "FileMMenuSeparator1";
            this.FileMMenuSeparator1.Size = new System.Drawing.Size(299, 6);
            // 
            // ExitMMenuItem
            // 
            this.ExitMMenuItem.Name = "ExitMMenuItem";
            this.ExitMMenuItem.Size = new System.Drawing.Size(302, 22);
            this.ExitMMenuItem.Text = "Выход";
            this.ExitMMenuItem.Click += new System.EventHandler(this.ExitMMenuItem_Click);
            // 
            // EditMMenuItem
            // 
            this.EditMMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BoundsMMenuItem});
            this.EditMMenuItem.Name = "EditMMenuItem";
            this.EditMMenuItem.Size = new System.Drawing.Size(59, 23);
            this.EditMMenuItem.Text = "Правка";
            // 
            // BoundsMMenuItem
            // 
            this.BoundsMMenuItem.Name = "BoundsMMenuItem";
            this.BoundsMMenuItem.Size = new System.Drawing.Size(148, 22);
            this.BoundsMMenuItem.Text = "Ограничения";
            this.BoundsMMenuItem.Click += new System.EventHandler(this.BoundsMMenuItem_Click);
            // 
            // OptioinsMMenuItem
            // 
            this.OptioinsMMenuItem.Name = "OptioinsMMenuItem";
            this.OptioinsMMenuItem.Size = new System.Drawing.Size(79, 23);
            this.OptioinsMMenuItem.Text = "Настройки";
            this.OptioinsMMenuItem.Click += new System.EventHandler(this.OptioinsMMenuItem_Click);
            // 
            // CurrentTaskComboBoxMMenuItem
            // 
            this.CurrentTaskComboBoxMMenuItem.Items.AddRange(new object[] {
            "Задача 1 - Задача расчета водно-энергетического режима ГЭС.",
            "Задача 2 - Задача расчета допустимого режима ГЭС при пропуске половодья."});
            this.CurrentTaskComboBoxMMenuItem.Name = "CurrentTaskComboBoxMMenuItem";
            this.CurrentTaskComboBoxMMenuItem.Size = new System.Drawing.Size(350, 23);
            this.CurrentTaskComboBoxMMenuItem.SelectedIndexChanged += new System.EventHandler(this.CurrentTaskComboBoxMMenuItem_SelectedIndexChanged);
            // 
            // ShowChartsMMenuItem
            // 
            this.ShowChartsMMenuItem.Enabled = false;
            this.ShowChartsMMenuItem.Name = "ShowChartsMMenuItem";
            this.ShowChartsMMenuItem.Size = new System.Drawing.Size(119, 23);
            this.ShowChartsMMenuItem.Text = "Показать графики";
            this.ShowChartsMMenuItem.Click += new System.EventHandler(this.ShowChartsMMenuItem_Click);
            // 
            // MStatusBar
            // 
            this.MStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MStatusBarLabel});
            this.MStatusBar.Location = new System.Drawing.Point(0, 339);
            this.MStatusBar.Name = "MStatusBar";
            this.MStatusBar.Size = new System.Drawing.Size(1134, 22);
            this.MStatusBar.TabIndex = 3;
            this.MStatusBar.Text = "MStatusBar";
            // 
            // MStatusBarLabel
            // 
            this.MStatusBarLabel.Name = "MStatusBarLabel";
            this.MStatusBarLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(12, 313);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(909, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Расчет";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CheckBoundsButton
            // 
            this.CheckBoundsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBoundsButton.Location = new System.Drawing.Point(927, 313);
            this.CheckBoundsButton.Name = "CheckBoundsButton";
            this.CheckBoundsButton.Size = new System.Drawing.Size(195, 23);
            this.CheckBoundsButton.TabIndex = 5;
            this.CheckBoundsButton.Text = "Проверить";
            this.CheckBoundsButton.UseVisualStyleBackColor = true;
            this.CheckBoundsButton.Click += new System.EventHandler(this.CheckBoundsButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 361);
            this.Controls.Add(this.CheckBoundsButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.MStatusBar);
            this.Controls.Add(this.MDataGridView);
            this.Controls.Add(this.MMenuBar);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "АНГЭС";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.MDataGridView)).EndInit();
            this.MMenuBar.ResumeLayout(false);
            this.MMenuBar.PerformLayout();
            this.MStatusBar.ResumeLayout(false);
            this.MStatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog MSaveFIleDialog;
        private System.Windows.Forms.OpenFileDialog MOpenFileDialog;
        private System.Windows.Forms.DataGridView MDataGridView;
        private System.Windows.Forms.MenuStrip MMenuBar;
        private System.Windows.Forms.ToolStripMenuItem FileMMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenMMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveMMenuItem;
        private System.Windows.Forms.ToolStripSeparator FileMMenuSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExitMMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditMMenuItem;
        private System.Windows.Forms.StatusStrip MStatusBar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem BoundsMMenuItem;
        private System.Windows.Forms.Button CheckBoundsButton;
        private System.Windows.Forms.ToolStripMenuItem OptioinsMMenuItem;
        private System.Windows.Forms.ToolStripComboBox CurrentTaskComboBoxMMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowChartsMMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel MStatusBarLabel;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
    }
}

