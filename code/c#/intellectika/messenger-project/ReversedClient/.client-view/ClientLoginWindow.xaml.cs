﻿using ReversedClient.ViewModel.ClientStartupWindow;

namespace ReversedClient.client_view
{
    /// <summary>
    /// Interaction logic for ClientLoginWindow.xaml
    /// </summary>
    public partial class ClientLoginWindow : Window
    {
        public ClientLoginWindow()
        {
            InitializeComponent();

            DataContext = new ClientLoginWindowViewModel();

            Name = "ClientLoginWindow";
        }
    }
}
