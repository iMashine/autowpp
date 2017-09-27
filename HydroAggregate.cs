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
    /// Класс объекта га
    /// </summary>
    public class HydroAggregate
    {
        private int number;

        /// <summary>
        /// Номер га
        /// </summary>
        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        /// <summary>
        /// Список ремонтов
        /// </summary>
        public List<Repair> Repairs = new List<Repair>();

        /// <summary>
        /// Базовый констркуктор
        /// </summary>
        public HydroAggregate()
        {

        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="number"> Номер га </param>
        public HydroAggregate(int number)
        {
            this.number = number;
        }

        /// <summary>
        /// Метод для получения доступного Id для объекта ремонта
        /// </summary>
        /// <returns></returns>
        public int GetAvailableId()
        {
            int AvailableId = 0;
            if (Repairs.Count != 0)
            {
                AvailableId = this.Repairs[this.Repairs.Count - 1].Id + 1;
            }
            return AvailableId;
        }

        /// <summary>
        /// Метод для добавления ремонта в список
        /// </summary>
        /// <param name="repair"> Объект ремонта </param>
        public void AddRepair(Repair repair)
        {
            if (repair.Id == -1)
            {
                repair.Id = GetAvailableId();
            }
            Repairs.Add(repair);
        }

        /// <summary>
        /// Метод для удаления ремонта из списка 
        /// </summary>
        /// <param name="id"> Id ремонта </param>
        public void RemoveRepair(int id)
        {
            int removeIndex = 0;
            for (int i = 0; i < this.Repairs.Count; i++)
            {
                if (this.Repairs[i].Id == id)
                {
                    removeIndex = i;
                    break;
                }
            }
            this.Repairs.RemoveAt(removeIndex);
        }
    }

    /// <summary>
    /// Класс для работы с га
    /// </summary>
    public static class HydroAggregates
    {
        /// <summary>
        /// Список га
        /// </summary>
        public static List<HydroAggregate> Aggregates = new List<HydroAggregate>();

        /// <summary>
        /// Запись в файл данных о га
        /// </summary>
        public static void WriteToFile()
        {
            JArray mainArray = new JArray();
            for (int i = 0; i < Aggregates.Count; i++)
            {
                JObject aggregate = new JObject();
                aggregate.Add("number", Aggregates[i].Number);
                JArray repairs = new JArray();
                for (int j = 0; j < Aggregates[i].Repairs.Count; j++)
                {
                    JObject repair = new JObject();
                    repair.Add("id", Aggregates[i].Repairs[j].Id);
                    repair.Add("start", Aggregates[i].Repairs[j].StartDate.ToShortDateString());
                    repair.Add("end", Aggregates[i].Repairs[j].EndDate.ToShortDateString());
                    repairs.Add(repair);
                }
                aggregate.Add("repairs", repairs);
                mainArray.Add(aggregate);
            }
            JObject jsonFile = new JObject();
            jsonFile.Add("data", mainArray);
            File.WriteAllText(Data.GetAppPath() + @"HydroAggregates.json", jsonFile.ToString());
        }

        /// <summary>
        /// Чтение из файла данных о га
        /// </summary>
        public static void ReadFromFile()
        {
            try
            {
                JObject parse = new JObject();
                parse = JObject.Parse(File.ReadAllText(Data.GetAppPath() + @"HydroAggregates.json"));
                JArray array = JArray.Parse(parse["data"].ToString());
                for (int i = 0; i < array.Count; i++)
                {
                    HydroAggregate aggregate = new HydroAggregate(int.Parse(array[i]["number"].ToString()));
                    JArray repairs = JArray.Parse(array[i]["repairs"].ToString());
                    for (int j = 0; j < repairs.Count; j++)
                    {
                        Repair repair = new Repair();
                        repair.Id = int.Parse(repairs[j]["id"].ToString());
                        repair.StartDate = DateTime.Parse(repairs[j]["start"].ToString());
                        repair.EndDate = DateTime.Parse(repairs[j]["end"].ToString());
                        aggregate.Repairs.Add(repair);
                    }
                    Aggregates.Add(aggregate);
                }
                if (array.Count == 0)
                {
                    Aggregates.Add(new HydroAggregate(1));
                    Aggregates.Add(new HydroAggregate(2));
                    Aggregates.Add(new HydroAggregate(3));
                    Aggregates.Add(new HydroAggregate(4));
                    Aggregates.Add(new HydroAggregate(5));
                    Aggregates.Add(new HydroAggregate(6));
                    Aggregates.Add(new HydroAggregate(7));
                }
            }
            catch (Exception)
            {

            }
        }
    }
}