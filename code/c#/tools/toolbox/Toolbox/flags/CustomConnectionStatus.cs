﻿using System.ComponentModel;

namespace Tools.Flags
{
    /// <summary>
    /// Represents a custom manual set of flags, providing basic connection info;
    /// <br />
    /// Представляет собой самописный набор флагов, предоставляющий элементарную информацию о подключении;
    /// </summary>
    public class CustomConnectionStatus : INotifyPropertyChanged
    {



        #region STATE



        /// <inheritdoc cref="IsConnected"/>
        private bool _IsConnected;


        /// <inheritdoc cref="Narrative"/>
        private string _Narrative;


        /// <inheritdoc cref="IsNotConnected"/>
        private bool _IsNotConnected;




        /// <summary>
        /// True if connected, otherwise false;
        /// <br />
        /// "True" если подключено, иначе "false";
        /// </summary>
        public bool IsConnected
        {
            get { return _IsConnected; }
            private set
            {
                _IsConnected = value;
                OnPropertyChanged(nameof(IsConnected));
            }
        }


        /// <summary>
        /// True if NOT connected, otherwise false;
        /// <br />
        /// "True" если НЕ подключено, иначе "false";
        /// </summary>
        public bool IsNotConnected
        {
            get { return _IsNotConnected; }
            private set
            {
                _IsNotConnected = value;
                OnPropertyChanged(nameof(IsNotConnected));
            }
        }


        /// <summary>
        /// Verbose connection status string;
        /// <br />
        /// Строка развёрнутого статуса подключения;
        /// </summary>
        public string Narrative
        {
            get { return _Narrative; }
            private set
            {
                _Narrative = value;
                OnPropertyChanged(nameof(Narrative));
            }
        }



        #endregion STATE




        #region API - public Contract


        /// <summary>
        /// Invert the connection flags;
        /// <br />
        /// Инвертировать флаги подключения;
        /// </summary>
        public void Toggle()
        {
            var bTemp = IsConnected;
            IsConnected = IsNotConnected;
            IsNotConnected = bTemp;

            if (IsConnected) Narrative = "Connected";
            else Narrative = "Waiting ...";
        }


        #endregion API - public Contract




        #region CONSTRUCTION - Object Lifetime




        #region Property changed


        /// <summary>
        /// Propery changed event handler;
        /// <br />
        /// Делегат-обработчик события 'property changed';
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;


        /// <summary>
        /// Handler-method of the 'property changed' delegate;
        /// <br />
        /// Метод-обработчик делегата 'property changed';
        /// </summary>
        /// <param name="propName">The name of the property;<br />Имя свойства;</param>
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


        #endregion Property changed




        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public CustomConnectionStatus()
        {
            _IsConnected = false;
            _IsNotConnected = true;
            _Narrative = "Waiting ...";
        }




        /// <summary>
        /// Parametrised constructor;
        /// <br />
        /// Конструктор с параметром;
        /// </summary>
        /// <param name="ConnectionAlreadyEstablished">
        /// True if you are already connected by the time you create the object, otherwise false;
        /// <br />
        /// "True", если к моменту создания этого объекта, подключение уже установлено, иначе "false";
        /// </param>
        public CustomConnectionStatus(bool ConnectionAlreadyEstablished) : this()
        {
            if (ConnectionAlreadyEstablished)
            {
                _IsConnected = true;
                _IsNotConnected = false;
                _Narrative = "Connected";
            }
        }


        #endregion CONSTRUCTION - Object Lifetime



    }
}
