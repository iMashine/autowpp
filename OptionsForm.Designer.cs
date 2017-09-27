namespace autowpp
{
    partial class OptionsForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Задача 1 - Задача расчета водно-энергетического режима ГЭС.");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Задача 2 - Задача расчета допустимого режима ГЭС при пропуске половодья.");
            this.FirstTaskControlPanel = new System.Windows.Forms.Panel();
            this.EndCalculate1Task = new System.Windows.Forms.MonthCalendar();
            this.StartCalculate1Task = new System.Windows.Forms.MonthCalendar();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.isAutoCalculate = new System.Windows.Forms.CheckBox();
            this.SecondTaskControlPanel = new System.Windows.Forms.Panel();
            this.EndCalculate2Task = new System.Windows.Forms.MonthCalendar();
            this.StartCalculate2Task = new System.Windows.Forms.MonthCalendar();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.AcceptOptionsButton = new System.Windows.Forms.Button();
            this.CancelOptionsButton = new System.Windows.Forms.Button();
            this.CurrentTaskTreeView = new System.Windows.Forms.TreeView();
            this.FirstTaskControlPanel.SuspendLayout();
            this.SecondTaskControlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // FirstTaskControlPanel
            // 
            this.FirstTaskControlPanel.Controls.Add(this.EndCalculate1Task);
            this.FirstTaskControlPanel.Controls.Add(this.StartCalculate1Task);
            this.FirstTaskControlPanel.Controls.Add(this.label3);
            this.FirstTaskControlPanel.Controls.Add(this.label2);
            this.FirstTaskControlPanel.Controls.Add(this.isAutoCalculate);
            this.FirstTaskControlPanel.Location = new System.Drawing.Point(226, 12);
            this.FirstTaskControlPanel.Name = "FirstTaskControlPanel";
            this.FirstTaskControlPanel.Size = new System.Drawing.Size(579, 273);
            this.FirstTaskControlPanel.TabIndex = 2;
            // 
            // EndCalculate1Task
            // 
            this.EndCalculate1Task.Location = new System.Drawing.Point(191, 54);
            this.EndCalculate1Task.Name = "EndCalculate1Task";
            this.EndCalculate1Task.ShowTodayCircle = false;
            this.EndCalculate1Task.TabIndex = 4;
            // 
            // StartCalculate1Task
            // 
            this.StartCalculate1Task.Location = new System.Drawing.Point(9, 54);
            this.StartCalculate1Task.MaxDate = new System.DateTime(2018, 4, 20, 0, 0, 0, 0);
            this.StartCalculate1Task.MinDate = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
            this.StartCalculate1Task.Name = "StartCalculate1Task";
            this.StartCalculate1Task.ShowTodayCircle = false;
            this.StartCalculate1Task.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(188, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Окончание вычислений";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Начало вычислений";
            // 
            // isAutoCalculate
            // 
            this.isAutoCalculate.AutoSize = true;
            this.isAutoCalculate.Location = new System.Drawing.Point(9, 10);
            this.isAutoCalculate.Name = "isAutoCalculate";
            this.isAutoCalculate.Size = new System.Drawing.Size(459, 17);
            this.isAutoCalculate.TabIndex = 0;
            this.isAutoCalculate.Text = "Расчитывать на заданный период используя табличные значения притока и расхода";
            this.isAutoCalculate.UseVisualStyleBackColor = true;
            this.isAutoCalculate.CheckedChanged += new System.EventHandler(this.isAutoCalculate_CheckedChanged);
            // 
            // SecondTaskControlPanel
            // 
            this.SecondTaskControlPanel.Controls.Add(this.EndCalculate2Task);
            this.SecondTaskControlPanel.Controls.Add(this.StartCalculate2Task);
            this.SecondTaskControlPanel.Controls.Add(this.label5);
            this.SecondTaskControlPanel.Controls.Add(this.label4);
            this.SecondTaskControlPanel.Location = new System.Drawing.Point(226, 12);
            this.SecondTaskControlPanel.Name = "SecondTaskControlPanel";
            this.SecondTaskControlPanel.Size = new System.Drawing.Size(579, 273);
            this.SecondTaskControlPanel.TabIndex = 0;
            // 
            // EndCalculate2Task
            // 
            this.EndCalculate2Task.Location = new System.Drawing.Point(191, 32);
            this.EndCalculate2Task.Name = "EndCalculate2Task";
            this.EndCalculate2Task.ShowTodayCircle = false;
            this.EndCalculate2Task.TabIndex = 3;
            // 
            // StartCalculate2Task
            // 
            this.StartCalculate2Task.Location = new System.Drawing.Point(9, 32);
            this.StartCalculate2Task.Name = "StartCalculate2Task";
            this.StartCalculate2Task.ShowTodayCircle = false;
            this.StartCalculate2Task.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(188, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Конец вычислений";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Старт вычислений";
            // 
            // AcceptOptionsButton
            // 
            this.AcceptOptionsButton.Location = new System.Drawing.Point(435, 291);
            this.AcceptOptionsButton.Name = "AcceptOptionsButton";
            this.AcceptOptionsButton.Size = new System.Drawing.Size(75, 23);
            this.AcceptOptionsButton.TabIndex = 3;
            this.AcceptOptionsButton.Text = "Сохранить";
            this.AcceptOptionsButton.UseVisualStyleBackColor = true;
            this.AcceptOptionsButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // CancelOptionsButton
            // 
            this.CancelOptionsButton.Location = new System.Drawing.Point(516, 291);
            this.CancelOptionsButton.Name = "CancelOptionsButton";
            this.CancelOptionsButton.Size = new System.Drawing.Size(75, 23);
            this.CancelOptionsButton.TabIndex = 4;
            this.CancelOptionsButton.Text = "Отмена";
            this.CancelOptionsButton.UseVisualStyleBackColor = true;
            this.CancelOptionsButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // CurrentTaskTreeView
            // 
            this.CurrentTaskTreeView.Location = new System.Drawing.Point(12, 12);
            this.CurrentTaskTreeView.Name = "CurrentTaskTreeView";
            treeNode1.Name = "Node0";
            treeNode1.Text = "Задача 1 - Задача расчета водно-энергетического режима ГЭС.";
            treeNode2.Name = "Node1";
            treeNode2.Text = "Задача 2 - Задача расчета допустимого режима ГЭС при пропуске половодья.";
            this.CurrentTaskTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.CurrentTaskTreeView.Size = new System.Drawing.Size(208, 298);
            this.CurrentTaskTreeView.TabIndex = 5;
            this.CurrentTaskTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.CurrentTaskTreeView_NodeMouseClick);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 322);
            this.Controls.Add(this.CurrentTaskTreeView);
            this.Controls.Add(this.CancelOptionsButton);
            this.Controls.Add(this.FirstTaskControlPanel);
            this.Controls.Add(this.AcceptOptionsButton);
            this.Controls.Add(this.SecondTaskControlPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "OptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.FirstTaskControlPanel.ResumeLayout(false);
            this.FirstTaskControlPanel.PerformLayout();
            this.SecondTaskControlPanel.ResumeLayout(false);
            this.SecondTaskControlPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel FirstTaskControlPanel;
        private System.Windows.Forms.Panel SecondTaskControlPanel;
        private System.Windows.Forms.MonthCalendar EndCalculate1Task;
        private System.Windows.Forms.MonthCalendar StartCalculate1Task;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox isAutoCalculate;
        private System.Windows.Forms.MonthCalendar EndCalculate2Task;
        private System.Windows.Forms.MonthCalendar StartCalculate2Task;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button AcceptOptionsButton;
        private System.Windows.Forms.Button CancelOptionsButton;
        private System.Windows.Forms.TreeView CurrentTaskTreeView;
    }
}