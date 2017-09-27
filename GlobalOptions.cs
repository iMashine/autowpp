using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autowpp
{
    /// <summary>
    /// Единый класс для обработки глобальных настроек приложения
    /// </summary>
    public static class GlobalOptions
    {
        /// <summary>
        /// Класс настроек для первой задачи
        /// </summary>
        public static class FirstTask
        {
            private static string m_name = "Задача 1 - Задача расчета водно-энергетического режима ГЭС.";

            /// <summary>
            /// Название задачи
            /// </summary>
            public static string Name
            {
                get { return m_name; }
            }

            private static bool m_isAutoCalculate;

            /// <summary>
            /// Флаг указывающий необходимость в авторасчете (расчете на несколько дней)
            /// </summary>
            public static bool isAutoCalculate
            {
                get { return m_isAutoCalculate; }
                set { m_isAutoCalculate = value; }
            }

            private static DateTime m_startCalculate;

            /// <summary>
            /// Начало периода расчета
            /// </summary>
            public static DateTime StartCalculate
            {
                get { return m_startCalculate; }
                set { m_startCalculate = value; }
            }

            private static DateTime m_endCalculate;

            /// <summary>
            /// Окончание периода расчета
            /// </summary>
            public static DateTime EndCalculate
            {
                get { return m_endCalculate; }
                set { m_endCalculate = value; }
            }

        }

        /// <summary>
        /// Класс настроек для второй задачи
        /// </summary>
        public static class SecondTask
        {
            private static string name = "Задача 2 - Задача расчета допустимого режима ГЭС при пропуске половодья.";

            /// <summary>
            /// Название задачи
            /// </summary>
            public static string Name
            {
                get { return name; }
            }

            private static DateTime m_startCalculate;

            /// <summary>
            /// Начало периода расчета
            /// </summary>
            public static DateTime StartCalculate
            {
                get { return m_startCalculate; }
                set { m_startCalculate = value; }
            }

            private static DateTime m_endCalculate;

            /// <summary>
            /// Окончание периода расчета
            /// </summary>
            public static DateTime EndCalculate
            {
                get { return m_endCalculate; }
                set { m_endCalculate = value; }
            }
        }

        /// <summary>
        /// Метод для сохранения настроек в файл
        /// </summary>
        public static void SaveToFile()
        {
            JObject tasks = new JObject();
            JObject firstTask = new JObject();
            firstTask.Add("isAutoCalculate", FirstTask.isAutoCalculate);
            firstTask.Add("start", FirstTask.StartCalculate.ToShortDateString());
            firstTask.Add("end", FirstTask.EndCalculate.ToShortDateString());
            tasks.Add("firstTask", firstTask);
            JObject secondTask = new JObject();
            secondTask.Add("start", SecondTask.StartCalculate.ToShortDateString());
            secondTask.Add("end", SecondTask.EndCalculate.ToShortDateString());
            tasks.Add("secondTask", secondTask);
            JObject jsonFile = new JObject();
            jsonFile.Add("data", tasks);
            File.WriteAllText(Data.GetAppPath() + @"TasksConfig.json", jsonFile.ToString());
        }

        /// <summary>
        /// Метод для чтения настроек из файла
        /// </summary>
        public static void ReadFromFile()
        {
            try
            {
                JObject parse = new JObject();
                parse = JObject.Parse(File.ReadAllText(Data.GetAppPath() + @"TasksConfig.json"));
                JObject data = JObject.Parse(parse["data"].ToString());
                FirstTask.isAutoCalculate = bool.Parse(data["firstTask"]["isAutoCalculate"].ToString());
                FirstTask.StartCalculate = DateTime.Parse(data["firstTask"]["start"].ToString());
                FirstTask.EndCalculate = DateTime.Parse(data["firstTask"]["end"].ToString());
                SecondTask.StartCalculate = DateTime.Parse(data["secondTask"]["start"].ToString());
                SecondTask.EndCalculate = DateTime.Parse(data["secondTask"]["end"].ToString());
            }
            catch (Exception)
            {

            }
        }
    }
}
