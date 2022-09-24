﻿

namespace AdoNetHomework.Model
{
    /// <summary>
    /// Represents one object from table 'Users';
    /// <br />
    /// Представляет собой один объект из таблицы "Users";
    /// </summary>
    public class User
    {

        #region PROPERTIES - forming the State of an Object


        private int _Id;

        private string _Name;

        private string _PhoneNumber;



        public int Id { get { return _Id; } set { _Id = value; } }

        public string Name { get { return _Name; } set { _Name = value; } }

        public string PhoneNumber { get { return _PhoneNumber; } set { _PhoneNumber = value; } }


        #endregion PROPERTIES - forming the State of an Object



        #region CONSTRUCTION - Object Lifetime Control


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public User()
        {
            this.Id = int.MaxValue;
            this.Name = "";
            this.PhoneNumber = "";
        }


        /// <summary>
        /// Constructor with parameters;
        /// <br />
        /// Конструктор с параметрами;
        /// </summary>
        /// <param name="Id">Id;<br />Id;</param>
        /// <param name="Name">Name;<br />Имя;</param>
        /// <param name="PhoneNumber">Phone number;<br />Номер телефона;</param>
        public User(string Name, string PhoneNumber) : this()
        {
            this.Name = Name;
            this.PhoneNumber = PhoneNumber;
        }


        #endregion CONSTRUCTION - Object Lifetime Control

    }
}