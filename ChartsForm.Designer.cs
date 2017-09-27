namespace autowpp
{
    partial class ChartsForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.ChartPanelForFirstTask = new System.Windows.Forms.Panel();
            this.removeChartForFirstTask = new System.Windows.Forms.Button();
            this.addChartForFirstTask = new System.Windows.Forms.Button();
            this.currentChartForFirstTask = new System.Windows.Forms.ComboBox();
            this.ChartPanelForSecondTask = new System.Windows.Forms.Panel();
            this.removeChartForSecondTask2 = new System.Windows.Forms.Button();
            this.addChartForSecondTask2 = new System.Windows.Forms.Button();
            this.currentChartForSecondTask2 = new System.Windows.Forms.ComboBox();
            this.removeChartForSecondTask = new System.Windows.Forms.Button();
            this.addChartForSecondTask = new System.Windows.Forms.Button();
            this.currentChartForSecondTask = new System.Windows.Forms.ComboBox();
            this.MainChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ChartPanelForFirstTask.SuspendLayout();
            this.ChartPanelForSecondTask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainChart)).BeginInit();
            this.SuspendLayout();
            // 
            // ChartPanelForFirstTask
            // 
            this.ChartPanelForFirstTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChartPanelForFirstTask.Controls.Add(this.removeChartForFirstTask);
            this.ChartPanelForFirstTask.Controls.Add(this.addChartForFirstTask);
            this.ChartPanelForFirstTask.Controls.Add(this.currentChartForFirstTask);
            this.ChartPanelForFirstTask.Location = new System.Drawing.Point(12, 376);
            this.ChartPanelForFirstTask.Name = "ChartPanelForFirstTask";
            this.ChartPanelForFirstTask.Size = new System.Drawing.Size(734, 36);
            this.ChartPanelForFirstTask.TabIndex = 0;
            // 
            // removeChartForFirstTask
            // 
            this.removeChartForFirstTask.Location = new System.Drawing.Point(213, 3);
            this.removeChartForFirstTask.Name = "removeChartForFirstTask";
            this.removeChartForFirstTask.Size = new System.Drawing.Size(75, 23);
            this.removeChartForFirstTask.TabIndex = 3;
            this.removeChartForFirstTask.Text = "Убрать";
            this.removeChartForFirstTask.UseVisualStyleBackColor = true;
            this.removeChartForFirstTask.Click += new System.EventHandler(this.removeChartForFirstTask_Click);
            // 
            // addChartForFirstTask
            // 
            this.addChartForFirstTask.Location = new System.Drawing.Point(131, 3);
            this.addChartForFirstTask.Name = "addChartForFirstTask";
            this.addChartForFirstTask.Size = new System.Drawing.Size(75, 23);
            this.addChartForFirstTask.TabIndex = 2;
            this.addChartForFirstTask.Text = "Добавить";
            this.addChartForFirstTask.UseVisualStyleBackColor = true;
            this.addChartForFirstTask.Click += new System.EventHandler(this.addChartForFirstTask_Click);
            // 
            // currentChartForFirstTask
            // 
            this.currentChartForFirstTask.FormattingEnabled = true;
            this.currentChartForFirstTask.Location = new System.Drawing.Point(3, 3);
            this.currentChartForFirstTask.Name = "currentChartForFirstTask";
            this.currentChartForFirstTask.Size = new System.Drawing.Size(121, 21);
            this.currentChartForFirstTask.TabIndex = 1;
            // 
            // ChartPanelForSecondTask
            // 
            this.ChartPanelForSecondTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChartPanelForSecondTask.Controls.Add(this.removeChartForSecondTask2);
            this.ChartPanelForSecondTask.Controls.Add(this.addChartForSecondTask2);
            this.ChartPanelForSecondTask.Controls.Add(this.currentChartForSecondTask2);
            this.ChartPanelForSecondTask.Controls.Add(this.removeChartForSecondTask);
            this.ChartPanelForSecondTask.Controls.Add(this.addChartForSecondTask);
            this.ChartPanelForSecondTask.Controls.Add(this.currentChartForSecondTask);
            this.ChartPanelForSecondTask.Location = new System.Drawing.Point(12, 376);
            this.ChartPanelForSecondTask.Name = "ChartPanelForSecondTask";
            this.ChartPanelForSecondTask.Size = new System.Drawing.Size(734, 36);
            this.ChartPanelForSecondTask.TabIndex = 0;
            // 
            // removeChartForSecondTask2
            // 
            this.removeChartForSecondTask2.Location = new System.Drawing.Point(500, 3);
            this.removeChartForSecondTask2.Name = "removeChartForSecondTask2";
            this.removeChartForSecondTask2.Size = new System.Drawing.Size(75, 23);
            this.removeChartForSecondTask2.TabIndex = 5;
            this.removeChartForSecondTask2.Text = "Убрать";
            this.removeChartForSecondTask2.UseVisualStyleBackColor = true;
            this.removeChartForSecondTask2.Click += new System.EventHandler(this.removeChartForSecondTask2_Click);
            // 
            // addChartForSecondTask2
            // 
            this.addChartForSecondTask2.Location = new System.Drawing.Point(419, 3);
            this.addChartForSecondTask2.Name = "addChartForSecondTask2";
            this.addChartForSecondTask2.Size = new System.Drawing.Size(75, 23);
            this.addChartForSecondTask2.TabIndex = 4;
            this.addChartForSecondTask2.Text = "Добавить";
            this.addChartForSecondTask2.UseVisualStyleBackColor = true;
            this.addChartForSecondTask2.Click += new System.EventHandler(this.addChartForSecondTask2_Click);
            // 
            // currentChartForSecondTask2
            // 
            this.currentChartForSecondTask2.FormattingEnabled = true;
            this.currentChartForSecondTask2.Location = new System.Drawing.Point(292, 3);
            this.currentChartForSecondTask2.Name = "currentChartForSecondTask2";
            this.currentChartForSecondTask2.Size = new System.Drawing.Size(121, 21);
            this.currentChartForSecondTask2.TabIndex = 3;
            // 
            // removeChartForSecondTask
            // 
            this.removeChartForSecondTask.Location = new System.Drawing.Point(211, 3);
            this.removeChartForSecondTask.Name = "removeChartForSecondTask";
            this.removeChartForSecondTask.Size = new System.Drawing.Size(75, 23);
            this.removeChartForSecondTask.TabIndex = 2;
            this.removeChartForSecondTask.Text = "Убрать";
            this.removeChartForSecondTask.UseVisualStyleBackColor = true;
            this.removeChartForSecondTask.Click += new System.EventHandler(this.removeChartForSecondTask_Click);
            // 
            // addChartForSecondTask
            // 
            this.addChartForSecondTask.Location = new System.Drawing.Point(130, 3);
            this.addChartForSecondTask.Name = "addChartForSecondTask";
            this.addChartForSecondTask.Size = new System.Drawing.Size(75, 23);
            this.addChartForSecondTask.TabIndex = 1;
            this.addChartForSecondTask.Text = "Добавить";
            this.addChartForSecondTask.UseVisualStyleBackColor = true;
            this.addChartForSecondTask.Click += new System.EventHandler(this.addChartForSecondTask_Click);
            // 
            // currentChartForSecondTask
            // 
            this.currentChartForSecondTask.FormattingEnabled = true;
            this.currentChartForSecondTask.Location = new System.Drawing.Point(3, 3);
            this.currentChartForSecondTask.Name = "currentChartForSecondTask";
            this.currentChartForSecondTask.Size = new System.Drawing.Size(121, 21);
            this.currentChartForSecondTask.TabIndex = 0;
            // 
            // MainChart
            // 
            this.MainChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainChart.BorderlineColor = System.Drawing.Color.Maroon;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.CursorY.IsUserSelectionEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.MainChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.MainChart.Legends.Add(legend1);
            this.MainChart.Location = new System.Drawing.Point(12, 12);
            this.MainChart.Name = "MainChart";
            this.MainChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.MainChart.Series.Add(series1);
            this.MainChart.Size = new System.Drawing.Size(734, 355);
            this.MainChart.TabIndex = 1;
            this.MainChart.Text = "chart1";
            // 
            // ChartsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 424);
            this.Controls.Add(this.MainChart);
            this.Controls.Add(this.ChartPanelForFirstTask);
            this.Controls.Add(this.ChartPanelForSecondTask);
            this.Name = "ChartsForm";
            this.Text = "Графическое отображение данных";
            this.ChartPanelForFirstTask.ResumeLayout(false);
            this.ChartPanelForSecondTask.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ChartPanelForFirstTask;
        private System.Windows.Forms.Panel ChartPanelForSecondTask;
        private System.Windows.Forms.Button removeChartForFirstTask;
        private System.Windows.Forms.Button addChartForFirstTask;
        private System.Windows.Forms.ComboBox currentChartForFirstTask;
        private System.Windows.Forms.Button removeChartForSecondTask2;
        private System.Windows.Forms.Button addChartForSecondTask2;
        private System.Windows.Forms.ComboBox currentChartForSecondTask2;
        private System.Windows.Forms.Button removeChartForSecondTask;
        private System.Windows.Forms.Button addChartForSecondTask;
        private System.Windows.Forms.ComboBox currentChartForSecondTask;
        private System.Windows.Forms.DataVisualization.Charting.Chart MainChart;
    }
}