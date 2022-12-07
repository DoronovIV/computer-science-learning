﻿using Net.Transmition;
using NetworkingAuxiliaryLibrary.Objects.Entities;
using ReversedClient.ViewModel.ClientChatWindow;
using ReversedClient.ViewModel.Misc;

namespace ReversedClient.client_view
{
    /// <summary>
    /// Interaction logic for ReversedClientWindow.xaml
    /// </summary>
    public partial class ReversedClientWindow : Window
    {
        public ReversedClientWindow()
        {
            InitializeComponent();

            DataContext = new ReversedClientWindowViewModel();

            SetDefaultName();
        }


        public ReversedClientWindow(User userData, ClientTransmitter clientRadio)
        {
            InitializeComponent();

            DataContext = new ReversedClientWindowViewModel(userData, clientRadio);

            SetDefaultName();
        }


        public void SetDefaultName()
        {
            Name = "ReversedClientWindow";
        }
    }
}
