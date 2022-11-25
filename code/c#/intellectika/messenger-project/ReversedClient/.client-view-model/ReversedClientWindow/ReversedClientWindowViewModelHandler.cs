﻿using ReversedClient.client_view;
using ReversedClient.Model.Basics;
using ReversedClient.ViewModel.Chatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ReversedClient.ViewModel
{
    public partial class ReversedClientWindowViewModel
    {




        #region LOGIC - internal behavior



        /// <summary>
        /// Remove a user from the client list;
        /// <br />
        /// Удалить пользователя из списка клиентов;
        /// </summary>
        private void RemoveUser()
        {
            var uid = _server.PacketReader.ReadMessage().Message;
            var user = Users.Where(x => x.UID == uid).FirstOrDefault();

            // foreach (var user in )
            Application.Current.Dispatcher.Invoke(() => Users.Remove(user));   // removing disconnected user;
        }



        /// <summary>
        /// Recieve user message;
        /// <br />
        /// Получить сообщение от пользователя;
        /// </summary>
        private void RecieveMessage()
        {
            try 
            {
                var msg = _server.PacketReader.ReadMessage();                                        // reading new message via our packet reader;
                Application.Current.Dispatcher.Invoke(() => activeChat.AddIncommingMessage(msg.Message as string));    // adding it to the observable collection;
            }
            catch (Exception ex)
            {

            }
        }



        /// <summary>
        /// Recieve a file from the network stream.
        /// <br />
        /// Получить файл из сетевого стрима.
        /// </summary>
        private void RecieveFile()
        {
            Application.Current.Dispatcher.Invoke(() => activeChat.MessageList.Add("File recieved."));
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
        }



        /// <summary>
        /// Connect new user;
        /// <br />
        /// Подключить нового пользователя;
        /// </summary>
        public void ConnectUser()
        {
            // create new user instance;
            var user = new UserModel()
            {
                UserName = _server.PacketReader.ReadMessage().Message as string,
                UID = _server.PacketReader.ReadMessage().Message as string,
            };

            /*
             
           [!] In case there's no such user in collection we add them manualy;
            To prevent data duplication;
            
             */

            MessengerChat newChat;
            if (!Users.Any(x => x.UID == user.UID))
            {
                if (user.UID != currentUser.UID)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Users.Add(user);
                    });
                    newChat = new MessengerChat(addressee: user, addresser: CurrentUser);
                    chatList.Add(newChat);
                }
            }
        }



        public void DisconnectFromServer()
        {

            Process.GetProcesses().ToList().FindAll(n => n.ProcessName == "ReversedClient")?.ForEach(p => p.Kill());
            //
        }



        /// <summary>
        /// Open file dialog to choose a file to send.
        /// <br />
        /// Открыть файловый диалог, чтобы выбрать файл к отправке.
        /// </summary>
        public void SelectFile()
        {
            try
            {
                if (_DialogService.OpenFileDialog())
                {
                    UserFile = new(_DialogService.FilePath);
                }
            }
            catch (Exception ex)
            {
                _DialogService.ShowMessage(ex.Message);
            }
        }



        /// <summary>
        /// Send a message to the service;
        /// <br />
        /// Is needed to nullify the chat message field after sending;
        /// <br />
        /// <br />
        /// Отправить сообщение на сервис;
        /// <br />
        /// Необходимо, чтобы стереть сообщение после отправкиж
        /// </summary>
        private void SendMessage()
        {
            try
            {
                if (Message != string.Empty)
                {
                    _server.SendMessageToServer(new TextMessagePackage(currentUser.UserName, SelectedContact.UserName, Message));
                    Message = string.Empty;
                }

                if (UserFile != null)
                {
                    _server.SendFileToServerAsync(new FileMessagePackage(currentUser.UserName, SelectedContact.UserName, UserFile));
                    Application.Current.Dispatcher.Invoke(() => activeChat.MessageList.Add($"File sent."));
                    UserFile = null;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please, choose a contact.", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }


        private void ConnectToService()
        {
            _server.ConnectToServer(currentUser.UserName);
        }



        private void ShowErrorMessage(string sMessage)
        {
            MessageBox.Show(sMessage, "Exception", MessageBoxButton.OK, MessageBoxImage.Information);
        }



        #endregion LOGIC - internal behavior






        #region HANDLERS



        /// <summary>
        /// Handle the 'Sign In' button;
        /// <br />
        /// Обработать клик по кнопке 'Sign In';
        /// </summary>
        public void OnSignInButtonClick()
        {
            try
            {
                // Debug feature [!]
                currentUser.UID = currentUser.UserName;

                chatWindowReference = new();
                loginWindowReference = new();

                Server.ConnectToServer(currentUser.UserName);

                //// [!] In this particular order;
                //
                WindowHeaderString = currentUser.UserName + " - common chat";
                chatWindowReference.Show();
                //

                loginWindowReference.Close();
                Application.Current.MainWindow.Close();
                Application.Current.MainWindow = chatWindowReference;
                // ===============================
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to connect.\n\nException: {ex.Message}", "Exception intercepted", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        /// <summary>
        /// Users collection changed handler.
        /// <br />
        /// Обработчик изменения коллекции пользователей.
        /// </summary>
        public void OnUsersCollectionChanged(object? sender, EventArgs e)
        {
            if (Users.Count != 1) TheContactsString = "contacts";
            else TheContactsString = "contact";
        }



        #endregion HANDLERS




    }
}
