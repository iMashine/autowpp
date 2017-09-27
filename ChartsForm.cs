using Newtonsoft.Json.Linq;
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
    /// Форма для графического отображения вычислений
    /// </summary>
    public partial class ChartsForm : Form
    {
        #region Объявление переменных 

        /// <summary>
        /// Первый массив с данными
        /// </summary>
        private JArray m_data;

        /// <summary>
        /// Второй массив с данными
        /// </summary>
        private JArray m_data2;

        /// <summary>
        /// Перечислитель для выбора режима работы
        /// </summary>
        private enum WorkMode
        {
            FirstTask = 0,
            SecondTask = 1
        };

        /// <summary>
        /// Текущий режим работы
        /// </summary>
        private WorkMode m_currentWorkmode;

        private System.Windows.Forms.DataVisualization.Charting.SeriesChartType m_currentChartType = 
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;

        #endregion

        /// <summary>
        /// Конструктор для инициализации формы 
        /// графического отображения для первой задачи
        /// </summary>
        /// <param name="data"> Массив с данными для отображения </param>
        public ChartsForm(JArray data)
        {
            InitializeComponent();
            m_currentWorkmode = WorkMode.FirstTask;
            m_data = JArray.Parse(data.ToString());
            ChartPanelForSecondTask.Visible = false;
            ChartPanelForFirstTask.Visible = true;
            InitializeControls();
        }

        /// <summary>
        /// Конструктор для инициализации формы 
        /// графического отображения для второй задачи
        /// </summary>
        /// <param name="oldData"> Массив с исходными данными для отображения </param>
        /// <param name="newData"> Массив с перерасчитанными данными для отображения </param>
        public ChartsForm(JArray oldData, JArray newData)
        {
            InitializeComponent();
            m_currentWorkmode = WorkMode.SecondTask;
            m_data = JArray.Parse(oldData.ToString());
            m_data2 = JArray.Parse(newData.ToString());
            ChartPanelForSecondTask.Visible = true;
            ChartPanelForFirstTask.Visible = false;
            InitializeControls();
        }

        private void InitializeControls()
        {
            InitializeMainChart();
            InitializeCurrentDataComboBox();
        }

        /// <summary>
        /// Метод инициализации контроллера выпадающего списка с данными для вывода
        /// </summary>
        private void InitializeCurrentDataComboBox()
        {
            if (m_currentWorkmode == WorkMode.FirstTask)
            {
                currentChartForFirstTask.Items.Clear();

                string[] ColumnNames = {
                                       "УВБ, м",  // ручной ввод
                                       "УНБ, м",  // ручной ввод
                                       "ΣQНБ, м3/с",  // ручной ввод
                                       "Объем водохр, млн. м3", 
                                       "Приток, м3/с", // ручной ввод
                                   };

                for (int i = 0; i < ColumnNames.Length; i++)
                {
                    currentChartForFirstTask.Items.Add(ColumnNames[i]);
                }
            }
            else
            {
                currentChartForSecondTask.Items.Clear();

                string[] ColumnNames = {
                                       "УВБ, м",  // ручной ввод
                                       "УНБ, м",  // ручной ввод
                                       "ΣQНБ, м3/с",  // ручной ввод
                                       "Объем водохр, млн. м3", 
                                       "Приток, м3/с", // ручной ввод
                                   };

                for (int i = 0; i < ColumnNames.Length; i++)
                {
                    currentChartForSecondTask.Items.Add(ColumnNames[i]);
                    currentChartForSecondTask2.Items.Add(ColumnNames[i] + "_2");
                }
            }
        }

        /// <summary>
        /// Метод инициализации контроллера графика
        /// </summary>
        private void InitializeMainChart()
        {
            MainChart.Series.Clear();
        }

        /// <summary>
        /// Метод для проверки существующего графика 
        /// </summary>
        /// <param name="seriesName"> Название добавляемого графика </param>
        /// <returns> Признак существования графика </returns>
        bool Contains(string seriesName)
        {
            bool isHave = false;
            for (int i = 0; i < MainChart.Series.Count; i++)
            {
                if (MainChart.Series[i].Name == seriesName)
                {
                    isHave = true;
                    break;
                }
            }
            return isHave;
        }

        #region Делегаты кнопок обработки событий для второй задачи

        /// <summary>
        /// Делегат для обработки события удаления графика
        /// Вторая задача, новые данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeChartForSecondTask2_Click(object sender, EventArgs e)
        {
            if (currentChartForSecondTask2.Text != "")
            {
                int index = -1;
                for (int i = 0; i < MainChart.Series.Count; i++)
                {
                    if (MainChart.Series[i].Name == currentChartForSecondTask2.Text)
                    {
                        index = i;
                        break;
                    }
                }
                if (index != -1)
                    MainChart.Series.RemoveAt(index);
            }
            else
            {
                MessageBox.Show("Выберите столбец!", "Внимание!");
            }
        }

        /// <summary>
        /// Делегат для обработки события добавление графика
        /// Вторая задача, новые данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addChartForSecondTask2_Click(object sender, EventArgs e)
        {
            if (currentChartForSecondTask2.Text != "")
            {
                if (!Contains(currentChartForSecondTask2.Text))
                {
                    string key = "";
                    for (int i = 0; i < currentChartForSecondTask2.Text.Length - 2; i++)
                    {
                        key += currentChartForSecondTask2.Text[i];
                    }
                    string keyForDataArray = autowpp.Data.GlobalNamesToKeys[key];
                    MainChart.Series.Add(currentChartForSecondTask2.Text);
                    MainChart.Series[currentChartForSecondTask2.Text].ChartType = m_currentChartType;

                    for (int i = 0; i < m_data2.Count; i++)
                    {
                        MainChart.Series[currentChartForSecondTask2.Text].Points.AddXY(
                              DateTime.Parse(m_data2[i]["date"].ToString()),
                            double.Parse(m_data2[i][keyForDataArray].ToString()));
                    }
                }
                else
                {
                    MessageBox.Show("Данная зависимость уже добавлена!", "Внимание!");
                }
            }
            else
            {
                MessageBox.Show("Выберите столбец!", "Внимание!");
            }
        }

        /// <summary>
        /// Делегат для обработки события удаления графика
        /// Вторая задача, исходные данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeChartForSecondTask_Click(object sender, EventArgs e)
        {
            if (currentChartForSecondTask.Text != "")
            {
                int index = -1;
                for (int i = 0; i < MainChart.Series.Count; i++)
                {
                    if (MainChart.Series[i].Name == currentChartForSecondTask.Text)
                    {
                        index = i;
                        break;
                    }
                }
                if (index != -1)
                    MainChart.Series.RemoveAt(index);
            }
            else
            {
                MessageBox.Show("Выберите столбец!", "Внимание!");
            }
        }

        /// <summary>
        /// Делегат для обработки события добавление графика
        /// Вторая задача, исходные данные 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addChartForSecondTask_Click(object sender, EventArgs e)
        {
            if (currentChartForSecondTask.Text != "")
            {
                if (!Contains(currentChartForSecondTask.Text))
                {
                    string keyForDataArray = autowpp.Data.GlobalNamesToKeys[currentChartForSecondTask.Text];
                    MainChart.Series.Add(currentChartForSecondTask.Text);
                    MainChart.Series[currentChartForSecondTask.Text].ChartType = m_currentChartType;

                    for (int i = 0; i < m_data.Count; i++)
                    {
                        MainChart.Series[currentChartForSecondTask.Text].Points.AddXY(
                            DateTime.Parse(m_data[i]["date"].ToString()),
                            double.Parse(m_data[i][keyForDataArray].ToString()));
                    }
                }
                else
                {
                    MessageBox.Show("Данная зависимость уже добавлена!", "Внимание!");
                }
            }
            else
            {
                MessageBox.Show("Выберите столбец!", "Внимание!");
            }
        }

        #endregion

        #region Делегаты кнопок обработки событий для первой задачи

        /// <summary>
        /// Делегат для обработки события добавление графика первой задачи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addChartForFirstTask_Click(object sender, EventArgs e)
        {
            if (currentChartForFirstTask.Text != "") 
            {
                if (!Contains(currentChartForFirstTask.Text))
                {
                    string keyName = autowpp.Data.GlobalNamesToKeys[currentChartForFirstTask.Text];
                    MainChart.Series.Add(currentChartForFirstTask.Text);
                    MainChart.Series[currentChartForFirstTask.Text].ChartType = m_currentChartType;

                    for (int i = 0; i < m_data.Count; i++)
                    {
                        MainChart.Series[currentChartForFirstTask.Text].Points.AddXY(
                            DateTime.Parse(m_data[i]["date"].ToString()),
                            double.Parse(m_data[i][keyName].ToString()));
                    }
                }
                else
                {
                    MessageBox.Show("Данная зависимость уже добавлена!", "Внимание!");
                }
            }
            else
            {
                MessageBox.Show("Выберите столбец!", "Внимание!");
            }
        }

        /// <summary>
        /// Делегат для обработки события удаления графика первой задачи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeChartForFirstTask_Click(object sender, EventArgs e)
        {
            if (currentChartForFirstTask.Text != "")
            {
                int index = -1;
                for (int i = 0; i < MainChart.Series.Count; i++)
                {
                    if (MainChart.Series[i].Name == currentChartForFirstTask.Text)
                    {
                        index = i;
                        break;
                    }
                }
                if (index != -1)
                    MainChart.Series.RemoveAt(index);
            }
            else
            {
                MessageBox.Show("Выберите столбец!", "Внимание!");
            }
        }

        #endregion
    }
}
