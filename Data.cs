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
    /// Статичный класс с данными названий рабочих столбцов и их ключей
    /// </summary>
    public static class Data
    {
        /// <summary>
        /// Метод возвращающий путь к исполняемому файлу
        /// </summary>
        /// <returns> Путь к исполняемому файлу </returns>
        public static string GetAppPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        #region Названия для столбцов
        /// <summary>
        /// Название рабочих столбцов
        /// </summary>
        public static string[] ColumnNames = {
                                              "Дата", "УВБ, м",  // ручной ввод
                                              "УНБ, м",  // ручной ввод
                                              "Кзим",  // ручной ввод, не обязательное
                                              "Нбрутто, м", "dh, м", "Ннетто, м", 
                                              "Qга, м3/с", "Qвсп, м3/с", "Qшлюз, м3/с",  // ручной ввод
                                              "Qфильтр, м3/с", "ΣQНБ, м3/с",  // ручной ввод
                                              "Объем водохр, млн. м3", "Изменение объема водохр, млн. м3", 
                                              "Аккум, м3/с", "Приток, м3/с", // ручной ввод
                                              "Ремонты ГА"
                                          };
        #endregion

        #region Cоответствие между ключами(столбца) и названиями
        /// <summary>
        /// Словарь соответсвия рабочего столбца ключу
        /// </summary>
        public static Dictionary<string, string> GlobalNamesToKeys = new Dictionary<string, string>() {
        {"Дата", "date"},
        {"УВБ, м", "upstreamBief"},
        {"УНБ, м", "downstreamBief"},
        {"Ннетто, м", "netto"},
        {"Qга, м3/с", "qga"},
        {"ΣQНБ, м3/с", "qnb"},
        {"Qвсп, м3/с", "qvsp"},
        {"Qшлюз, м3/с", "qshluz"},
        {"Объем водохр, млн. м3", "volume"},
        {"Изменение объема водохр, млн. м3", "dV"},
        {"Аккум, м3/с", "accum"},
        {"Приток, м3/с", "pritok"},
        {"Кзим", "winterFactor"},
        {"dh, м", "dh"},
        {"Нбрутто, м", "brutto"},
        {"Qфильтр, м3/с",  "qfiltr"},
        {"Ремонты ГА", "qgarepairs"}
        };
        #endregion

        #region Cоответствие между названиями(столбца) и ключами
        /// <summary>
        /// Словарь соответсвия ключа рабочему столбцу
        /// </summary>
        public static Dictionary<string, string> GlobalKeysToNames = new Dictionary<string, string>() {
        {"date", "Дата"},
        {"upstreamBief", "УВБ, м"},
        {"downstreamBief", "УНБ, м"},
        {"netto", "Ннетто, м"},
        {"qga", "Qга, м3/с"},
        {"qnb", "ΣQНБ, м3/с"},
        {"qvsp", "Qвсп, м3/с"},
        {"qshluz", "Qшлюз, м3/с"},
        { "volume", "Объем водохр, млн. м3"},
        {"dV", "Изменение объема водохр, млн. м3"},
        {"accum", "Аккум, м3/с"},
        {"pritok", "Приток, м3/с"},
        {"winterFactor", "Кзим"},
        {"dh", "dh, м"},
        {"brutto", "Нбрутто, м"},
        {"qfiltr", "Qфильтр, м3/с"},
        {"qgarepairs", "Ремонты ГА"}
        };
        #endregion
    }
}
