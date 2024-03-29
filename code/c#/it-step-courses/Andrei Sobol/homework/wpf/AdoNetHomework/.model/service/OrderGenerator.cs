﻿using AdoNetHomework.Model;
using System.Globalization;

namespace AdoNetHomework.Service
{
    /// <summary>
    /// Order generator;
    /// <br />
    /// Генератор заказов;
    /// </summary>
    public class OrderGenerator
    {


        #region PROPERTIES


        /// <summary>
        /// A reference of 'System.Random' instance;
        /// <br />
        /// Ссылка на копию класса "System.Random";
        /// </summary>
        private Random random;


        #endregion PROPERTIES




        #region API


        /// <summary>
        /// Get new random order;
        /// <br />
        /// Получить новый случайный заказ;
        /// </summary>
        /// <param name="nUserTableCount">
        /// Current users quantity;
        /// <br />
        /// Текущее кол-мо пользователей;
        /// </param>
        /// <returns></returns>
        public Order GetRandomOrder(int[] ptrUsersIds)
        {
            return new Order(id: int.MaxValue, customerId: random.Next(ptrUsersIds[0], ptrUsersIds[random.Next(0, ptrUsersIds.Length)]), summ: random.NextDouble(), dateString: DateTime.Now.ToString(CultureInfo.InvariantCulture)); ;
        }


        #endregion API




        #region CONSTRUCTION


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public OrderGenerator()
        {
            random = new Random();
        }


        #endregion CONSTRUCTION



    }
}
