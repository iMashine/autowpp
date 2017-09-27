namespace autowpp
{
    partial class RepairTimeChangerForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.StartRepairCalendar = new System.Windows.Forms.MonthCalendar();
            this.EndRepairCalendar = new System.Windows.Forms.MonthCalendar();
            this.RepairsListBox = new System.Windows.Forms.ListBox();
            this.AddRepairButton = new System.Windows.Forms.Button();
            this.RemoveRepairButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Начало ремонта";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(189, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Окончание ремонта";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(366, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ремонты";
            // 
            // StartRepairCalendar
            // 
            this.StartRepairCalendar.Location = new System.Drawing.Point(16, 35);
            this.StartRepairCalendar.MaxSelectionCount = 1;
            this.StartRepairCalendar.Name = "StartRepairCalendar";
            this.StartRepairCalendar.ShowTodayCircle = false;
            this.StartRepairCalendar.TabIndex = 3;
            // 
            // EndRepairCalendar
            // 
            this.EndRepairCalendar.Location = new System.Drawing.Point(192, 35);
            this.EndRepairCalendar.MaxSelectionCount = 1;
            this.EndRepairCalendar.Name = "EndRepairCalendar";
            this.EndRepairCalendar.ShowTodayCircle = false;
            this.EndRepairCalendar.TabIndex = 4;
            // 
            // RepairsListBox
            // 
            this.RepairsListBox.FormattingEnabled = true;
            this.RepairsListBox.Location = new System.Drawing.Point(369, 36);
            this.RepairsListBox.Name = "RepairsListBox";
            this.RepairsListBox.Size = new System.Drawing.Size(258, 160);
            this.RepairsListBox.TabIndex = 5;
            this.RepairsListBox.SelectedValueChanged += new System.EventHandler(this.RepairsListBox_SelectedValueChanged);
            // 
            // AddRepairButton
            // 
            this.AddRepairButton.Location = new System.Drawing.Point(633, 35);
            this.AddRepairButton.Name = "AddRepairButton";
            this.AddRepairButton.Size = new System.Drawing.Size(75, 23);
            this.AddRepairButton.TabIndex = 6;
            this.AddRepairButton.Text = "Добавить";
            this.AddRepairButton.UseVisualStyleBackColor = true;
            this.AddRepairButton.Click += new System.EventHandler(this.AddRepairButton_Click);
            // 
            // RemoveRepairButton
            // 
            this.RemoveRepairButton.Location = new System.Drawing.Point(633, 64);
            this.RemoveRepairButton.Name = "RemoveRepairButton";
            this.RemoveRepairButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveRepairButton.TabIndex = 7;
            this.RemoveRepairButton.Text = "Удалить";
            this.RemoveRepairButton.UseVisualStyleBackColor = true;
            this.RemoveRepairButton.Click += new System.EventHandler(this.RemoveRepairButton_Click);
            // 
            // RepairTimeChangerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 214);
            this.Controls.Add(this.RemoveRepairButton);
            this.Controls.Add(this.AddRepairButton);
            this.Controls.Add(this.RepairsListBox);
            this.Controls.Add(this.EndRepairCalendar);
            this.Controls.Add(this.StartRepairCalendar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "RepairTimeChangerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактор ремонтов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MonthCalendar StartRepairCalendar;
        private System.Windows.Forms.MonthCalendar EndRepairCalendar;
        private System.Windows.Forms.ListBox RepairsListBox;
        private System.Windows.Forms.Button AddRepairButton;
        private System.Windows.Forms.Button RemoveRepairButton;
    }
}