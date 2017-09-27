using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ExcelObj = Microsoft.Office.Interop.Excel;

namespace autowpp
{
    /// <summary>
    /// Класс основной формы прилоежения
    /// </summary>
    public partial class MainForm : Form
    {
        #region Блок объявления переменных

        #region Переменные для работы с Excel

        /// <summary>
        /// Экземпляр приложения Excel
        /// </summary>
        private ExcelObj.Application m_excelApplication;

        /// <summary>
        /// Текущий лист в таблице
        /// </summary>
        private ExcelObj.Workbook m_excelWorkbook;

        /// <summary>
        /// Переменная хранитель ячеек со значениями в таблице
        /// </summary>
        private ExcelObj.Range m_excelSheetRange;

        #endregion

        /// <summary>
        /// Основное контекстное меню формы
        /// </summary>
        private ContextMenuStrip m_contexMenuStrip;

        /// <summary>
        /// Последняя позиция курсора на MDataGridView
        /// </summary>
        private Point m_lastPosOfRightMouseButton;

        /// <summary>
        /// Массив базовых данных для работы
        /// </summary>
        private JArray m_dataFromlPower;

        /// <summary>
        /// Массив данных мощностей для старых га
        /// </summary>
        private JArray m_dataPGAOld;

        /// <summary>
        /// Массив данных мощностей для новых га
        /// </summary>
        private JArray m_dataPGANew;

        /// <summary>
        /// Массив данных по расходам для старых га
        /// </summary>
        private JArray m_dataQGAOld;

        /// <summary>
        /// Массив данных по расходам для новых га
        /// </summary>
        private JArray m_dataQGANew;

        /// <summary>
        /// Массив данных по расходам от мощности и нетто для гэс
        /// </summary>
        private JArray m_dataQ;

        /// <summary>
        /// Указатель на текущую строку
        /// </summary>
        private DataGridViewRow m_currentRow;

        /// <summary>
        /// Список режимов для работы
        /// </summary>
        private enum WorkMode {
            FirstTask = 0,
            SecondTask = 1
        };

        /// <summary>
        /// Текущий режим работы
        /// </summary>
        private WorkMode m_currentWorkmode;

        /// <summary>
        /// Текущая форма графического отображения данных
        /// </summary>
        private ChartsForm m_chartsForm;

        /// <summary>
        /// Словарь соответствий имен столбцов и их заголовков
        /// </summary>
        private Dictionary<string, string> TableColumnsNames = new Dictionary<string, string>();

        /// <summary>
        /// Словарь соответствий заголовков столбцов и их имен
        /// </summary>
        private Dictionary<string, string> TableColumnsHeaders = new Dictionary<string, string>();

        #endregion

        /// <summary>
        /// Конструктор основной формы
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            OpenDataFromJson();
        }

        #region Методы

        /// <summary>
        /// Метод для установки текущего режима работы приложения
        /// </summary>
        /// <param name="mode"> Режим для установки </param>
        private void SetWorkMode(WorkMode mode)
        {
            m_currentWorkmode = mode;
        }

        #region Методы для работы с excel и json

