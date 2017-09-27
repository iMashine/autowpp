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
    /// Класс объекта ограничений
    /// </summary>
    public class Bound
    {
        private int id;

        /// <summary>
        /// Id ограничения
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string type;

        /// <summary>
        /// Тип ограничения
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private double left;

        /// <summary>
        /// Левая граница ограничения
        /// </summary>
        public double Left
        {
            get { return left; }
            set { left = value; }
        }

        private double right;

        /// <summary>
        /// Правая граница ограничения
        /// </summary>
        public double Right
        {
            get { return right; }
            set { right = value; }
        }

        /// <summary>
        /// Базовый конструктор
        /// </summary>
        public Bound()
        {
            left = right = id = -1;
        }
    }

    /// <summary>
    /// Класс для работы с ограничениями
    /// </summary>
    public static class Bounds
    {
        /// <summary>
        /// Список со всеми ограничениями
        /// </summary>
        private static List<Bound> m_bounds = new List<Bound>();

        /// <summary>
        /// Метод для получения доступного Id для объекта ограничеия
        /// </summary>
        /// <returns></returns>
        private static int GetAvailableId()
        {
            int AvailableId = 0;
            if (m_bounds.Count != 0)
            {
                AvailableId = m_bounds[m_bounds.Count - 1].Id + 1;
            }
            return AvailableId;
        }

        /// <summary>
        /// Метод для поиска вхождения значения в список
        /// </summary>
        /// <param name="Id"> Id ограничения </param>
        /// <returns> Признак вхождения </returns>
        public static bool Contains(int Id)
        {
            bool isContains = false;
            for (int i = 0; i < m_bounds.Count && !isContains; i++)
            {
                if (Id == m_bounds[i].Id)
                {
                    isContains = true;
                    break;
                }
            }
            return isContains;
        }

        /// <summary>
        /// Метод для создания ограничения
        /// </summary>
        /// <param name="bound"> Новое ограничение </param>
        public static void AddBound(Bound bound)
        {
            bound.Id = GetAvailableId();
            m_bounds.Add(bound);
            SaveBoundsToFile();
        }

        /// <summary>
        /// Метод для изменения ограниченияы
        /// </summary>
        /// <param name="bound"> Ограничение </param>
        public static void EditBound(Bound bound)
        {
            for (int i = 0; i < m_bounds.Count; i++)
            {
                if (bound.Id == m_bounds[i].Id)
                {
                    m_bounds[i] = bound;
                    break;
                }
            }
            SaveBoundsToFile();
        }

        /// <summary>
        /// Метод для получения ограничений
        /// </summary>
        /// <returns> Список ограничений </returns>
        public static List<Bound> GetBounds()
        {
            return new List<Bound>(m_bounds);
        }

        /// <summary>
        /// Метод для удаления ограничения по Id
        /// </summary>
        /// <param name="id"> Id ограничения </param>
        public static void RemoveBound(int id)
        {
            int index = 0;
            bool isFind = false;
            for (int i = 0; i < m_bounds.Count && !isFind; i++)
            {
                if (id == m_bounds[i].Id)
                {
                    index = i;
                    isFind = true;
                }
            }
            if (isFind)
            {
                m_bounds.RemoveAt(index);
            }
            SaveBoundsToFile();
        }

        /// <summary>
        /// Метод для записи ограничений в файл
        /// </summary>
        public static void SaveBoundsToFile()
        {
            JArray arrayForSave = new JArray();
            foreach (Bound bound in m_bounds)
            {
                JObject _bound = new JObject();
                _bound.Add("id", bound.Id);
                _bound.Add("type", bound.Type);
                _bound.Add("leftBound", bound.Left);
                _bound.Add("rightBound", bound.Right);
                arrayForSave.Add(_bound);
            }
            JObject jsonFile = new JObject();
            jsonFile.Add("data", arrayForSave);
            File.WriteAllText(Data.GetAppPath() + @"lPowerBounds.json", jsonFile.ToString());
        }

        /// <summary>
        /// Метод для загрузки ограничений из файла
        /// </summary>
        public static void OpenBoundsFromFile()
        {
            string openData = File.ReadAllText(Data.GetAppPath() + @"lPowerBounds.json");
            JObject parse = JObject.Parse(openData);
            JArray arrayForLoad = new JArray();
            arrayForLoad = JArray.Parse(parse["data"].ToString());

            m_bounds.Clear();

            for (int i = 0; i < arrayForLoad.Count; i++)
            {
                JObject bound = JObject.Parse(arrayForLoad[i].ToString());
                AddBound(new Bound()
                {
                    Id = int.Parse(bound["id"].ToString()),
                    Type = bound["type"].ToString(),
                    Left =  double.Parse(bound["leftBound"].ToString()), 
                    Right = double.Parse(bound["rightBound"].ToString())
                });
            }
        }
    }
}