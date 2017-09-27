namespace autowpp
{
    partial class BoundsForm
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
            this.SaveBoundsButton = new System.Windows.Forms.Button();
            this.DeleteBoundButton = new System.Windows.Forms.Button();
            this.BoundsDataGridView = new System.Windows.Forms.DataGridView();
            this.BoundIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoundTypeColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.LeftBoundColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RightBoundColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.BoundsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveBoundsButton
            // 
            this.SaveBoundsButton.Location = new System.Drawing.Point(660, 12);
            this.SaveBoundsButton.Name = "SaveBoundsButton";
            this.SaveBoundsButton.Size = new System.Drawing.Size(80, 23);
            this.SaveBoundsButton.TabIndex = 2;
            this.SaveBoundsButton.Text = "Сохранить";
            this.SaveBoundsButton.UseVisualStyleBackColor = true;
            this.SaveBoundsButton.Click += new System.EventHandler(this.SaveBoundsButton_Click);
            // 
            // DeleteBoundButton
            // 
            this.DeleteBoundButton.Location = new System.Drawing.Point(660, 41);
            this.DeleteBoundButton.Name = "DeleteBoundButton";
            this.DeleteBoundButton.Size = new System.Drawing.Size(80, 23);
            this.DeleteBoundButton.TabIndex = 4;
            this.DeleteBoundButton.Text = "Удалить";
            this.DeleteBoundButton.UseVisualStyleBackColor = true;
            this.DeleteBoundButton.Click += new System.EventHandler(this.DeleteBoundButton_Click);
            // 
            // BoundsDataGridView
            // 
            this.BoundsDataGridView.AllowUserToAddRows = false;
            this.BoundsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BoundsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BoundIdColumn,
            this.BoundTypeColumn,
            this.LeftBoundColumn,
            this.RightBoundColumn});
            this.BoundsDataGridView.Location = new System.Drawing.Point(12, 12);
            this.BoundsDataGridView.MultiSelect = false;
            this.BoundsDataGridView.Name = "BoundsDataGridView";
            this.BoundsDataGridView.Size = new System.Drawing.Size(642, 325);
            this.BoundsDataGridView.TabIndex = 5;
            this.BoundsDataGridView.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.BoundsDataGridView_RowHeaderMouseDoubleClick);
            // 
            // BoundIdColumn
            // 
            this.BoundIdColumn.HeaderText = "Id ограничения";
            this.BoundIdColumn.Name = "BoundIdColumn";
            this.BoundIdColumn.ReadOnly = true;
            // 
            // BoundTypeColumn
            // 
            this.BoundTypeColumn.HeaderText = "Тип ограничения";
            this.BoundTypeColumn.Name = "BoundTypeColumn";
            // 
            // LeftBoundColumn
            // 
            this.LeftBoundColumn.HeaderText = "Левая граница";
            this.LeftBoundColumn.Name = "LeftBoundColumn";
            // 
            // RightBoundColumn
            // 
            this.RightBoundColumn.HeaderText = "Правая граница";
            this.RightBoundColumn.Name = "RightBoundColumn";
            // 
            // BoundsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 349);
            this.Controls.Add(this.BoundsDataGridView);
            this.Controls.Add(this.DeleteBoundButton);
            this.Controls.Add(this.SaveBoundsButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "BoundsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактор ограничений";
            ((System.ComponentModel.ISupportInitialize)(this.BoundsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SaveBoundsButton;
        private System.Windows.Forms.Button DeleteBoundButton;
        private System.Windows.Forms.DataGridView BoundsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn BoundIdColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn BoundTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeftBoundColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RightBoundColumn;
    }
}