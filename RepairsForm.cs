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
    /// Класс для работы с ремонтами га
    /// </summary>
    public partial class RepairsForm : Form
    {
        /// <summary>
        /// Базовый конструктор
        /// </summary>
        public RepairsForm()
        {
            InitializeComponent();
            QG1Button.MouseClick += QGButton_MouseClick;
            QG2Button.MouseClick += QGButton_MouseClick;
            QG3Button.MouseClick += QGButton_MouseClick;
            QG4Button.MouseClick += QGButton_MouseClick;
            QG5Button.MouseClick += QGButton_MouseClick;
            QG6Button.MouseClick += QGButton_MouseClick;
            QG7Button.MouseClick += QGButton_MouseClick;
            ShowRepairs();
        }

        /// <summary>
        /// Метод для отображения состояние ремонтов на га
        /// </summary>
        private void ShowRepairs()
        {
            Dictionary<int, bool> repairs = Repairs.GetRepairsForDate(MCalendar.SelectionStart);
            QG1CheckBox.Checked = false;
            QG2CheckBox.Checked = false;
            QG3CheckBox.Checked = false;
            QG4CheckBox.Checked = false;
            QG5CheckBox.Checked = false;
            QG6CheckBox.Checked = false;
            QG7CheckBox.Checked = false;
            if (repairs.ContainsKey(1))
            {
                QG1CheckBox.Checked = true;
            }
            if (repairs.ContainsKey(2))
            {
                QG2CheckBox.Checked = true;
            }
            if (repairs.ContainsKey(3))
            {
                QG3CheckBox.Checked = true;
            }
            if (repairs.ContainsKey(4))
            {
                QG4CheckBox.Checked = true;
            }
            if (repairs.ContainsKey(5))
            {
                QG5CheckBox.Checked = true;
            }
            if (repairs.ContainsKey(6))
            {
                QG6CheckBox.Checked = true;
            }
            if (repairs.ContainsKey(7))
            {
                QG7CheckBox.Checked = true;
            }
        }

        /// <summary>
        /// Делегат для обработки события нажатия кнопки
        /// </summary>
        private void QGButton_MouseClick(object sender, MouseEventArgs e)
        {
            RepairTimeChangerForm rtcf = new RepairTimeChangerForm();
            switch (((Button)sender).Name)
            {
                case "QG1Button":
                    rtcf.SetGNumber(1);
                    break;
                case "QG2Button":
                    rtcf.SetGNumber(2);
                    break;
                case "QG3Button":
                    rtcf.SetGNumber(3);
                    break;
                case "QG4Button":
                    rtcf.SetGNumber(4);
                    break;
                case "QG5Button":
                    rtcf.SetGNumber(5);
                    break;
                case "QG6Button":
                    rtcf.SetGNumber(6);
                    break;
                case "QG7Button":
                    rtcf.SetGNumber(7);
                    break;
            }
            rtcf.Show();
        }

        /// <summary>
        /// Делегат для обработки события изменения даты в календаре
        /// </summary>
        private void MCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            ShowRepairs();
        }
    }
}
