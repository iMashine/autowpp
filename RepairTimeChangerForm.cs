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
    /// Форма для работы с изменением времени ремонта
    /// </summary>
    public partial class RepairTimeChangerForm : Form
    {

        private int gNumber;

        private int indexOfAggregate = 0;

        /// <summary>
        /// Номер га
        /// </summary>
        public int GNumber
        {
            get { return gNumber; }
            set { gNumber = value; }
        }

        /// <summary>
        /// Базовый конструктор
        /// </summary>
        public RepairTimeChangerForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод для поиска индекса текущего га в массиве
        /// </summary>
        private void FindIndex()
        {
            for (int i = 0; i < HydroAggregates.Aggregates.Count; i++)
            {
                if (gNumber == HydroAggregates.Aggregates[i].Number)
                {
                    indexOfAggregate = i;
                }
            }
        }

        /// <summary>
        /// Метод для инициализации списка ремонтов
        /// </summary>
        private void InitializeRepairsListBox()
        {
            RepairsListBox.Items.Clear();
            for (int i = 0; i < HydroAggregates.Aggregates[indexOfAggregate].Repairs.Count; i++)
            {
                RepairsListBox.Items.Add(HydroAggregates.Aggregates[indexOfAggregate].Repairs[i].Id);
            }
        }

        /// <summary>
        /// Метод для установки номера текущего га
        /// </summary>
        /// <param name="gNumber"> Номер га </param>
        public void SetGNumber(int gNumber) 
        {
            this.gNumber = gNumber;
            this.Text = "QGA" + gNumber.ToString();
            FindIndex();
            UpdateListBox();
        }

        /// <summary>
        /// Метод для обновления списка ремонтов
        /// </summary>
        private void UpdateListBox()
        {
            InitializeRepairsListBox();
            HydroAggregates.WriteToFile();
        }

        /// <summary>
        /// Делегат для обработки события нажатия кнопки добавления ремонта
        /// </summary>
        private void AddRepairButton_Click(object sender, EventArgs e)
        {
            HydroAggregates.Aggregates[indexOfAggregate].AddRepair(
                new Repair(StartRepairCalendar.SelectionStart, EndRepairCalendar.SelectionStart));
            UpdateListBox();
        }

        /// <summary>
        /// Делегат для обработки события нажатия кнопки удаленияы ремонта
        /// </summary>
        private void RemoveRepairButton_Click(object sender, EventArgs e)
        {
            HydroAggregates.Aggregates[indexOfAggregate].RemoveRepair(int.Parse(RepairsListBox.SelectedItem.ToString()));
            UpdateListBox();
        }

        /// <summary>
        /// Делегат для обработки события нажатия на определенный объект ремонта в списке
        /// </summary>
        private void RepairsListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < HydroAggregates.Aggregates[indexOfAggregate].Repairs.Count; i++)
            {
                if (RepairsListBox.SelectedItem.ToString() == 
                    HydroAggregates.Aggregates[indexOfAggregate].Repairs[i].Id.ToString())
                {
                    StartRepairCalendar.SelectionStart = HydroAggregates.Aggregates[indexOfAggregate].Repairs[i].StartDate;
                    EndRepairCalendar.SelectionStart = HydroAggregates.Aggregates[indexOfAggregate].Repairs[i].EndDate;
                }
            }
        }
    }
}
