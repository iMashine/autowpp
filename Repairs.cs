using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autowpp
{
    /// <summary>
    /// Класс объект - временной интервал ремонта
    /// </summary>
    public class Repair
    {
        private int id;

        /// <summary>
        /// Id ремонта
        /// </summary>
        public int Id
        {
            get { return id; }
            set
            {
                if (value < -1)
                {
                    id = -1;
                }
                else
                {
                    id = value;
                }
            }
        }

        private DateTime startDate;

        /// <summary>
        /// Начало ремонта
        /// </summary>
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        private DateTime endDate;

        /// <summary>
        /// Окончание ремонта
        /// </summary>
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        /// <summary>
        /// Базовый конструктор
        /// </summary>
        public Repair()
        {
            id = -1;
        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="startDate"> Начало ремонта </param>
        /// <param name="endDate"> Окончание ремонта </param>
        /// <param name="id"> Id ремонта </param>
        public Repair(DateTime startDate, DateTime endDate, int id = -1)
        {
            this.id = id;
            this.startDate = startDate;
            this.endDate = endDate;
        }
    }

    /// <summary>
    /// Класс для работы с ремонтами га
    /// </summary>
    public static class Repairs
    {
        /// <summary>
        /// Получение ремонтируемых га на заданный день
        /// </summary>
        /// <param name="Date"> День по которому смотрим ремонты </param>
        /// <returns></returns>
        public static Dictionary<int, bool> GetRepairsForDate(DateTime Date)
        {
            Dictionary<int, bool> repairs = new Dictionary<int, bool>();
            for (int i = 0; i < HydroAggregates.Aggregates.Count; i++)
            {

                for (int j = 0; j < HydroAggregates.Aggregates[i].Repairs.Count; j++)
                {
                    if (DateTime.Compare(HydroAggregates.Aggregates[i].Repairs[j].StartDate, Date) <= 0 && 
                        DateTime.Compare(HydroAggregates.Aggregates[i].Repairs[j].EndDate, Date) >= 0)
                    {
                        repairs.Add(HydroAggregates.Aggregates[i].Number, true);
                        break;
                    }
                }
            }
            return repairs;
        }
    }
}
