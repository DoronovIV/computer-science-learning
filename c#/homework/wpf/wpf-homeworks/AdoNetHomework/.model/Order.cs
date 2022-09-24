﻿

namespace AdoNetHomework.Model
{
    /// <summary>
    /// Represents one object from table 'Orders';
    /// <br />
    /// Представляет собой один объект из таблицы "Orders";
    /// </summary>
    public class Order
    {

        #region PROPERTIES - forming the State of an Object



        #region Private references


        /// <summary>
        /// 'Orders' table primary key id;
        /// <br />
        /// Идентификатор, первичный ключ для таблицы 'Orders';
        /// </summary>
        private int _Id;


        /// <summary>
        /// 'Orders' table customers' id;
        /// <br />
        /// Идентификатор клиентов для таблицы 'Orders';
        /// </summary>
        private int _CustomerId;


        /// <summary>
        /// 'Orders' table order price value;
        /// <br />
        /// Показатель суммы заказа для таблицы 'Orders';
        /// </summary>
        private double _Summ;


        /// <summary>
        /// 'Orders' table date reference;
        /// <br />
        /// Заметка о дате заказа для таблицы 'Orders';
        /// </summary>
        private DateOnly _Date;


        #endregion Private references




        #region Public properties


        /// <summary>
        /// @see private int _Id in this file in 'Private references' region;
        /// </summary>
        public int Id { get { return _Id; } set { _Id = value; } }


        /// <summary>
        /// @see private int _CustomerId in this file in 'Private references' region;
        /// </summary>
        public int CustomerId { get { return _CustomerId; } set { _CustomerId = value; } }


        /// <summary>
        /// @see private double _Summ in this file in 'Private references' region;
        /// </summary>
        public double Summ { get { return _Summ; } set { _Summ = value; } }


        /// <summary>
        /// @see private DateOnly _Date in this file in 'Private references' region;
        /// </summary>
        public DateOnly Date { get { return _Date; } set { _Date = value; } }


        #endregion Public properties



        #endregion PROPERTIES - forming the State of an Object



        #region CONSTRUCTION - Object Lifetime Control


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public Order()
        {
            Id = int.MaxValue;
            CustomerId = int.MaxValue;
            Summ = double.MaxValue;
            
            // it sort of parses 'DateTime.Now' into 'DateOnly'-like format;
            Date = new DateOnly(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day);
        }




        #region OVERLOADING - Parameters Overloading


        /// <summary>
        /// Constructor with all the properties;
        /// <br />
        /// Конструктор со всеми параметрами;
        /// </summary>
        /// <param name="id">
        /// 'Orders' table primary key id;
        /// <br />
        /// Идентификатор, первичный ключ таблицы "Orders";
        /// </param>
        /// <param name="customerId">
        /// 'Orders' table customer's id;
        /// <br />
        /// Иднтификатор клиента из таблицы "Orders";
        /// </param>
        /// <param name="summ">
        /// 'Orders' table price value;
        /// <br />
        /// Показатель стоимости заказа из таблицы "Orders";
        /// </param>
        /// <param name="date">
        /// 'Orders' table date reference;
        /// <br />
        /// Заметка о дате из таблицы "Orders";
        /// </param>
        public Order(int id, int customerId, double summ, DateOnly date)
        {
            Id = id;
            CustomerId = customerId;
            Summ = summ;
            Date = date;
        }


        /// <summary>
        /// @see Order(int, int, double, DateOnly) in this file right above this method;
        /// <br />
        /// It's a copy of the previous one but with 'DateTime.Now' parameter;
        /// <br />
        /// Это просто копия предыдущего, только с параметром "DateTime.Now";
        /// </summary>
        public Order(int id, int customerId, double summ, DateTime DateTimeNow)
        {
            Id = id;
            CustomerId = customerId;
            Summ = summ;
            Date = new DateOnly(DateTimeNow.Date.Year, DateTimeNow.Date.Month, DateTimeNow.Date.Day);
        }


        #endregion OVERLOADING - Parameters Overloading




        #endregion CONSTRUCTION - Object Lifetime Control

    }
}