﻿using NetworkingAuxiliaryLibrary.Processing;
using System.Net;
using System.Net.Sockets;
using System.Windows;

namespace NetworkingAuxiliaryLibrary.ClientService
{
    /// <summary>
    /// A service that manages both connections and reading/writing data on lower level;
    /// <br />
    /// Сервис, который отвечает за подключения и чтение/запись данных на более низком уровне;
    /// </summary>
    public class ReversedService
    {


        #region PROPERTIES - public & private Properties



        //
        // EndPoint
        //

        /// <summary>
        /// Current service EndPoint;
        /// <br />
        /// Текущий эндпоинт сервиса;
        /// </summary>
        private IPEndPoint ourEndPoint = new(localHostIpAddress, 7891);


        /// <summary>
        /// Localhost address;
        /// <br />
        /// Адрес локалхоста;
        /// </summary>
        private static IPAddress localHostIpAddress = IPAddress.Parse("127.0.0.1");


        /// <summary>
        /// A field you can insert your address into;
        /// <br />
        /// Поле, в которое вы можете вписать свой адрес;
        /// </summary>
        private static IPAddress otherHostIpAddress = IPAddress.Parse("127.0.0.1");

        //
        // /EndPoint
        //




        /// <summary>
        /// Provides client connections fo the service;
        /// <br />
        /// Предоставляет клиенские подключения для сервиса;
        /// </summary>
        private TcpClient _client;



        /// <summary>
        /// An auxiliary object to make reading/writing messages easier;
        /// <br />
        /// Вспомогательный объект, который необходим, чтобы упростить чтение/запись сообщений;
        /// </summary>
        public PackageReader PacketReader;



        /// <summary>
        /// User connection Event;
        /// <br />
        /// Событие подключения пользователя;
        /// </summary>
        public event Action connectedEvent;



        /// <summary>
        /// Message recievment event;
        /// <br />
        /// Событие получения сообщения;
        /// </summary>
        public event Action msgReceivedEvent;


        public event Action fileReceivedEvent;



        /// <summary>
        /// User disconnection event;
        /// <br />
        /// Событие отключения пользователя;
        /// </summary>
        public event Action userDisconnectEvent;





        /// <summary>
        /// A delegate for transeffring output to other objects;
        /// <br />
        /// Делегат для передачи аутпута другим объектам;
        /// </summary>
        /// <param name="sOutputMessage">
        /// A message that we want to see somewhere (в данном случае, в консоли сервера и в пользовательском клиенте);
        /// <br />
        /// Сообщение, которое мы хотим где-то увидеть (в данном случае, в консоли сервера и в пользовательском клиенте);
        /// </param>
        public delegate void PendOutputDelegate(string sOutputMessage);

        /// <summary>
        /// @see public delegate void PendOutputDelegate(string sOutputMessage);
        /// </summary>
        public event PendOutputDelegate SendOutput;




        #endregion PROPERTIES - public & private Properties





        #region API - public Behavior



        /// <summary>
        /// Connect user to the service;
        /// <br />
        /// Подключить пользователя к сервису;
        /// </summary>
        /// <param name="userName">
        /// The nickname of the user chosen on login;
        /// <br />
        /// Никнейм, который пользователь выбрал на логине;
        /// </param>
        public void ConnectToServer(string userName)
        {
            //Если клиент не подключен
            if (!_client.Connected)
            {
                /// 
                /// - Client connection [!]
                ///
                _client.Connect(ourEndPoint);
                PacketReader = new(_client.GetStream());

                if (!string.IsNullOrEmpty(userName))
                {
                    var connectPacket = new PackageBuilder();

                    connectPacket.WriteOpCode(0);

                    connectPacket.WriteMessage(userName);

                    _client.Client.Send(connectPacket.GetPacketBytes());

                    connectPacket = new();
                }

                ReadPackets();
            }
            
        }



        /// <summary>
        /// Disconnect client from service;
        /// <br />
        /// Отключить клиент от сервиса;
        /// </summary>
        public void Disconnect()
        {
            if (_client.Connected)
            {
                _client.Close();
            }
        }



        /// <summary>
        /// Send user's message to the service;
        /// <br />
        /// Отправить сообщение пользователя на сервис;
        /// </summary>
        /// <param name="message">
        /// User's message;
        /// <br />
        /// Сообщение пользователя;
        /// </param>
        public void SendMessageToServer(string message)
        {
            try
            {
                var messagePacket = new PackageBuilder();
                messagePacket.WriteOpCode(5);
                messagePacket.WriteMessage(message);
                _client.Client.Send(messagePacket.GetPacketBytes());
                messagePacket = new();
            }
            catch (Exception ex)
            {
                SendOutput.Invoke($"You haven't connected yet.\n\nException: {ex.Message}");
            }
        }



        public void SendFileToServer(FileInfo info)
        {
            try
            {
                var messagePacket = new PackageBuilder();
                messagePacket.WriteOpCode(6);
                messagePacket.WriteFile(info);
                _client.Client.Send(messagePacket.GetPacketBytes());
                messagePacket = new();
            }
            catch (Exception ex)
            {
                SendOutput.Invoke($"You haven't connected yet.\n\nException: {ex.Message}");
            }
        }



        #endregion API - public Behavior





        #region LOGIC - internal Behavior



        /// <summary>
        /// Read the incomming packet. A packet is a specific message, sent by ServiceHub to handle different actions;
        /// <br />
        /// Прочитать входящий пакет. Пакет - это специальное сообщение, отправленное объектом "ServiceHub", чтобы структурировать обработку разных событий;
        /// </summary>
        private void ReadPackets()
        {
            Task.Run(() =>
            {
                byte opCode = 0;
                while (true)
                {
                    //код операции;
                    try
                    {
                        opCode = PacketReader.ReadByte();
                    }
                    catch (Exception e)
                    {
                        userDisconnectEvent?.Invoke();
                    }
                    switch (opCode)
                    {

                        case 1:
                            connectedEvent?.Invoke();// client connection;
                            break;

                        case 5:
                            msgReceivedEvent?.Invoke(); // message recieved;
                            break;

                        case 6:
                            fileReceivedEvent?.Invoke(); // file  recieved;
                            break;

                        case 10:
                            userDisconnectEvent?.Invoke();// client disconnection;
                            break;

                        default:
                            SendOutput.Invoke("Operation code out of [1,5,6,10]. This is a debug message.\nproject: ReversedClient, class: ReversedService, method: ReadPackets.");
                            break;
                    }
                }
            });
        }



        #endregion LOGIC - internal Behavior





        #region CONSTRUCTION - Object Lifetime


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ReversedService()
        {
            _client = new TcpClient();
        }


        #endregion CONSTRUCTION - Object Lifetime



    }
}