        /// <summary>
        /// Метод для открытия рабочей книги Excel с данными
        /// </summary>
        private void OpenDataFromExcelWorkBook()
        {
            m_excelApplication = new ExcelObj.Application();

            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    m_excelWorkbook = m_excelApplication.Workbooks.Open(ofd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Файл не был открыт!", "Внимание!");

                    m_excelWorkbook.Saved = false;
                    m_excelApplication.DisplayAlerts = false;
                    m_excelWorkbook.Close();
                    m_excelApplication.Quit();

                    ReleaseComObject(m_excelSheetRange);
                    ReleaseComObject(m_excelWorkbook);
                    ReleaseComObject(m_excelApplication);

                    throw new Exception("Не удалось открыть рабочую книгу!" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Файл не был открыт!", "Внимание!");
            }
        }

        /// <summary>
        /// Метод для уничтожения в памяти объектов для работы с excel
        /// </summary>
        /// <param name="obj"></param>
        private void ReleaseComObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occurred while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// Метод для открытия JSON файлов с данными
        /// </summary>
        private void OpenDataFromJson()
        {
            try
            {
                // чтение основной таблицы
                JObject parse = new JObject();
                parse = JObject.Parse(File.ReadAllText(Data.GetAppPath() + @"lPower.json"));
                m_dataFromlPower = JArray.Parse(parse["data"].ToString());
                // чтение данных по мощностям для га старого типа
                parse = new JObject();
                parse = JObject.Parse(File.ReadAllText(Data.GetAppPath() + @"lPowerPGANew.json"));
                m_dataPGANew = JArray.Parse(parse["data"].ToString());
                // чтение данных по мощностям для га нового типа
                parse = new JObject();
                parse = JObject.Parse(File.ReadAllText(Data.GetAppPath() + @"lPowerPGAOld.json"));
                m_dataPGAOld = JArray.Parse(parse["data"].ToString());
                // чтение данных по расходам для га старого типа
                parse = new JObject();
                parse = JObject.Parse(File.ReadAllText(Data.GetAppPath() + @"lPowerQGANew.json"));
                m_dataQGANew = JArray.Parse(parse["data"].ToString());
                // чтение данных по расходам для га нового типа
                parse = new JObject();
                parse = JObject.Parse(File.ReadAllText(Data.GetAppPath() + @"lPowerQGAOld.json"));
                m_dataQGAOld = JArray.Parse(parse["data"].ToString());
                // чтение данных по расходам для га нового типа
                parse = new JObject();
                parse = JObject.Parse(File.ReadAllText(Data.GetAppPath() + @"lPowerQ.json"));
                m_dataQ = JArray.Parse(parse["data"].ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("Для начала вы должны импортировать данные из Excel", "Внимание!");
            }

            try
            {
                HydroAggregates.ReadFromFile();
            }
            catch (Exception) { }

            try
            {
                autowpp.Bounds.OpenBoundsFromFile();
            }
            catch (Exception) { }

            try
            {
                GlobalOptions.ReadFromFile();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Метод для парса указанных столбцов из книги excel в json файл
        /// </summary>
        private void ParseExcelToJson()
        {
            try
            {
                OpenDataFromExcelWorkBook();

                JArray mainArray;

                int iterationCount = 0;
                int procentage = 0;

                for (int WSnum = 1; WSnum <= m_excelWorkbook.Sheets.Count; WSnum++)
                {
                    if ((m_excelWorkbook.Sheets.get_Item(WSnum) as ExcelObj.Worksheet) != null)
                    {

                        #region парс основных характеристик

                        if ((m_excelWorkbook.Sheets.get_Item(WSnum) as ExcelObj.Worksheet).Name == "ГИДРОгод")
                        {
                            mainArray = new JArray();
                            iterationCount++;
                            MStatusBarLabel.Text = "Задача " + iterationCount + "/6. Парсинг: " + procentage + "%";

                            try
                            {
                                m_excelSheetRange = (m_excelWorkbook.Sheets.get_Item(WSnum) as ExcelObj.Worksheet).UsedRange;
                                for (int Rnum = 8; Rnum <= m_excelSheetRange.Rows.Count; Rnum++)
                                {
                                    if ((m_excelSheetRange.Cells[Rnum, 1] as ExcelObj.Range).Value2 != null)
                                    {
                                        JObject JDay = new JObject();
                                        double date = double.Parse((m_excelSheetRange.Cells[Rnum, 1] as ExcelObj.Range).Value2.ToString());
                                        string dateString = DateTime.FromOADate(date).ToShortDateString();
                                        JDay.Add("date", dateString);
                                        JDay.Add("upstreamBief", ((m_excelSheetRange.Cells[Rnum, 48] as ExcelObj.Range).Value2));
                                        JDay.Add("downstreamBief", ((m_excelSheetRange.Cells[Rnum, 50] as ExcelObj.Range).Value2));
                                        JDay.Add("winterFactor", ((m_excelSheetRange.Cells[Rnum, 121] as ExcelObj.Range).Value2));
                                        JDay.Add("dh", ((m_excelSheetRange.Cells[Rnum, 118] as ExcelObj.Range).Value2));
                                        JDay.Add("netto", ((m_excelSheetRange.Cells[Rnum, 52] as ExcelObj.Range).Value2));
                                        JDay.Add("qga", ((m_excelSheetRange.Cells[Rnum, 53] as ExcelObj.Range).Value2));
                                        JDay.Add("qnb", ((m_excelSheetRange.Cells[Rnum, 56] as ExcelObj.Range).Value2));
                                        JDay.Add("qvsp", ((m_excelSheetRange.Cells[Rnum, 54] as ExcelObj.Range).Value2));
                                        if (JDay["qvsp"].ToString() == "")
                                        {
                                            JDay["qvsp"] = (double)0.0;
                                        }
                                        JDay.Add("qshluz", ((m_excelSheetRange.Cells[Rnum, 55] as ExcelObj.Range).Value2));
                                        if (JDay["qshluz"].ToString() == "")
                                        {
                                            JDay["qshluz"] = (double)0.0;
                                        }
                                        JDay.Add("volume", ((m_excelSheetRange.Cells[Rnum, 58] as ExcelObj.Range).Value2));
                                        JDay.Add("dV", ((m_excelSheetRange.Cells[Rnum, 59] as ExcelObj.Range).Value2));
                                        JDay.Add("accum", ((m_excelSheetRange.Cells[Rnum, 60] as ExcelObj.Range).Value2));
                                        JDay.Add("pritok", ((m_excelSheetRange.Cells[Rnum, 61] as ExcelObj.Range).Value2));
                                        //  "Нбрутто, м",
                                        //  "Qфильтр, м3/с",
                                        mainArray.Add(JDay);
                                    }

                                    double step = ((double)Rnum / m_excelSheetRange.Rows.Count * 100);
                                    procentage = procentage < 100 ? (int)step : 100;
                                    MStatusBarLabel.Text = "Задача " + iterationCount + "/6. Парсинг: " + procentage + "%";
                                }
                            }
                            catch (Exception) { }

                            MStatusBarLabel.Text = "";
                            JObject jsonFile = new JObject();
                            jsonFile.Add("data", mainArray);
                            File.WriteAllText(Data.GetAppPath() + @"lPower.json", jsonFile.ToString());
                        }

                        #endregion

                        #region парс таблицы мощностей гидроагрегатов

                        if ((m_excelWorkbook.Sheets.get_Item(WSnum) as ExcelObj.Worksheet).Name == "V")
                        {

                            #region новые данные

                            mainArray = new JArray();
                            iterationCount++;
                            procentage = 0;

                            try
                            {
                                m_excelSheetRange = (m_excelWorkbook.Sheets.get_Item(WSnum) as ExcelObj.Worksheet).UsedRange;
                                for (int Rnum = 5; Rnum <= 105; Rnum++)
                                {
                                    JObject JH = new JObject();
                                    JH.Add("netto", double.Parse((m_excelSheetRange.Cells[Rnum, 34] as ExcelObj.Range).Value2.ToString()));
                                    JArray JHArray = new JArray();
                                    for (int Cnum = 35; Cnum <= 76; Cnum++)
                                    {
                                        JObject znb = new JObject();
                                        znb.Add("downstreamBief", double.Parse((m_excelSheetRange.Cells[4, Cnum] as ExcelObj.Range).Value2.ToString()));
                                        znb.Add("value", double.Parse((m_excelSheetRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2.ToString()));
                                        JHArray.Add(znb);
                                    }
                                    JH.Add("data", JHArray);
                                    mainArray.Add(JH);

                                    double step = ((double)Rnum / 105 * 100);
                                    procentage = procentage < 100 ? (int)step : 100;
                                    MStatusBarLabel.Text = "Задача " + iterationCount + "/6. Парсинг: " + procentage + "%";
                                }
                            }
                            catch (Exception) { }

                            MStatusBarLabel.Text = "";
                            JObject jsonFile = new JObject();
                            jsonFile.Add("data", mainArray);
                            File.WriteAllText(Data.GetAppPath() + @"lPowerPGANew.json", jsonFile.ToString());

                            #endregion

                            #region старые данные

                            mainArray = new JArray();
                            iterationCount++;
                            procentage = 0;

                            try
                            {
                                for (int Rnum = 5; Rnum <= 105; Rnum++)
                                {
                                    JObject JH = new JObject();
                                    JH.Add("netto", double.Parse((m_excelSheetRange.Cells[Rnum, 21] as ExcelObj.Range).Value2.ToString()));
                                    JArray JHArray = new JArray();
                                    for (int Cnum = 22; Cnum <= 32; Cnum++)
                                    {
                                        JObject znb = new JObject();
                                        znb.Add("downstreamBief", double.Parse((m_excelSheetRange.Cells[4, Cnum] as ExcelObj.Range).Value2.ToString()));
                                        znb.Add("value", double.Parse((m_excelSheetRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2.ToString()));
                                        JHArray.Add(znb);
                                    }
                                    JH.Add("data", JHArray);
                                    mainArray.Add(JH);

                                    double step = ((double)Rnum / 105 * 100);
                                    procentage = procentage < 100 ? (int)step : 100;
                                    MStatusBarLabel.Text = "Задача " + iterationCount + "/6. Парсинг: " + procentage + "%";
                                }
                            }
                            catch (Exception) { }

                            MStatusBarLabel.Text = "";
                            jsonFile = new JObject();
                            jsonFile.Add("data", mainArray);
                            File.WriteAllText(Data.GetAppPath() + @"lPowerPGAOld.json", jsonFile.ToString());

                            #endregion

                        }

                        #endregion

                        #region парс таблицы расхода гидроагрегатов

                        #region новые данные

                        if ((m_excelWorkbook.Sheets.get_Item(WSnum) as ExcelObj.Worksheet).Name == "Qg2-g7")
                        {
                            mainArray = new JArray();
                            iterationCount++;
                            procentage = 0;

                            try
                            {
                                m_excelSheetRange = (m_excelWorkbook.Sheets.get_Item(WSnum) as ExcelObj.Worksheet).UsedRange;
                                for (int Rnum = 3; Rnum <= 135; Rnum++)
                                {
                                    JObject JPga = new JObject();
                                    JPga.Add("pga", double.Parse((m_excelSheetRange.Cells[Rnum, 1] as ExcelObj.Range).Value2.ToString()));
                                    JArray JHArray = new JArray();
                                    for (int Cnum = 2; Cnum <= 92; Cnum++)
                                    {
                                        JObject netto = new JObject();
                                        netto.Add("netto", double.Parse((m_excelSheetRange.Cells[1, Cnum] as ExcelObj.Range).Value2.ToString()));
                                        netto.Add("value", double.Parse((m_excelSheetRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2.ToString()));
                                        JHArray.Add(netto);
                                    }
                                    JPga.Add("data", JHArray);
                                    mainArray.Add(JPga);

                                    double step = ((double)Rnum / 135 * 100);
                                    procentage = procentage < 100 ? (int)step : 100;
                                    MStatusBarLabel.Text = "Задача " + iterationCount + "/6. Парсинг: " + procentage + "%";
                                }
                            }
                            catch (Exception) { }

                            MStatusBarLabel.Text = "";
                            JObject jsonFile = new JObject();
                            jsonFile.Add("data", mainArray);
                            File.WriteAllText(Data.GetAppPath() + @"lPowerQGANew.json", jsonFile.ToString());
                        }

                        #endregion

                        #region старые данные

                        if ((m_excelWorkbook.Sheets.get_Item(WSnum) as ExcelObj.Worksheet).Name == "Qg1")
                        {
                            mainArray = new JArray();
                            iterationCount++;
                            procentage = 0;

                            try
                            {
                                m_excelSheetRange = (m_excelWorkbook.Sheets.get_Item(WSnum) as ExcelObj.Worksheet).UsedRange;
                                for (int Rnum = 3; Rnum <= 145; Rnum++)
                                {
                                    JObject JPga = new JObject();
                                    JPga.Add("pga", double.Parse((m_excelSheetRange.Cells[Rnum, 1] as ExcelObj.Range).Value2.ToString()));
                                    JArray JHArray = new JArray();
                                    for (int Cnum = 2; Cnum <= 92; Cnum++)
                                    {
                                        JObject netto = new JObject();
                                        netto.Add("netto", double.Parse((m_excelSheetRange.Cells[1, Cnum] as ExcelObj.Range).Value2.ToString()));
                                        netto.Add("value", double.Parse((m_excelSheetRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2.ToString()));
                                        JHArray.Add(netto);
                                    }
                                    JPga.Add("data", JHArray);
                                    mainArray.Add(JPga);

                                    double step = ((double)Rnum / 145 * 100);
                                    procentage = procentage < 100 ? (int)step : 100;
                                    MStatusBarLabel.Text = "Задача " + iterationCount + "/6. Парсинг: " + procentage + "%";
                                }
                            }
                            catch (Exception) { }

                            MStatusBarLabel.Text = "";
                            JObject jsonFile = new JObject();
                            jsonFile.Add("data", mainArray);
                            File.WriteAllText(Data.GetAppPath() + @"lPowerQGAOld.json", jsonFile.ToString());
                        }

                        #endregion

                        #endregion

                        #region парс таблицы расхода мощности

                        if ((m_excelWorkbook.Sheets.get_Item(WSnum) as ExcelObj.Worksheet).Name == "q")
                        {
                            mainArray = new JArray();
                            iterationCount++;
                            procentage = 0;

                            try
                            {
                                m_excelSheetRange = (m_excelWorkbook.Sheets.get_Item(WSnum) as ExcelObj.Worksheet).UsedRange;
                                for (int Rnum = 5; Rnum <= 451; Rnum++)
                                {
                                    JObject JPga = new JObject();
                                    JPga.Add("p", double.Parse((m_excelSheetRange.Cells[Rnum, 1] as ExcelObj.Range).Value2.ToString()));
                                    JArray JHArray = new JArray();
                                    for (int Cnum = 2; Cnum <= 92; Cnum++)
                                    {
                                        JObject netto = new JObject();
                                        netto.Add("netto",
                                            double.Parse((m_excelSheetRange.Cells[3, Cnum] as ExcelObj.Range).Value2 != null ?
                                            (m_excelSheetRange.Cells[3, Cnum] as ExcelObj.Range).Value2.ToString() : "0"));
                                        netto.Add("value",
                                            double.Parse((m_excelSheetRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2 != null ?
                                            (m_excelSheetRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2.ToString() : "0"));
                                        JHArray.Add(netto);
                                    }
                                    JPga.Add("data", JHArray);
                                    mainArray.Add(JPga);

                                    double step = ((double)Rnum / 451 * 100);
                                    procentage = procentage < 100 ? (int)step : 100;
                                    MStatusBarLabel.Text = "Задача " + iterationCount + "/6. Парсинг: " + procentage + "%";
                                }
                            }
                            catch (Exception ex)
                            {
                                string lastError = ex.Message;
                            }

                            MStatusBarLabel.Text = "";
                            JObject jsonFile = new JObject();
                            jsonFile.Add("data", mainArray);
                            File.WriteAllText(Data.GetAppPath() + @"lPowerQ.json", jsonFile.ToString());
                        }

                        #endregion

                    }
                }

                m_excelSheetRange.Delete();
                m_excelWorkbook.Saved = false;
                m_excelApplication.DisplayAlerts = false;
                m_excelWorkbook.Close();
                m_excelApplication.Quit();

                ReleaseComObject(m_excelSheetRange);
                ReleaseComObject(m_excelWorkbook);
                ReleaseComObject(m_excelApplication);

                OpenDataFromJson();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
            }
        }

        /// <summary>
        /// Метод для сохранения данных из таблицы в excel файл
        /// </summary>
        private void SaveMDataGridViewToExcel()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = m_currentWorkmode == WorkMode.FirstTask ? 
                "DataForExportBy" + "FirstTask.xls":
                "DataForExportBy" + "SecondTask.xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                object misValue = System.Reflection.Missing.Value;
                m_excelApplication = new ExcelObj.Application();
                m_excelApplication.Visible = false;

                m_excelApplication.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                m_excelWorkbook = m_excelApplication.Workbooks.Add(misValue);
                ExcelObj.Worksheet m_excelWorksheet = (ExcelObj.Worksheet)m_excelWorkbook.Worksheets.get_Item(1);

                int columnIndex = 1;
                for (int Cnum = 0; Cnum < MDataGridView.ColumnCount; Cnum++)
                {
                    if (MDataGridView.Columns[Cnum].Visible)
                    {
                        if (MDataGridView.Columns[Cnum].Name == "qgarepairs")
                        {
                            continue;
                        }
                        Color columnColor = MDataGridView.Columns[Cnum].DefaultCellStyle.BackColor;
                        ((ExcelObj.Range)m_excelWorksheet.Cells[1, columnIndex]).Value = MDataGridView.Columns[Cnum].HeaderText;
                        for (int Rnum = 0; Rnum < MDataGridView.RowCount; Rnum++)
                        {
                            if (columnColor.A != 0 && columnColor.B != 0 && columnColor.G != 0 && columnColor.R != 0)
                            {
                                ((ExcelObj.Range)m_excelWorksheet.Cells[2 + Rnum, columnIndex]).Interior.Color =
                                    System.Drawing.ColorTranslator.ToOle(columnColor);
                            }
                            ((ExcelObj.Range)m_excelWorksheet.Cells[2 + Rnum, columnIndex]).Value =
                                 MDataGridView[Cnum, Rnum].Value;

                        }
                        columnIndex++;
                    }
                }

                m_excelWorkbook.SaveAs(sfd.FileName, ExcelObj.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, ExcelObj.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                m_excelApplication.DisplayAlerts = true;
                m_excelWorkbook.Close(true, misValue, misValue);
                m_excelApplication.Quit();

                ReleaseComObject(m_excelWorksheet);
                ReleaseComObject(m_excelWorkbook);
                ReleaseComObject(m_excelApplication);
            }
        }

        #endregion

        /// <summary>
        /// Метод для заполения итемов контекстного меню названиями столбцов
        /// </summary>
        private void FillMContextMenuStripByColumnHeaders()
        {
            m_contexMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            m_contexMenuStrip.Items.Clear();
            for (int i = 0; i < MDataGridView.Columns.Count; i++)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(MDataGridView.Columns[i].HeaderText);
                item.Checked = true;
                m_contexMenuStrip.Items.Add(item);
            }
            m_contexMenuStrip.ItemClicked += m_contexMenuStrip_ItemClicked;
        }

        #region Методы для обработки таблицы

        /// <summary>
        /// Инициализация структуры основной таблицы
        /// </summary>
        private void InitializeMDataGridView()
        {
            TableColumnsNames.Clear();
            TableColumnsHeaders.Clear();

            if (m_currentWorkmode == WorkMode.FirstTask)
            {
                MDataGridView.ColumnCount = Data.ColumnNames.Length;

                for (int i = 0; i < MDataGridView.ColumnCount; i++)
                {
                    string columnName = Data.GlobalNamesToKeys.ContainsKey(Data.ColumnNames[i]) == true ?
                        Data.GlobalNamesToKeys[Data.ColumnNames[i]] : Data.ColumnNames[i];

                    string columnHeaderText = Data.ColumnNames[i];

                    MDataGridView.Columns[i].Name = columnName;
                    MDataGridView.Columns[i].HeaderText = columnHeaderText;

                    TableColumnsNames.Add(columnHeaderText, columnName);
                    TableColumnsHeaders.Add(columnName, columnHeaderText);
                }

                foreach (DataGridViewColumn column in MDataGridView.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            else
            {
                MDataGridView.ColumnCount = Data.ColumnNames.Length * 2;

                int index = 0;

                for (int i = 0; i < MDataGridView.ColumnCount; i++)
                {
                    string columnName = Data.GlobalNamesToKeys.ContainsKey(Data.ColumnNames[index]) == true ?
                        Data.GlobalNamesToKeys[Data.ColumnNames[index]] : Data.ColumnNames[index];

                    string columnName2 = columnName + "_2";

                    string columnHeaderText = Data.ColumnNames[index];

                    string columnHeaderText2 = columnHeaderText + "_2";

                    MDataGridView.Columns[i].Name = columnName;
                    MDataGridView.Columns[i].HeaderText = columnHeaderText;

                    i++;
                    index++;

                    MDataGridView.Columns[i].Name = columnName2;
                    MDataGridView.Columns[i].HeaderText = columnHeaderText2;

                    TableColumnsNames.Add(columnHeaderText, columnName);
                    TableColumnsNames.Add(columnHeaderText2, columnName2);
                    TableColumnsHeaders.Add(columnName, columnHeaderText);
                    TableColumnsHeaders.Add(columnName2, columnHeaderText2);
                }

                foreach (DataGridViewColumn column in MDataGridView.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    if (column.Name.Length > 0 && column.Name[column.Name.Length - 1] == '2')
                    {
                        column.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                }
            }

            MDataGridView.Rows.Clear();

            MDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            MDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            MDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            MDataGridView.GridColor = Color.Black;
            MDataGridView.RowHeadersVisible = false;
            MDataGridView.MultiSelect = false;

            FillMDataGridView();
            FillMContextMenuStripByColumnHeaders();
        }

        /// <summary>
        /// Метод для добавления новой строки в контроллер MDataGridView
        /// </summary>
        /// <param name="count"> Количество строк </param>
        private void AddRowToMDataGridView(int count = 1)
        {
            MDataGridView.Rows.Add(count);
            for (int i = 0; i < count; i++)
            {
                m_currentRow = m_currentRow == null ? MDataGridView.Rows[0] : MDataGridView.Rows[m_currentRow.Index + 1];
                MDataGridView.Rows[m_currentRow.Index].Cells["qgarepairs"] = new DataGridViewButtonCell()
                {
                    Value = "Ремонты"
                };
            }
            if (m_currentRow.Index > 0 && MDataGridView.Rows[m_currentRow.Index - 1].Cells[0].Value != null)
            {
                DateTime prevDay = DateTime.Parse(MDataGridView.Rows[m_currentRow.Index - 1].Cells[0].Value.ToString());
                MDataGridView.Rows[m_currentRow.Index].Cells[0].Value = prevDay.AddDays(1).ToShortDateString();
            }
        }

        /// <summary>
        /// Метод для заполнения таблицы
        /// </summary>
        private void FillMDataGridView()
        {
            if (m_currentWorkmode == WorkMode.FirstTask)
            {
                AddRowToMDataGridView(1);
                for (int i = 0; i < MDataGridView.Columns.Count; i++)
                {
                    string columnName = MDataGridView.Columns[i].Name;
                    string columnHeaderText = MDataGridView.Columns[i].HeaderText;

                    if (m_dataFromlPower[0][columnName] != null)
                    {
                        MDataGridView.Rows[0].Cells[columnName].Value =
                            m_dataFromlPower[0][MDataGridView.Columns[i].Name];
                    }
                    else if (columnName != "qgarepairs")
                    {
                        MDataGridView.Rows[0].Cells[columnName].Value = "";
                    }
                }
                AddRowToMDataGridView(1);
                m_currentRow = MDataGridView.Rows[1];
            }
        }

        /// <summary>
        /// Метод для установления статуса видимости столбца
        /// </summary>
        /// <param name="columnName"> Имя столбца </param>
        /// <param name="state"> Признак видимости </param>
        private void SetStateOfTableColumn(string columnName, bool state = false)
        {
            bool isCompleted = false;          
            for (int i = 0; i < MDataGridView.Columns.Count || !isCompleted; i++)
            {
                if (TableColumnsNames[columnName] == MDataGridView.Columns[i].Name)
                {
                    for (int j = 0; j < m_contexMenuStrip.Items.Count || !isCompleted; j++)
                    {
                        if (columnName == m_contexMenuStrip.Items[j].Text)
                        {
                            ((ToolStripMenuItem)m_contexMenuStrip.Items[j]).Checked = state;
                            MDataGridView.Columns[i].Visible = state;
                            isCompleted = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Метод для проверки того что данные входят в диапазон ограничений
        /// </summary>
        private void CheckAndApplyBounds()
        {
            if (CurrentTaskComboBoxMMenuItem.Text != "")
            {
                for (int row = 0; row < MDataGridView.Rows.Count; row++)
                {
                    for (int i = 0; i < MDataGridView.Rows[row].Cells.Count; i++)
                    {
                        if (MDataGridView.Rows[row].Cells[i].Value != null)
                        {
                            MDataGridView.Rows[row].Cells[i].Style.ForeColor = Color.Black;
                        }
                        for (int b = 0; b < autowpp.Bounds.GetBounds().Count; b++)
                        {
                            if (MDataGridView.Columns[MDataGridView.Rows[row].Cells[i].ColumnIndex].Name == autowpp.Bounds.GetBounds()[b].Type)
                            {
                                if (MDataGridView.Rows[row].Cells[i].Value != null &&
                                    MDataGridView.Rows[row].Cells[i].Value != "")
                                {
                                    double currentValue = double.Parse(MDataGridView.Rows[row].Cells[i].Value.ToString());
                                    if (currentValue < autowpp.Bounds.GetBounds()[b].Left ||
                                        currentValue > autowpp.Bounds.GetBounds()[b].Right)
                                    {
                                        MDataGridView.Rows[row].Cells[i].Style.ForeColor = Color.Red;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Методы для получения расчетных данных

        /// <summary>
        /// Метод для расчета мощности старых га по заданным параметрам
        /// </summary>
        /// <param name="downstreamBief"> Уровень НБ </param>
        /// <param name="netto"> Ннетто </param>
        /// <param name="round"> Ограничение (70 или 65) </param>
        /// <returns> Мощность на га </returns>
        private double CalculatePgaOld(double downstreamBief, double netto, double round)
        {
            double Qga = 0;
            for (int i = 0; i < m_dataPGAOld.Count; i++)
            {
                double nettoFromTable = double.Parse(m_dataPGAOld[i]["netto"].ToString());
                if (nettoFromTable == netto)
                {
                    JArray downstreamBiefArray = JArray.Parse(m_dataPGAOld[i]["data"].ToString());
                    int index = 0;
                    for (int j = 0; j < downstreamBiefArray.Count; j++)
                    {
                        double valueFromTable = double.Parse(downstreamBiefArray[j]["downstreamBief"].ToString());
                        if (downstreamBief == valueFromTable)
                        {
                            index = j;
                            break;
                        }
                        else if (downstreamBief > valueFromTable)
                        {
                            index = j;
                        }
                    }
                    Qga = double.Parse(downstreamBiefArray[index]["value"].ToString());
                    break;
                }
            }
            if (Qga > round)
            {
                Qga = round;
            }
            return Qga;
        }

        /// <summary>
        /// Метод для расчета мощности новых га по заданным параметрам
        /// </summary>
        /// <param name="downstreamBief"> Уровень НБ </param>
        /// <param name="netto"> Ннетто </param>
        /// <returns> Мощность на га </returns>
        private double CalculatePgaNew(double downstreamBief, double netto)
        {
            double Qga = 0;
            for (int i = 0; i < m_dataPGANew.Count; i++)
            {
                double nettoFromTable = double.Parse(m_dataPGANew[i]["netto"].ToString());
                if (nettoFromTable == netto)
                {
                    JArray downstreamBiefArray = JArray.Parse(m_dataPGANew[i]["data"].ToString());
                    int index = 0;
                    for (int j = 0; j < downstreamBiefArray.Count; j++)
                    {
                        double valueFromTable = double.Parse(downstreamBiefArray[j]["downstreamBief"].ToString());
                        if (downstreamBief == valueFromTable)
                        {
                            index = j;
                            break;
                        }
                        else if (downstreamBief > valueFromTable)
                        {
                            index = j;
                        }
                    }
                    Qga = double.Parse(downstreamBiefArray[index]["value"].ToString());
                    break;
                }
            }
            return Qga;
        }

        /// <summary>
        /// экстраполяция по 4-м точкам с ивестными параметрами
        /// Расчет расхода гидроагрегата
        /// </summary>
        /// <param name="pga"> Мощность га </param>
        /// <param name="netto"> Уровень нетто </param>
        /// <param name="mode"> Режим расчета </param>
        /// <returns></returns>
        private double CalculateQga(double pga, double netto, int mode = 0)
        {
            JArray currentArray = mode == 1 ? m_dataQGANew : m_dataQGAOld;

            double heigth = Math.Round(netto, 1, MidpointRounding.ToEven);
            int Stolb = 0;
            JArray arr = JArray.Parse(currentArray[1]["data"].ToString());
            for (int i = 0; i < arr.Count; i++)
            {
                if (double.Parse(arr[i]["netto"].ToString()) == heigth)
                {
                    Stolb = i;
                    break;
                }
            }

            double x1, x2, x3, x4, y1, y2;

            double x_dwn;
            double x_up;
            double y_dwn;
            double y_up;

            x_up = x_dwn = y_dwn = y_up = x1 = x2 = x3 = x4 = y1 = y2 = 0;

            x_dwn = Math.Round(pga, 0, MidpointRounding.ToEven);
            x_up = Math.Round(pga, 0, MidpointRounding.AwayFromZero);

            y_dwn = heigth;
            y_up = Math.Round(heigth + 0.1, 1, MidpointRounding.ToEven); 

            if (x_dwn == x_up)
            {

                for (int i = 0; i < currentArray.Count; i++)
                {
                    if (double.Parse(currentArray[i]["pga"].ToString()) == x_dwn)
                    {
                        x1 = double.Parse(JArray.Parse(currentArray[i]["data"].ToString())[Stolb]["value"].ToString());
                        x2 = double.Parse(JArray.Parse(currentArray[i]["data"].ToString())[Stolb + 1]["value"].ToString());
                    }
                }

                return dTbl(x1, x2, y_dwn, y_up, netto, 0);
            }
            else
            {

                for (int i = 0; i < currentArray.Count; i++)
                {
                    if (double.Parse(currentArray[i]["pga"].ToString()) == x_up)
                    {
                        x3 = double.Parse(JArray.Parse(currentArray[i]["data"].ToString())[Stolb]["netto"].ToString());
                        x4 = double.Parse(JArray.Parse(currentArray[i]["data"].ToString())[Stolb + 1]["netto"].ToString());
                    }
                }

                y1 = dTbl(x1, x2, y_dwn, y_up, netto, 2);
                y2 = dTbl(x3, x4, y_dwn, y_up, netto, 2);

                return dTbl(y1, y2, x_dwn, x_up, pga, 0);
            }
        }

        /// <summary>
        /// *Результат экстраполяции по 2м точкам (перенесенно из VB)
        /// </summary>
        /// <param name="x_1"></param>
        /// <param name="x_2"></param>
        /// <param name="x_dwn"></param>
        /// <param name="x_up"></param>
        /// <param name="Val"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        private double dTbl(double x_1, double x_2, double x_dwn, double x_up, double Val, int d)
        {
            double kD, kQ;
            kD = (x_2 - x_1) / (x_up - x_dwn);
            kQ = x_1 + kD * (Val - x_dwn);
            return Math.Round(kQ, d, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Округление до ближайшего кратного
        /// </summary>
        /// <param name="value"> Число </param>
        /// <param name="significance"> Кратное </param>
        /// <returns> Ближайшее кратное к нулю </returns>
        public double Floor(double value, double significance)
        {
            if ((value % significance) != 0)
            {
                return ((int)(value / significance) * significance);
            }

            return Convert.ToDouble(value);
        }

        /// <summary>
        /// Метод для нахождения P гэс
        /// </summary>
        /// <param name="H"> Нетто </param>
        /// <param name="q"> Расход </param>
        /// <returns></returns>
        private double extrpl(double H, double q)
        {
            int currentColumn = 0;
            int currentRow = 0;
            H = Math.Round(H, 2);
            double h1 = Floor(H, 0.1);
            h1 = Math.Round(h1, 1);
            double dh = H - h1;
            dh = Math.Round(dh, 2);

            JArray sArray = JArray.Parse(m_dataQ[0]["data"].ToString());
            for (int i = 0; i < sArray.Count; i++)
            {
                if (double.Parse(sArray[i]["netto"].ToString()) == h1)
                {
                    currentColumn = i;
                    break;
                }
            }

            double q1, q2, q3, q4, n1, n2;

            q1 = q2 = q3 = q4 = n1 = n2 = 0;

            double dif = 0;

            for (int i = 0; i < m_dataQ.Count; i++)
            {
                sArray = JArray.Parse(m_dataQ[i]["data"].ToString());
                double value = double.Parse(sArray[currentColumn]["value"].ToString());
                double currentDif = q - value;
                if (dif != 0)
                {
                    if (currentDif < dif)
                    {
                        if (currentDif < 0)
                        {
                            if (Math.Abs(dif) > Math.Abs(currentDif))
                            {
                                currentRow = i;
                            }
                            JArray tempArr = JArray.Parse(m_dataQ[currentRow - 1]["data"].ToString());
                            q1 = double.Parse(tempArr[currentColumn]["value"].ToString());
                            q2 = double.Parse(tempArr[currentColumn + 1]["value"].ToString());
                            n1 = double.Parse(m_dataQ[currentRow - 1]["p"].ToString());
                            int row = currentRow + 3;
                            if (currentRow + 3 > m_dataQ.Count)
                            {
                                row = m_dataQ.Count - 1;
                            }
                            tempArr = JArray.Parse(m_dataQ[row]["data"].ToString());
                            q3 = double.Parse(tempArr[currentColumn]["value"].ToString());
                            q4 = double.Parse(tempArr[currentColumn + 1]["value"].ToString());
                            n2 = double.Parse(m_dataQ[row]["p"].ToString());

                            break;
                        }
                        currentRow = i;
                    }
                }

                dif = currentDif;
            }

            double dn = n2 - n1;
            double dq12 = q1 - ((q1 - q2) * dh * 10);
            double dq34 = q3 - ((q3 - q4) * dh * 10);
            double qqq = n1 + ((n1 / dq12) * q);

            return n1 + (((n2 - n1) / (dq34 - dq12)) * (q - dq12));
        }

        /// <summary>
        /// Метод для получения расчетного значения для верхнего бьефа
        /// </summary>
        /// <param name="volume"> Объем </param>
        /// <returns></returns>
        private double GetUpstreamBief(double volume)
        {
            // если V = az^2-bz+c, то 
            // z = (b + (b^2 - 4ac + 4av)^(1/2)) / 2a

            double a = 45.449;
            double b = 9213.2; // по сути тут должен быть минус, но в вычислениях и так плюс будет
            double c = 469005.0;
            double v = volume;

            double result = (b + (Math.Sqrt(Math.Pow(b, 2) - (4 * a * c) + (4 * a * v)))) / (2 * a);

            return result;
        }

        /// <summary>
        /// Метод для получения расчетного значения для нижнего бьефа
        /// </summary>
        /// <param name="qnb"> Расход </param>
        /// <param name="k"> Коэффицент Kзим </param>
        /// <returns></returns>
        private double GetDownstreamBief(double qnb, double k)
        {
            #region Расчет по полиному 6 степени с учетом winterFactor
            double polinom6 =
                -6.23859E-25 * Math.Pow(qnb, 6) +
                5.3206283034E-20 * Math.Pow(qnb, 5) -
                1.82093183244422E-15 * Math.Pow(qnb, 4) +
                3.22690525897938E-11 * Math.Pow(qnb, 3) -
                3.21143071552177E-07 * Math.Pow(qnb, 2) +
                0.00212144284807891 * qnb +
                91.3325496770558;

            polinom6 *= k;
            #endregion

            #region Расчет по полиному 2 степени с учетом winterFactor
            //double polinom2 =
            //    91.9543 +
            //    (-0.000000084 * (Math.Pow(qnb, 2))) +
            //    (0.001432 * qnb);

            //polinom2 *= k;
            #endregion

            return polinom6;
        }

        /// <summary>
        /// Метод для получения расчетного значения объема
        /// </summary>
        /// <param name="downstreanBief"> Уровень верхнего бьефа </param>
        /// <returns></returns>
        private double GetVolume(double downstreanBief)
        {
            double a = 45.449;
            double b = 9213.2;
            double c = 469005.0;

            double vdownstreanBief = (a * Math.Pow(downstreanBief, 2)) - (b * downstreanBief) + c;

            return vdownstreanBief;
        }

        #endregion

        /// <summary>
        /// Метод вычисляющий режим работы ГЭС по мат модели
        /// </summary>
        /// <returns></returns>
        private void CalculateFirstModule()
        {
            if (CurrentTaskComboBoxMMenuItem.Text == GlobalOptions.FirstTask.Name)
            {
                try
                {
                    bool isContinueCalculate = true;
                    while (isContinueCalculate)
                    {
                        DataGridViewRow _prevRow = MDataGridView.Rows[m_currentRow.Index - 1];
                        DateTime currentDate =
                                DateTime.Parse(MDataGridView.Rows[m_currentRow.Index].Cells[0].Value.ToString());

                        #region [x] Проверка и поиск табличных данных, если в настройках выбран авторасчет

                        // замена на данные если автозаполнение
                        if (m_currentRow.Cells["pritok"].Value == null)
                        {
                            if (GlobalOptions.FirstTask.isAutoCalculate)
                            {
                                for (int i = 0; i < m_dataFromlPower.Count; i++)
                                {
                                    if (DateTime.Parse(m_dataFromlPower[i]["date"].ToString()) == currentDate)
                                    {
                                        m_currentRow.Cells["pritok"].Value =
                                            double.Parse(m_dataFromlPower[i]["pritok"].ToString());

                                        m_currentRow.Cells["qnb"].Value =
                                            double.Parse(m_dataFromlPower[i]["qnb"].ToString());
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Поле для столбца \"Приток, м3/с\" не должно быть пустым", "Ошибка!");
                                return;
                            }
                        }

                        if (m_currentRow.Cells["qnb"].Value == null)
                        {
                            MessageBox.Show("Поле для столбца \"ΣQНБ, м3/с\" не должно быть пустым", "Ошибка!");
                            return;
                        }

                        #endregion

                        #region [x] РАСЧЕТ АККУМУЛЯЦИИ => A(t+1) = Qпр(t+1) - Qнб(t+1)

                        m_currentRow.Cells["accum"].Value =
                            double.Parse(m_currentRow.Cells["pritok"].Value.ToString()) -
                            double.Parse(m_currentRow.Cells["qnb"].Value.ToString());

                        #endregion

                        #region [x] РАСЧЕТ ДЕЛЬТЫ ПО ОБЪЕМУ dV(t+1)
                        m_currentRow.Cells["dV"].Value =
                            double.Parse(m_currentRow.Cells["accum"].Value.ToString())
                            * 86400 *  // доп коэффицент
                            Math.Pow(10, -6);
                        #endregion

                        #region [x] РАСЧЕТ V(t+1) = V(t) + dV(t+1)
                        m_currentRow.Cells["volume"].Value =
                            double.Parse(_prevRow.Cells["volume"].Value.ToString()) +
                            double.Parse(m_currentRow.Cells["dV"].Value.ToString());
                        #endregion

                        #region [x] РАСЧЕТ Zвб(t+1) = F(V(t+1))

                        m_currentRow.Cells["upstreamBief"].Value =
                            GetUpstreamBief(double.Parse(m_currentRow.Cells["volume"].Value.ToString()));

                        #endregion

                        #region [x] РАСЧЕТ Zнб(t+1) = F(Qнб(t+1)) * Кзим

                        double qnb = double.Parse(m_currentRow.Cells["qnb"].Value.ToString());
                        m_currentRow.Cells["downstreamBief"].Value =
                            GetDownstreamBief(qnb, double.Parse(m_dataFromlPower[m_currentRow.Index]["winterFactor"].ToString()));

                        m_currentRow.Cells["winterFactor"].Value =
                           m_dataFromlPower[m_currentRow.Index]["winterFactor"];

                        #endregion

                        #region [x] РАСЧЕТ Ннетто(t+1) = Zвб(t+1) - Zнб(t+1) - dh(t+1)
                        m_currentRow.Cells["dh"].Value =
                           m_dataFromlPower[m_currentRow.Index]["dh"];

                        m_currentRow.Cells["netto"].Value =
                            double.Parse(m_currentRow.Cells["upstreamBief"].Value.ToString()) -
                            double.Parse(m_currentRow.Cells["downstreamBief"].Value.ToString()) -
                            double.Parse(m_currentRow.Cells["dh"].Value.ToString());
                        #endregion

                        //#region [x] Расчет данных по Qga и Qmaxga

                        //#region [x] (1) РАСЧЕТ Pga(t+1)

                        //double Pga1 = CalculatePgaOld(
                        //    Math.Round(double.Parse(m_currentRow.Cells["downstreamBief"].Value.ToString()), 1),
                        //    Math.Round(double.Parse(m_currentRow.Cells["netto"].Value.ToString()), 1), 70);

                        //double Pga2 = CalculatePgaNew(
                        //   Math.Round(double.Parse(m_currentRow.Cells["downstreamBief"].Value.ToString()), 1),
                        //    Math.Round(double.Parse(m_currentRow.Cells["netto"].Value.ToString()), 1));

                        //double Pga3 = CalculatePgaOld(
                        //    Math.Round(double.Parse(m_currentRow.Cells["downstreamBief"].Value.ToString()), 1),
                        //    Math.Round(double.Parse(m_currentRow.Cells["netto"].Value.ToString()), 1), 65);

                        //double Pga4 = CalculatePgaOld(
                        //    Math.Round(double.Parse(m_currentRow.Cells["downstreamBief"].Value.ToString()), 1),
                        //    Math.Round(double.Parse(m_currentRow.Cells["netto"].Value.ToString()), 1), 65);

                        //double Pga5 = CalculatePgaOld(
                        //    Math.Round(double.Parse(m_currentRow.Cells["downstreamBief"].Value.ToString()), 1),
                        //    Math.Round(double.Parse(m_currentRow.Cells["netto"].Value.ToString()), 1), 70);

                        //double Pga6 = CalculatePgaOld(
                        //    Math.Round(double.Parse(m_currentRow.Cells["downstreamBief"].Value.ToString()), 1),
                        //    Math.Round(double.Parse(m_currentRow.Cells["netto"].Value.ToString()), 1), 70);

                        //double Pga7 = CalculatePgaNew(
                        //   Math.Round(double.Parse(m_currentRow.Cells["downstreamBief"].Value.ToString()), 1),
                        //    Math.Round(double.Parse(m_currentRow.Cells["netto"].Value.ToString()), 1));

                        //double CommonP = Pga1 + Pga2 + Pga3 + Pga4 + Pga5 + Pga6 + Pga7;

                        //#endregion

                        //#region [x] (4) Расчет Qga(t+1)

                        //double Qga1 = CalculateQga(Pga1, double.Parse(m_currentRow.Cells["netto"].Value.ToString()));
                        //double Qga2 = CalculateQga(Pga2, double.Parse(m_currentRow.Cells["netto"].Value.ToString()), 1);
                        //double Qga3 = CalculateQga(Pga3, double.Parse(m_currentRow.Cells["netto"].Value.ToString()));
                        //double Qga4 = CalculateQga(Pga4, double.Parse(m_currentRow.Cells["netto"].Value.ToString()));
                        //double Qga5 = CalculateQga(Pga5, double.Parse(m_currentRow.Cells["netto"].Value.ToString()));
                        //double Qga6 = CalculateQga(Pga6, double.Parse(m_currentRow.Cells["netto"].Value.ToString()));
                        //double Qga7 = CalculateQga(Pga7, double.Parse(m_currentRow.Cells["netto"].Value.ToString()), 1);

                        //double CommonQ = Qga1 + Qga2 + Qga3 + Qga4 + Qga5 + Qga6 + Qga7;

                        //#endregion

                        //#region [x] (5) Расчет Znb и напор при CommonQ

                        //double newDownstreamBief = double.Parse(m_currentRow.Cells["downstreamBief"].Value.ToString());
                        //if (qnb < CommonQ)
                        //{
                        //    newDownstreamBief =
                        //        GetDownstreamBief(CommonQ,
                        //        double.Parse(m_dataFromlPower[m_currentRow.Index]["winterFactor"].ToString()));
                        //}

                        //double _netto =
                        //   double.Parse(m_currentRow.Cells["upstreamBief"].Value.ToString()) -
                        //   newDownstreamBief -
                        //   double.Parse(m_currentRow.Cells["dh"].Value.ToString());

                        //#endregion

                        //#region [x] (6) Находим P по новому напору

                        //double newPga1 = CalculatePgaOld(
                        //    Math.Round(double.Parse(m_currentRow.Cells["downstreamBief"].Value.ToString()), 1),
                        //    Math.Round(_netto, 1), 70);

                        //double newPga2 = CalculatePgaNew(
                        //   Math.Round(double.Parse(m_currentRow.Cells["downstreamBief"].Value.ToString()), 1),
                        //    Math.Round(_netto, 1));

                        //double newPga3 = CalculatePgaNew(
                        //   Math.Round(double.Parse(m_currentRow.Cells["downstreamBief"].Value.ToString()), 1),
                        //    Math.Round(_netto, 1));

                        //double newPga4 = CalculatePgaNew(
                        //   Math.Round(double.Parse(m_currentRow.Cells["downstreamBief"].Value.ToString()), 1),
                        //    Math.Round(_netto, 1));

                        //double newPga5 = CalculatePgaOld(
                        //    Math.Round(double.Parse(m_currentRow.Cells["downstreamBief"].Value.ToString()), 1),
                        //    Math.Round(_netto, 1), 70);

                        //double newPga6 = CalculatePgaOld(
                        //    Math.Round(double.Parse(m_currentRow.Cells["downstreamBief"].Value.ToString()), 1),
                        //    Math.Round(_netto, 1), 70);

                        //double newPga7 = CalculatePgaNew(
                        //   Math.Round(double.Parse(m_currentRow.Cells["downstreamBief"].Value.ToString()), 1),
                        //    Math.Round(_netto, 1));

                        //double newCommonP = newPga1 + newPga2 + newPga3 + newPga4 + newPga5 + newPga6 + newPga7;

                        //#endregion

                        //#region [x] (7) Находим Q по новой P

                        //double newQga1 = CalculateQga(newPga1, double.Parse(m_currentRow.Cells["netto"].Value.ToString()));
                        //double newQga2 = CalculateQga(newPga2, double.Parse(m_currentRow.Cells["netto"].Value.ToString()), 1);
                        //double newQga3 = CalculateQga(newPga3, double.Parse(m_currentRow.Cells["netto"].Value.ToString()));
                        //double newQga4 = CalculateQga(newPga4, double.Parse(m_currentRow.Cells["netto"].Value.ToString()));
                        //double newQga5 = CalculateQga(newPga5, double.Parse(m_currentRow.Cells["netto"].Value.ToString()));
                        //double newQga6 = CalculateQga(newPga6, double.Parse(m_currentRow.Cells["netto"].Value.ToString()));
                        //double newQga7 = CalculateQga(newPga7, double.Parse(m_currentRow.Cells["netto"].Value.ToString()), 1);

                        //double newCommonQ = newQga1 + newQga2 + newQga3 + newQga4 + newQga5 + newQga6 + newQga7;

                        //#endregion

                        //#endregion

                        //#region [x] РАСЧЕТ P(t+1) = F(Hнетто(t+1);Qга(t+1))

                        //// Qга = расход через гидроагрегаты = макс пропускная Qmaxga

                        //double P = extrpl(
                        //    double.Parse(m_currentRow.Cells["netto"].Value.ToString()), CommonQ);

                        //if (P > CommonP)
                        //{
                        //    P = CommonP;
                        //}
                        //else
                        //{
                        //    P *= 1.035;
                        //}

                        //#endregion

                        //#region [x] Расчет Qхс(t+1) = Qнб(t+1) - Qмакс га(t+1)

                        //double Qhs = qnb - newCommonQ;

                        //#endregion

                        #region [х] Проверка на следующий шаг, если стоит флаг авторасчета

                        if (GlobalOptions.FirstTask.isAutoCalculate)
                        {
                            int start = currentDate.CompareTo(GlobalOptions.FirstTask.StartCalculate);
                            int end = currentDate.CompareTo(GlobalOptions.FirstTask.EndCalculate);

                            if (start < 0 || end >= 0)
                            {
                                isContinueCalculate = false;
                            }
                        }
                        else
                        {
                            isContinueCalculate = false;
                        }

                        #endregion

                        AddRowToMDataGridView();
                    }


                    JArray data = new JArray();

                    for (int i = 0; i < MDataGridView.Rows.Count; i++)
                    {
                        try
                        {
                            JObject exampleObject = new JObject();
                            exampleObject.Add("date",
                                DateTime.Parse(MDataGridView.Rows[i].Cells["date"].Value.ToString()));
                            exampleObject.Add(Data.GlobalNamesToKeys["УВБ, м"],
                                double.Parse(MDataGridView.Rows[i].Cells[Data.GlobalNamesToKeys["УВБ, м"]].Value.ToString()));
                            exampleObject.Add(Data.GlobalNamesToKeys["УНБ, м"],
                                double.Parse(MDataGridView.Rows[i].Cells[Data.GlobalNamesToKeys["УНБ, м"]].Value.ToString()));
                            exampleObject.Add(Data.GlobalNamesToKeys["ΣQНБ, м3/с"],
                                double.Parse(MDataGridView.Rows[i].Cells[Data.GlobalNamesToKeys["ΣQНБ, м3/с"]].Value.ToString()));
                            exampleObject.Add(Data.GlobalNamesToKeys["Объем водохр, млн. м3"],
                                double.Parse(MDataGridView.Rows[i].Cells[Data.GlobalNamesToKeys["Объем водохр, млн. м3"]].Value.ToString()));
                            exampleObject.Add(Data.GlobalNamesToKeys["Приток, м3/с"],
                                double.Parse(MDataGridView.Rows[i].Cells[Data.GlobalNamesToKeys["Приток, м3/с"]].Value.ToString()));

                            data.Add(exampleObject);
                        }
                        catch { ;}
                    }

                    m_chartsForm = new ChartsForm(data);
                }
                catch (Exception) { }
            }
        }

        /// <summary>
        /// Метод вычисляющий равномерный расход
        /// </summary>
        private void CalculateSecondModule()
        {
            #region расчет Qср (newQ)

            double k = 0.0864;

            int firstIndex = 0, lastIndex = 0;
            for (int i = 0; i < m_dataFromlPower.Count; i++)
            {
                if (m_dataFromlPower[i]["date"].ToString() == GlobalOptions.SecondTask.StartCalculate.ToShortDateString())
                {
                    firstIndex = i;
                }
                if (m_dataFromlPower[i]["date"].ToString() == GlobalOptions.SecondTask.EndCalculate.ToShortDateString())
                {
                    lastIndex = i;
                    break;
                }
            }

            if (firstIndex == 0 && lastIndex == 0)
            {
                MessageBox.Show("Выберите диапазон для вычислений второй задачи в настройках!", "Внимание!");
                return;
            }

            double zvb0 = double.Parse(m_dataFromlPower[firstIndex]["upstreamBief"].ToString());
            double zvb1 = double.Parse(m_dataFromlPower[firstIndex + 1]["upstreamBief"].ToString());

            double vzvb0 = GetVolume(zvb0);
            double vzvb1 = GetVolume(zvb1);

            double Qsr = 0;

            JArray oldData = new JArray();

            for (int i = firstIndex; i <= lastIndex; i++)
            {
                double pritok = double.Parse(m_dataFromlPower[i]["pritok"].ToString());
                double qpr = pritok * k - (zvb0 - zvb1);
                Qsr += qpr;
                oldData.Add(m_dataFromlPower[i]);
            }

            Qsr /= (lastIndex - firstIndex) * k;

            int index = 0;
            double div = Math.Abs(double.Parse(m_dataFromlPower[firstIndex]["pritok"].ToString()) - Qsr);

            for (int i = firstIndex; i <= lastIndex; i++)
            {
                double pritok = double.Parse(m_dataFromlPower[i]["pritok"].ToString());
                double newDiv = Math.Abs(pritok - Qsr);
                if (newDiv < div && pritok > Qsr)
                {
                    div = newDiv;
                    index = i;
                }
            }

            if (index == 0)
                index++;

            double newQ = double.Parse(m_dataFromlPower[index]["qnb"].ToString());

            #endregion

            #region ПЕРВЫЙ ШАГ В БЛОК СХЕМЕ

            double Accum = double.Parse(m_dataFromlPower[firstIndex - 1]["accum"].ToString()) - newQ;
            double dV = Accum * 86400 * Math.Pow(10, -6);
            double prevVolume = double.Parse(m_dataFromlPower[firstIndex - 1]["volume"].ToString()) + dV;

            JArray dataOfSecondModule = new JArray();
            for (int i = firstIndex; i <= lastIndex; i++)
            {
                JObject exampleObject = new JObject();

                // расчет мат модели на новый день
                double newPritok = double.Parse(m_dataFromlPower[i]["pritok"].ToString());
                double newAccum = newPritok - newQ;
                double newdV = newAccum * 86400 * Math.Pow(10, -6);
                double newVolume = prevVolume + newdV;
                prevVolume = newVolume;
                double newUpstreamBief = GetUpstreamBief(newVolume);
                double newDownstreamBief = GetDownstreamBief(
                    newQ, double.Parse(m_dataFromlPower[i]["winterFactor"].ToString()));
                // также нужен тут расчет нетто, мощности, Qга, Qхс, но они опущены

                exampleObject.Add("date", m_dataFromlPower[i]["date"]);
                exampleObject.Add("upstreamBief", newUpstreamBief);
                exampleObject.Add("downstreamBief", newDownstreamBief);
                exampleObject.Add("winterFactor", m_dataFromlPower[i]["winterFactor"]);
                exampleObject.Add("qnb", newQ);
                exampleObject.Add("volume", newVolume);
                exampleObject.Add("pritok", newPritok);
                exampleObject.Add("dV", newdV);
                exampleObject.Add("accum", newAccum);

                dataOfSecondModule.Add(exampleObject);
            }

            #endregion

            JArray OldData = JArray.Parse(oldData.ToString());// JArray.Parse(dataOfSecondModule.ToString());

            #region ВТОРОЙ ШАГ БЛОК СХЕМЫ

            List<int> srabotka = new List<int>();
            List<int> napolnenie = new List<int>();

            for (int i = 0; i < dataOfSecondModule.Count; i++)
            {
                double currentPritok = double.Parse(dataOfSecondModule[i]["pritok"].ToString());
                if (currentPritok < newQ) // если сработка - исключаем
                {
                    srabotka.Add(i);
                }
                else
                {
                    napolnenie.Add(i);
                }
            }

            #endregion

            double Zvbmax = 113.7;
            double Qmin = 108.5;

            // если есть сработка - считаем
            if (srabotka.Count != 0)
            {
                #region ПЕРВЫЙ ЦИКЛ БЛОК СХЕМЫ - ОБРАБОТКА СРАБОТКИ

                double Qnedorabotka = 0;

                for (int i = 0; i < srabotka.Count; i++)
                {
                    double currentPritok = double.Parse(dataOfSecondModule[srabotka[i]]["pritok"].ToString());

                    if (Qmin <= currentPritok && currentPritok < newQ)
                    {
                        Qnedorabotka +=
                            ((double.Parse(dataOfSecondModule[srabotka[i]]["qnb"].ToString())) - currentPritok);
                        dataOfSecondModule[srabotka[i]]["qnb"] = currentPritok;
                    }
                    else if (currentPritok < Qmin)
                    {
                        Qnedorabotka +=
                            ((double.Parse(dataOfSecondModule[srabotka[i]]["qnb"].ToString())) - Qmin);
                        dataOfSecondModule[srabotka[i]]["qnb"] = Qmin;
                    }
                }

                #endregion

                #region ДЛЯ ВСЕХ ПЕРИОДОВ НАПОЛНЕНИЯ ДОБОВЛЯЕМ QНЕДОРАБ

                for (int i = 0; i < napolnenie.Count; i++)
                {
                    dataOfSecondModule[napolnenie[i]]["qnb"] =
                        double.Parse(dataOfSecondModule[napolnenie[i]]["qnb"].ToString()) + (Qnedorabotka / napolnenie.Count);
                }

                #endregion

                #region ВТОРОЙ ЦИКЛ БЛОК СХЕМЫ - ОБРАБОТКА НАПОЛНЕНИЯ

                Qnedorabotka = 0;

                for (int i = 0; i < napolnenie.Count; i++)
                {
                    double currentZvb = double.Parse(dataOfSecondModule[napolnenie[i]]["upstreamBief"].ToString());
                    if (currentZvb > Zvbmax)
                    {
                        Qnedorabotka += (GetVolume(Zvbmax) - GetVolume(currentZvb)) / k;
                    }
                }

                #endregion

                #region ДЛЯ ВСЕХ ПЕРИОДОВ НАПОЛНЕНИЯ ДОБОВЛЯЕМ QНЕДОРАБ

                for (int i = 0; i < napolnenie.Count; i++)
                {
                    dataOfSecondModule[napolnenie[i]]["qnb"] =
                        double.Parse(dataOfSecondModule[napolnenie[i]]["qnb"].ToString()) + (Qnedorabotka / napolnenie.Count);
                }

                #endregion
            }

            JArray NewData = JArray.Parse(dataOfSecondModule.ToString());

            #region Заполнение таблицы

            AddRowToMDataGridView();
            MDataGridView.Rows[m_currentRow.Index].Cells["date"].Value =
                DateTime.Parse(OldData[0]["date"].ToString()).ToShortDateString();

            MDataGridView.Rows[m_currentRow.Index].Cells["upstreamBief"].Value =
                double.Parse(OldData[0]["upstreamBief"].ToString());

            MDataGridView.Rows[m_currentRow.Index].Cells["upstreamBief_2"].Value =
                double.Parse(NewData[0]["upstreamBief"].ToString());

            MDataGridView.Rows[m_currentRow.Index].Cells["downstreamBief"].Value =
                double.Parse(OldData[0]["downstreamBief"].ToString());

            MDataGridView.Rows[m_currentRow.Index].Cells["downstreamBief_2"].Value =
                double.Parse(NewData[0]["downstreamBief"].ToString());

            MDataGridView.Rows[m_currentRow.Index].Cells["qnb"].Value =
                double.Parse(OldData[0]["qnb"].ToString());

            MDataGridView.Rows[m_currentRow.Index].Cells["qnb_2"].Value =
                double.Parse(NewData[0]["qnb"].ToString());

            MDataGridView.Rows[m_currentRow.Index].Cells["pritok"].Value =
                double.Parse(OldData[0]["pritok"].ToString());

            MDataGridView.Rows[m_currentRow.Index].Cells["pritok_2"].Value =
                double.Parse(NewData[0]["pritok"].ToString());

            for (int i = 1; i < OldData.Count; i++)
            {
                AddRowToMDataGridView();
                MDataGridView.Rows[m_currentRow.Index].Cells["upstreamBief"].Value =
                    double.Parse(OldData[i]["upstreamBief"].ToString());

                MDataGridView.Rows[m_currentRow.Index].Cells["upstreamBief_2"].Value =
                    double.Parse(NewData[i]["upstreamBief"].ToString());

                MDataGridView.Rows[m_currentRow.Index].Cells["downstreamBief"].Value =
                    double.Parse(OldData[i]["downstreamBief"].ToString());

                MDataGridView.Rows[m_currentRow.Index].Cells["downstreamBief_2"].Value =
                    double.Parse(NewData[i]["downstreamBief"].ToString());

                MDataGridView.Rows[m_currentRow.Index].Cells["qnb"].Value =
                    double.Parse(OldData[i]["qnb"].ToString());

                MDataGridView.Rows[m_currentRow.Index].Cells["qnb_2"].Value =
                    double.Parse(NewData[i]["qnb"].ToString());

                MDataGridView.Rows[m_currentRow.Index].Cells["pritok"].Value =
                    double.Parse(OldData[i]["pritok"].ToString());

                MDataGridView.Rows[m_currentRow.Index].Cells["pritok_2"].Value =
                    double.Parse(NewData[i]["pritok"].ToString());
            }

            #endregion

            m_chartsForm = new ChartsForm(OldData, NewData);
        }

        #endregion

        #region Делегаты событий

        #region События таблицы MDataGridView

        /// <summary>
        /// Обработчик события щелчка мыши по заголовку столбца таблицы
        /// </summary>
        private void MDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                m_contexMenuStrip.Show(MDataGridView, m_lastPosOfRightMouseButton);
            }
        }

        /// <summary>
        /// Обработчик события щелчка мыши по ячейке таблицы
        /// </summary>
        private void MDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (MDataGridView.Columns["qgarepairs"].Index == e.ColumnIndex && e.RowIndex >= 0)
                {
                    RepairsForm form = new RepairsForm();
                    form.Show();
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Обработка события клика мыши по таблице
        /// </summary>
        private void MDataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                m_lastPosOfRightMouseButton = e.Location;
            }
        }
        
        #endregion

        /// <summary>
        /// Обработчик события щелчка мыши по пункту всплывающего меню
        /// </summary>
        private void m_contexMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            SetStateOfTableColumn(e.ClickedItem.Text, !((ToolStripMenuItem)e.ClickedItem).Checked);
        }

        /// <summary>
        /// Делегат для обработки события нажатия кнопки расчета
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
#if DEBUG
            DateTime start = DateTime.Now;
#endif

            if (CurrentTaskComboBoxMMenuItem.Text == GlobalOptions.FirstTask.Name)
            {
                CalculateFirstModule();
            }
            else if (CurrentTaskComboBoxMMenuItem.Text == GlobalOptions.SecondTask.Name)
            {
                CalculateSecondModule();
            }
            else
            {
                MessageBox.Show("Выберите пункт в выпадающем списке!", "Внимание!");
            }

#if DEBUG
            DateTime end = DateTime.Now;

            MessageBox.Show(start.Second.ToString() + ":" + start.Millisecond.ToString() +
                "/" + end.Second.ToString() + ":" + end.Millisecond.ToString());
#endif
        }

        /// <summary>
        /// Делегат для открытия формы работы с ограничениями
        /// </summary>
        private void BoundsMMenuItem_Click(object sender, EventArgs e)
        {
            BoundsForm boundsForm = new BoundsForm();
            boundsForm.Show();
        }

        /// <summary>
        /// Делегат для обработки закрытия окна
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            HydroAggregates.WriteToFile();
            autowpp.Bounds.SaveBoundsToFile();
        }

        /// <summary>
        /// Делегат для обработки событий кнопки проверки ограничений
        /// </summary>
        private void CheckBoundsButton_Click(object sender, EventArgs e)
        {
            CheckAndApplyBounds();
        }

        /// <summary>
        /// Делегат для обработки события нажатия кнопки настроек
        /// </summary>
        private void OptioinsMMenuItem_Click(object sender, EventArgs e)
        {
            OptionsForm of = new OptionsForm();
            of.Show();
        }

        /// <summary>
        /// Делегат для обработки выбранного итема в выпадающем списке задач
        /// </summary>
        private void CurrentTaskComboBoxMMenuItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowChartsMMenuItem.Enabled = false;
            if (CurrentTaskComboBoxMMenuItem.Text == GlobalOptions.FirstTask.Name)
            {
                SetWorkMode(WorkMode.FirstTask);
                InitializeMDataGridView();

                SetStateOfTableColumn(TableColumnsHeaders["brutto"], false);
                SetStateOfTableColumn(TableColumnsHeaders["winterFactor"], false);
                SetStateOfTableColumn(TableColumnsHeaders["qfiltr"], false);
                SetStateOfTableColumn(TableColumnsHeaders["qvsp"], false);
                SetStateOfTableColumn(TableColumnsHeaders["qshluz"], false);
                SetStateOfTableColumn(TableColumnsHeaders["dV"], false);
                SetStateOfTableColumn(TableColumnsHeaders["volume"], false);
                SetStateOfTableColumn(TableColumnsHeaders["accum"], false);
                SetStateOfTableColumn(TableColumnsHeaders["qga"], false);

                ShowChartsMMenuItem.Enabled = true;
            }
            else
            {
                SetWorkMode(WorkMode.SecondTask);
                InitializeMDataGridView();

                SetStateOfTableColumn(TableColumnsHeaders["brutto"], false);
                SetStateOfTableColumn(TableColumnsHeaders["winterFactor"], false);
                SetStateOfTableColumn(TableColumnsHeaders["qfiltr"], false);
                SetStateOfTableColumn(TableColumnsHeaders["qvsp"], false);
                SetStateOfTableColumn(TableColumnsHeaders["dh"], false);
                SetStateOfTableColumn(TableColumnsHeaders["qshluz"], false);
                SetStateOfTableColumn(TableColumnsHeaders["dV"], false);
                SetStateOfTableColumn(TableColumnsHeaders["volume"], false);
                SetStateOfTableColumn(TableColumnsHeaders["accum"], false);
                SetStateOfTableColumn(TableColumnsHeaders["qga"], false);
                SetStateOfTableColumn(TableColumnsHeaders["qgarepairs"], false);
                SetStateOfTableColumn(TableColumnsHeaders["netto"], false);

                SetStateOfTableColumn(TableColumnsHeaders["date_2"], false);
                SetStateOfTableColumn(TableColumnsHeaders["brutto_2"], false);
                SetStateOfTableColumn(TableColumnsHeaders["winterFactor_2"], false);
                SetStateOfTableColumn(TableColumnsHeaders["qfiltr_2"], false);
                SetStateOfTableColumn(TableColumnsHeaders["qvsp_2"], false);
                SetStateOfTableColumn(TableColumnsHeaders["dh_2"], false);
                SetStateOfTableColumn(TableColumnsHeaders["qshluz_2"], false);
                SetStateOfTableColumn(TableColumnsHeaders["dV_2"], false);
                SetStateOfTableColumn(TableColumnsHeaders["volume_2"], false);
                SetStateOfTableColumn(TableColumnsHeaders["accum_2"], false);
                SetStateOfTableColumn(TableColumnsHeaders["qga_2"], false);
                SetStateOfTableColumn(TableColumnsHeaders["qgarepairs_2"], false);
                SetStateOfTableColumn(TableColumnsHeaders["netto_2"], false);

                ShowChartsMMenuItem.Enabled = true;
            }
        }

        /// <summary>
        /// Делегат для обработки события нажатия кнопки отображения графиков
        /// </summary>s
        private void ShowChartsMMenuItem_Click(object sender, EventArgs e)
        {
            if (m_chartsForm != null)
            {
                try
                {
                    m_chartsForm.Show();
                }
                catch (Exception) { }
            }
            else
            {
                MessageBox.Show("Для отображения графиков выполните вычисления!", "Внимание!");
            }
        }

        /// <summary>
        /// Делегат для обработки события нажатия кнопки открытия данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenMMenuItem_Click(object sender, EventArgs e)
        {
            ParseExcelToJson();
        }

        /// <summary>
        /// Делегат для обработки события нажатия кнопки сохранения данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveMMenuItem_Click(object sender, EventArgs e)
        {
            SaveMDataGridViewToExcel();
        }

        /// <summary>
        /// Делегат для обработки события нажатия кнопки закрытия программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitMMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}