﻿using Newtonsoft.Json;
using System.Net;
using Tools.Toolbox;

namespace NetworkingAuxiliaryLibrary.Net.Config
{
    /// <summary>
    /// Tip: 0'th address is client's one, 1st - authorizer, 2nd - messenger.
    /// <br />
    /// Подсказка: 0-й адрес клиентский, первый - авторизатора, второй - месенжера.
    /// </summary>
    public static class NetworkConfigurator
    {


        #region PORTS

        private static int _authorizerMessengerPort = 7111;
        private static int _clientAuthorizerPort = 7222;
        private static int _clientMessengerPort = 7333;

        public static int AuthorizerMessengerPort { get => _authorizerMessengerPort; }
        public static int ClientAuthorizerPort { get => _clientAuthorizerPort; }
        public static int ClientMessengerPort { get => _clientMessengerPort; }

        #endregion PORTS




        #region ADDRESSES

        private static IPAddress? _clientIPAddress = null;
        private static IPAddress? _messengerIPAddress = null;
        private static IPAddress? _authorizerIPAddress = null;


        public static IPAddress ClientIPAddress 
        {
            get
            {
                if (_clientIPAddress is null)
                {
                    ReadJson();
                }
                return _clientIPAddress;
            }
            private set
            {
                _clientIPAddress = value;
            }
        }
        public static IPAddress MessengerIPAddress 
        { 
            get
            {
                if (_messengerIPAddress is null)
                {
                    ReadJson();
                }
                return _messengerIPAddress;
            } 
            private set
            {
                _messengerIPAddress = value;
            }
        }
        public static IPAddress AuthorizerIPAddress 
        {
            get
            {
                if (_authorizerIPAddress is null)
                {
                    ReadJson();
                }
                return _authorizerIPAddress;
            } 
            private set
            {
                _authorizerIPAddress = value;
            }
        }

        #endregion ADDRESSES




        #region ENDPOINTS

        private static IPEndPoint _clientMessengerEndPoint = new(ClientIPAddress, _clientMessengerPort);
        private static IPEndPoint _clientAuthorizerEndPoint = new(ClientIPAddress, _clientAuthorizerPort);
        private static IPEndPoint _authorizerMessengerEndPoint = new(MessengerIPAddress, _authorizerMessengerPort);

        public static IPEndPoint ClientMessengerEndPoint { get => _clientMessengerEndPoint; }
        public static IPEndPoint ClientAuthorizerEndPoint { get => _clientAuthorizerEndPoint; }
        public static IPEndPoint AuthorizerMessengerEndPoint { get => _authorizerMessengerEndPoint; }


        #endregion ENDPOINTS




        #region AUXILIARY


        private static void ReadJson()
        {
            var json = File.ReadAllText(@$"..\..\..\.config\network-config.json");

            var config = JsonConvert.DeserializeObject<NetworkConfiguration>(json);

            if (config is not null)
            {
                _clientIPAddress = IPAddress.Parse(Utilizer.GetLocalIPAddress());
                _authorizerIPAddress = IPAddress.Parse(config.addresses[1]);
                _messengerIPAddress = IPAddress.Parse(config.addresses[2]);
            }
        }


        #endregion AUXILIARY


    }
}
