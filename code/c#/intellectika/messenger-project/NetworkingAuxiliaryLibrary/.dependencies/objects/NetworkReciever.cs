﻿using NetworkingAuxiliaryLibrary.Packages;
using NetworkingAuxiliaryLibrary.Processing;
using System.Net.Sockets;

namespace NetworkingAuxiliaryLibrary.Dependencies.DataAccess
{
    public class NetworkReciever
    {
        public IUserDataAccess User { get; set; }

        public TcpClient ClientSocket { get; set; }
        
        public PackageReader PackageReader { get; set; }

        public delegate void MessageRecievedDelegate(MessagePackage recievedMessage);

        public event MessageRecievedDelegate ProcessTextMessage;
        public event MessageRecievedDelegate SendMessageOutput;

        public void InvokeTextMessageEvents(MessagePackage recievedMessage)
        {
            ProcessTextMessage?.Invoke(recievedMessage);
            SendMessageOutput?.Invoke(recievedMessage);
        }

        public delegate void UserConnectionEvent(IUserDataAccess connectedUser);

        public event UserConnectionEvent ProcessConnection;
        public event UserConnectionEvent SendConnectionOutput;

        public void InvokeConnectionEvents()
        {

        }

        public delegate void UserDisconnectionEvent(IUserDataAccess disconnectedUser);

        public event UserDisconnectionEvent ProcessDisconnection;
        public event UserDisconnectionEvent SendDisconnectionOutput;

        public void InvokeDisconnectionEvents()
        {
            ProcessDisconnection?.Invoke(User);
            SendDisconnectionOutput?.Invoke(User);
        }


        public NetworkReciever(TcpClient client)
        {
            ClientSocket = client;

            PackageReader = new(ClientSocket.GetStream());
        }

    }
}