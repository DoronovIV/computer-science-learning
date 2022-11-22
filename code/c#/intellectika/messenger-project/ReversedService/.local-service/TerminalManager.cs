﻿using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace ReversedService.LocalService
{
    /// <summary>
    /// An intermediate between terminal object and view model of the service window.
    /// <br />
    /// Посредник между объектом терминала и вью моделью окна сервиса.
    /// </summary>
    public class TerminalManager : INotifyPropertyChanged
    {


        #region STATE



        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                               ↓   FIELDS   ↓                             ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


        /// <inheritdoc cref="Log"/>
        private AsyncObservableCollection<string> log;


        private string line;





        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                             ↓   PROPERTIES   ↓                           ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


        /// <summary>
        /// A collection of lines of the terminal messages.
        /// <br />
        /// Коллекция строк сообщений терминала.
        /// </summary>
        public AsyncObservableCollection<string> Log
        {
            get { return log; }
            set
            {
                log = value;
                OnPropertyChanged(nameof(Log));
            }
        }


        public string Line
        {
            get { return line; }
            set
            {
                line = value;
                OnPropertyChanged(nameof(Line));
            }
        }





        #endregion STATE





        #region API


        /// <summary>
        /// Add a line to the list.
        /// <br />
        /// Добавить строку к списку.
        /// </summary>
        /// <param name="message">
        /// A line to add.
        /// <br />
        /// Строка к добавлению.
        /// </param>
        public void AddMessage(string message)
        {
            string messageClosureCopy = message; // just in case.
            Application.Current.Dispatcher.Invoke(() => { Log.Add(messageClosureCopy); });
        }


        public void AddLine()
        {
            AddMessage(Line);
            Line = string.Empty;
        }



        public void ClearLog()
        {
            Log = new();
        }


        #endregion API





        #region CONSTRUCTION



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public TerminalManager()
        {
            log = new();
        }



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



        #endregion CONSTRUCTION


    }
}
