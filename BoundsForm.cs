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
    /// Класс формы для работы с ограничениями
    /// </summary>
    public partial class BoundsForm : Form
    {
        /// <summary>
        /// Индекс текущей строки
        /// </summary>
        private int m_currentRow = 0;

        /// <summary>
        /// Базовый конструктор
        /// </summary>
        public BoundsForm()
        {
            InitializeComponent();
            FillDataGridView();
        }

        /// <summary>
        /// Заполнение таблицы данными по ограничения
        /// </summary>
        private void FillDataGridView()
        {
            BoundsDataGridView.Rows.Clear();
            if (autowpp.Bounds.GetBounds().Count == 0)
            {
                AddRowToBoundsDataGridView();
            }
            else
            {
                for (int i = 0; i < autowpp.Bounds.GetBounds().Count; i++)
                {
                    AddRowToBoundsDataGridView(autowpp.Bounds.GetBounds()[i]);
                }
            }
        }

        /// <summary>
        /// Метод для создания новой строки в таблице
        /// </summary>
        private void AddRowToBoundsDataGridView(Bound addedBound = null)
        {
            DataGridViewRow newRow = new DataGridViewRow();
            DataGridViewTextBoxCell idCell, leftBCell, rightBCell;
            idCell = new DataGridViewTextBoxCell();
            DataGridViewComboBoxCell typeCell = new DataGridViewComboBoxCell();
            int index = 0;
            for (int i = 0; i < Data.ColumnNames.Length; i++)
            {
                if (addedBound != null && addedBound.Type == Data.GlobalNamesToKeys[Data.ColumnNames[i]])
                {
                    index = i;
                }
                typeCell.Items.Add(Data.ColumnNames[i]);
            }  
            leftBCell = new DataGridViewTextBoxCell();
            rightBCell = new DataGridViewTextBoxCell();
            if (addedBound != null)
            {
                idCell.Value = addedBound.Id;
                typeCell.Value = Data.ColumnNames[index];
                leftBCell.Value = addedBound.Left;
                rightBCell.Value = addedBound.Right;
            }
            newRow.Cells.Add(idCell);
            newRow.Cells.Add(typeCell);
            newRow.Cells.Add(leftBCell);
            newRow.Cells.Add(rightBCell);
            BoundsDataGridView.Rows.Add(newRow);
        }

        /// <summary>
        /// Делегат обрабатывающий событие по двойному щелчку на заголовке строки
        /// </summary>
        private void BoundsDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            AddRowToBoundsDataGridView();
        }

        /// <summary>
        /// Метод для проверки выделенна ли строка
        /// </summary>
        /// <returns> Результат проверки </returns>
        private bool CheckRow()
        {
            if (BoundsDataGridView.SelectedCells.Count != 0)
            {
                m_currentRow = BoundsDataGridView.SelectedCells[0].RowIndex;
                return true;
            }
            MessageBox.Show("Для манипуляций с данными выберите нужную строку!", "Внимание");
            return false;
        }

        /// <summary>
        /// Делегат обрабатывающий событие с кнопки сохранения
        /// </summary>
        private void SaveBoundsButton_Click(object sender, EventArgs e)
        {
            if (CheckRow())
            {
                Bound newBound = new Bound();
                if (BoundsDataGridView.Rows[m_currentRow].Cells[0].Value != null)
                {
                    newBound.Id = int.Parse(BoundsDataGridView.Rows[m_currentRow].Cells[0].Value.ToString());
                }
                if (BoundsDataGridView.Rows[m_currentRow].Cells[1].Value == null ||
                    BoundsDataGridView.Rows[m_currentRow].Cells[2].Value == null ||
                    BoundsDataGridView.Rows[m_currentRow].Cells[3].Value == null)
                {
                    MessageBox.Show("Заполните данные всех ячеек в строке!", "Ошибка");
                    return;
                }
                newBound.Type = Data.GlobalNamesToKeys[BoundsDataGridView.Rows[m_currentRow].Cells[1].Value.ToString()];
                newBound.Left = double.Parse(BoundsDataGridView.Rows[m_currentRow].Cells[2].Value.ToString());
                newBound.Right = double.Parse(BoundsDataGridView.Rows[m_currentRow].Cells[3].Value.ToString());
                if (newBound.Id != -1 && autowpp.Bounds.Contains(newBound.Id))
                {
                    autowpp.Bounds.EditBound(newBound);
                }
                else
                {
                    autowpp.Bounds.AddBound(newBound);
                }
                autowpp.Bounds.SaveBoundsToFile();
                autowpp.Bounds.OpenBoundsFromFile();
                FillDataGridView();
            }
        }

        /// <summary>
        /// Делегат обрабатывающий событие с кнопки удаления
        /// </summary>
        private void DeleteBoundButton_Click(object sender, EventArgs e)
        {
            if (CheckRow())
            {
                if (BoundsDataGridView.Rows[m_currentRow].Cells[0].Value != null)
                {
                    autowpp.Bounds.RemoveBound(int.Parse(BoundsDataGridView.Rows[m_currentRow].Cells[0].Value.ToString()));
                    FillDataGridView();
                }
                else
                {
                    MessageBox.Show("Невозможно удалить еще не созданное значение!", "Ошибка");
                }
            }
        }
    }
}
