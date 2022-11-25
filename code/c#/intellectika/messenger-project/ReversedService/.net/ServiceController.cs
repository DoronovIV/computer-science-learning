﻿using ReversedService.ViewModel.ServiceWindow;
using System.ComponentModel;
using System.IO.Packaging;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace NetworkingAuxiliaryLibrary.ClientService
{
    /// <summary>
    /// A controller for ServiceReciever instance to broadcast recieved data to every users.
    /// <br />
    /// Контроллер экземпляра "ServiceReciever" для рассылки полученных данных всем пользователям
    /// </summary>
    public class ServiceController : INotifyPropertyChanged
    {


        #region PROPERTIES - State of an Object



        /// <summary>
        /// A list of current users;
        /// <br />
        /// Актуальный список пользователей;
        /// </summary>
        private List<ServiceReciever> _UserList = null!;


        /// <summary>
        /// The main TCP listener;
        /// <br />
        /// Основной слушатель;
        /// </summary>
        private TcpListener _Listener = null!;


        /// <inheritdoc cref="IsRunning"/>
        private bool _isRunning;


        /// <summary>
        /// Is service running;
        /// <br />
        /// Работает ли сервис;
        /// </summary>
        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                OnPropertyChanged(nameof(IsRunning));
            }
        }





        /// <summary>
        /// A delegate for transeffring output to other objects;
        /// <br />
        /// Делегат для передачи аутпута другим объектам;
        /// </summary>
        /// <param name="sOutputMessage">
        /// A message that we want to see somewhere (In this case, in a server console);
        /// <br />
        /// Сообщение, которое мы хотим где-то увидеть (в данном случае, в консоли сервера);
        /// </param>
        public delegate void ServiceOutputDelegate(string sOutputMessage);


        /// <inheritdoc cref="ServiceOutputDelegate"/>
        public event ServiceOutputDelegate SendServiceOutput;



        #endregion PROPERTIES - State of an Object




        #region API - public Behavior



        /// <summary>
        /// Run controller;
        /// <br />
        /// Запустить контроллер;
        /// </summary>
        public void Run()
        {
            try
            {
                _UserList = new List<ServiceReciever>();

                _Listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7891);

                _Listener.Start();

                if (ServiceWindowViewModel.cancellationTokenSource.IsCancellationRequested)
                    ServiceWindowViewModel.cancellationTokenSource = new();

                ServiceReciever client;

                IsRunning = true;

                while (!ServiceWindowViewModel.cancellationTokenSource.IsCancellationRequested)
                {
                    client = new ServiceReciever(_Listener.AcceptTcpClient(), this);
                    _UserList.Add(client);
                    BroadcastConnection();
                }
            }
            catch { }
        }


        /// <summary>
        /// Stop service.
        /// <br />
        /// Остановить сервис.
        /// </summary>
        public void Stop()
        {
            _Listener.Stop();

            _UserList.ForEach(u => u.ClientSocket.Close());

            _UserList = new();

            IsRunning = false;
        }



        /// <summary>
        /// Broadcast a notification message to all users about the new user connection;
        /// <br />
        /// Транслировать уведомление для всех пользователей о подключении нового пользователя;
        /// </summary>
        public void BroadcastConnection()
        {
            var broadcastPacket = new PackageBuilder();
            foreach (var user in _UserList)
            {
                foreach (var usr in _UserList)
                {
                    broadcastPacket.WriteOpCode(1); // code '1' means 'ew user have connected;
                    broadcastPacket.WriteMessage(new TextMessagePackage(usr.CurrentUID, "@All",usr.CurrentUserName));
                    broadcastPacket.WriteMessage(new TextMessagePackage(usr.CurrentUID, "@All", usr.CurrentUID.ToString()));

                    user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
                }
            }
        }



        /// <summary>
        /// Send message to all users. Mostly use to deliver one user's message to all other ones;
        /// <br />
        /// Отправить сообщение всем пользователям. В основном, используется, чтобы переслать сообщение одного пользователя всем остальным;
        /// </summary>
        /// <param name="message"></param>
        public void BroadcastMessage(MessagePackage package)
        {
            var msgPacket = new PackageBuilder();
            msgPacket.WriteOpCode(5);
            msgPacket.WritePackageLength(package);
            msgPacket.WriteMessage(package);
            foreach (var user in _UserList)
            {
                if (package.Reciever != "@All")
                {
                    if (user.CurrentUID == package.Reciever || user.CurrentUID == package.Sender)
                    {
                        user.ClientSocket.Client.Send(msgPacket.GetPacketBytes(), SocketFlags.Partial);
                    
                    }
                }
                else user.ClientSocket.Client.Send(msgPacket.GetPacketBytes(), SocketFlags.Partial);
            }
        }



        /// <summary>
        /// Send file to all clients.
        /// <br />
        /// Разослать файл всем клиентам.
        /// </summary>
        /// <param name="info">
        /// Broadcasted file.
        /// <br />
        /// Транслируемый файл.
        /// </param>
        /// <param name="SenderId">
        /// The id of the sender-user.
        /// <br />
        /// Идентификатор отправителя.
        /// </param>
        public void BroadcastFileInParallel(FileMessagePackage filePackage)
        {
            var msgPacket = new PackageBuilder();
            msgPacket.WriteOpCode(6);
            msgPacket.WriteMessage(filePackage);

            var bytes = msgPacket.GetPacketBytes();
            const int bufferSize = 4096;
            byte[] buffer;
            Parallel.ForEach(_UserList, (user) =>
            {
                if (user.CurrentUID != filePackage.Sender && user.CurrentUID == filePackage.Reciever)
                {
                    if (bytes.Length > bufferSize)
                    {
                        using (MemoryStream stream = new(bytes))
                        {
                            while (stream.Position != stream.Length)
                            {
                                if (stream.Length - stream.Position >= bufferSize)
                                    buffer = new byte[bufferSize];
                                else
                                {
                                    buffer = new byte[stream.Length - stream.Position];
                                }
                                stream.Read(buffer, 0, buffer.Length);
                                user.ClientSocket.Client.Send(buffer, SocketFlags.Partial);
                                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
                            }
                        }
                    }
                    else user.ClientSocket.Client.Send(msgPacket.GetPacketBytes(), SocketFlags.Partial);
                }
            });
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
        }



        /// <summary>
        /// Broadcast a notification message to all users about some user disconnection;
        /// <br />
        /// Транслировать уведомление для всех пользователей о том, что один из пользователей отключился;
        /// </summary>
        /// <param name="uid">
        /// id of the user in order to find and delete him from the user list;
        /// <br />
        /// id пользователя, чтобы найти его и удалить из списка пользователей;
        /// </param>
        public void BroadcastDisconnect(string uid)
        {
            var disconnectedUser = _UserList.Where(x => x.CurrentUID.ToString() == uid).FirstOrDefault();
            _UserList.Remove(disconnectedUser);            // removing user;

            var broadcastPacket = new PackageBuilder();
            foreach (var user in _UserList)
            {
                broadcastPacket.WriteOpCode(10);    // on user disconnection, service recieves the code-10 operation and broadcasts the "disconnect message";  
                broadcastPacket.WriteMessage(new TextMessagePackage(uid, "@All", uid)); // it also passes disconnected user id (not sure where that goes, mb viewmodel delegate) so we can pull it out from users list;
                user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes(), SocketFlags.Partial);
            }

            BroadcastMessage(new TextMessagePackage(disconnectedUser.CurrentUID, "@All" ,$"{disconnectedUser.CurrentUserName} Disconnected!"));
        }



        /// <summary>
        /// Pass the message out to another object that might have the ability to output this message.
        /// <br />
        /// Передать сообщение другому объекту, который может иметь возможность вывести его куда-нибудь.
        /// </summary>
        /// <param name="sMessage">
        /// Message text.
        /// <br />
        /// Текст сообщения.
        /// </param>
        public void PassOutputMessage(string sMessage)
        {
            SendServiceOutput.Invoke(sMessage);
        }



        #endregion API - public Behavior




        #region CONSTRUCTION - Object Lifetime



        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ServiceController()
        {
            _UserList = new List<ServiceReciever>();
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



        #endregion CONSTRUCTION - Object Lifetime


    }
}