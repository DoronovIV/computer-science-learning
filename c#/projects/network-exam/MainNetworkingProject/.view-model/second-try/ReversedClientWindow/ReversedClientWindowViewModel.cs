﻿using System.Collections.ObjectModel;
using System.Windows;

using MainNetworkingProject.Model.Basics;

namespace MainNetworkingProject.ViewModel
{
    public class ReversedClientWindowViewModel
    {


        #region PROPERTIES - Object State


        /// <summary>
        /// Обозреваемая коллекция из моделей пользователя
        /// </summary>
        public ObservableCollection<UserModel> Users { get; set; }

        /// <summary>
        /// Обозреваемая коллекция из сообщений
        /// </summary>
        public ObservableCollection<string> Messages { get; set; }


        /// <summary>
        /// Свойство: Имя пользователя
        /// </summary>
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Свойство: Сообщение
        /// </summary>
        public string Message { get; set; } = null!;

        /// <summary>
        /// Экземпляр класса Сервер
        /// </summary>
        private ReversedService _server;


        #endregion PROPERTIES - Object State




        #region COMMANDS - Prism Commands


        /// <summary>
        /// Команда для подключения к серверу
        /// </summary>
        public RelayCommand ConnectToServerCommand { get; set; }

        /// <summary>
        /// Команда для отправки сообщения
        /// </summary>
        public RelayCommand SendMessageCommand { get; set; }


        #endregion COMMANDS - Prism Commands




        #region CONSTRUCTION - Object Lifetime


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ReversedClientWindowViewModel()
        {
            Users = new ObservableCollection<UserModel>();
            Messages = new ObservableCollection<string>();
            _server = new();

            _server.connectedEvent += ConnectUser;//подключение нового пользователя
            _server.msgReceivedEvent += RecieveMessage;//получение сообщения
            _server.userDisconnectEvent += RemoveUser;//отключение пользователя

            //Команда подключения к серверу. Если имя пользователя не будет введено в текстовое поле, то команда не выполниться.
            ConnectToServerCommand = new RelayCommand(o => _server.ConnectToServer(UserName), o => !string.IsNullOrEmpty(UserName));

            //Команда для отправки сообщения. Если сообщение не будет введено в текстовое поле, то команда не выполниться.
            SendMessageCommand = new RelayCommand(o => _server.SendMessageToServer(Message), o => !string.IsNullOrEmpty(Message));
        }


        #endregion CONSTRUCTION - Object Lifetime




        #region LOGIC - internal behavior


        /// <summary>
        /// Remove a user from the client list;
        /// <br />
        /// Удалить пользователя из списка клиентов;
        /// </summary>
        private void RemoveUser()
        {
            var uid = _server.PacketReader.ReadMessage();
            var user = Users.Where(x => x.UID == uid).FirstOrDefault();
            user = null!;
            Application.Current.Dispatcher.Invoke(() => Users.Remove(user)); // removing disconnected user;
        }


        /// <summary>
        /// Recieve user message;
        /// <br />
        /// Получить сообщение от пользователя;
        /// </summary>
        private void RecieveMessage()
        {
            var msg = _server.PacketReader.ReadMessage();                   // reading new message via our packet reader;
            Application.Current.Dispatcher.Invoke(() => Messages.Add(msg)); // adding it to the observable collection;
        }



        /// <summary>
        /// Connect new user;
        /// <br />
        /// Подключить нового пользователя;
        /// </summary>
        private void ConnectUser()
        {
            // create new user instance;
            var user = new UserModel
            {
                UserName = _server.PacketReader.ReadMessage(),
                UID = _server.PacketReader.ReadMessage(),
            };

            /*
             
           [!] In case there's no such user in collection we add them manualy;
            To prevent data duplication;
            
             */

            if (!Users.Any(x => x.UID == user.UID))
            {
                Application.Current.Dispatcher.Invoke(() => Users.Add(user));
            }
        }


        #endregion LOGIC - internal behavior


    }
}
