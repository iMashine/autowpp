using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace autowpp
{
    /// <summary>
    /// Форма для работы с глобальными настройками приложения
    /// </summary>
    public partial class OptionsForm : Form
    {
        /// <summary>
        /// Базовый конструктор
        /// </summary>
        public OptionsForm()
        {
            InitializeComponent();
            FirstTaskControlPanel.Enabled = FirstTaskControlPanel.Visible = false;
            SecondTaskControlPanel.Enabled = SecondTaskControlPanel.Visible = false;
            StartCalculate1Task.Enabled = EndCalculate1Task.Enabled = false;

            StartCalculate1Task.MaxSelectionCount = EndCalculate1Task.MaxSelectionCount = 1;
            StartCalculate2Task.MaxSelectionCount = EndCalculate2Task.MaxSelectionCount = 1;
        }

        /// <summary>
        /// Делегат для обработки события выбора флага контроллера для установки автоматического расчета первой задачи
        /// </summary>
        private void isAutoCalculate_CheckedChanged(object sender, EventArgs e)
        {
            if (isAutoCalculate.Checked == true)
            {
                StartCalculate1Task.Enabled = EndCalculate1Task.Enabled = true;
            }
            else
            {
                StartCalculate1Task.Enabled = EndCalculate1Task.Enabled = false;
            }
        }

        /// <summary>
        /// Делегат для обработки события нажатия кнопки отмены
        /// </summary>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Делегат для обработки события нажатия кнопки применить
        /// </summary>
        private void AcceptButton_Click(object sender, EventArgs e)
        {
            if (CurrentTaskTreeView.SelectedNode.Text == GlobalOptions.FirstTask.Name)
            {
                GlobalOptions.FirstTask.isAutoCalculate = isAutoCalculate.Checked;
                if (GlobalOptions.FirstTask.isAutoCalculate)
                {
                    GlobalOptions.FirstTask.StartCalculate = StartCalculate1Task.SelectionStart;
                    GlobalOptions.FirstTask.EndCalculate = EndCalculate1Task.SelectionStart;
                }
            }
            else if (CurrentTaskTreeView.SelectedNode.Text == GlobalOptions.SecondTask.Name)
            {
                GlobalOptions.SecondTask.StartCalculate = StartCalculate2Task.SelectionStart;
                GlobalOptions.SecondTask.EndCalculate = EndCalculate2Task.SelectionStart;
            }
            GlobalOptions.SaveToFile();
            GlobalOptions.ReadFromFile();
            this.Close();
        }

        /// <summary>
        /// Делегат для обработки события выбора итема контроллера дерева задач для настройки
        /// </summary>
        private void CurrentTaskTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Text == GlobalOptions.FirstTask.Name)
            {
                FirstTaskControlPanel.Enabled = FirstTaskControlPanel.Visible = true;
                isAutoCalculate.Checked = GlobalOptions.FirstTask.isAutoCalculate;
                if (isAutoCalculate.Checked)
                {
                    StartCalculate1Task.SelectionStart = GlobalOptions.FirstTask.StartCalculate;
                    EndCalculate1Task.SelectionStart = GlobalOptions.FirstTask.EndCalculate;
                }
                SecondTaskControlPanel.Enabled = SecondTaskControlPanel.Visible = false;
            }
            else
            {
                FirstTaskControlPanel.Enabled = FirstTaskControlPanel.Visible = false;
                SecondTaskControlPanel.Enabled = SecondTaskControlPanel.Visible = true;
                StartCalculate2Task.SelectionStart = GlobalOptions.SecondTask.StartCalculate;
                EndCalculate2Task.SelectionStart = GlobalOptions.SecondTask.EndCalculate;
            }
        }
    }
}
