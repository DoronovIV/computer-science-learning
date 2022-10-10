﻿using MainNetworkingProject.Model.Basics;

namespace MainNetworkingProject.ViewModel.ClientWindow
{
    public partial class ClientWindowViewModel : INotifyPropertyChanged
    {


        #region PROPERTIES



        private ClientWindowViewModelHandler _Handler;

        public ClientWindowViewModelHandler Handler
        {
            get { return _Handler; }
            set
            {
                _Handler = value;
                OnPropertyChanged(nameof(Handler));
            }
        }



        private List<string> _ChatLog;

        public List<string> ChatLog
        {
            get { return _ChatLog; }
            set
            {
                _ChatLog = value;
                OnPropertyChanged(nameof(ChatLog));
            }
        }



        private string _UserMessage;

        public string UserMessage
        {
            get { return _UserMessage; }
            set
            {
                UserMessage = value;
                OnPropertyChanged(nameof(UserMessage));
            }
        }

        private ServiceUser _User;


        public ServiceUser User
        {
            get { return _User; }
            set
            {
                _User = value;
                OnPropertyChanged(nameof(User));
            }
        }



        private ExplorerClient _ExplorerClient;


        public ExplorerClient ExplorerClient
        {
            get { return _ExplorerClient; }
            set
            {
                _ExplorerClient = value;
                OnPropertyChanged(nameof(ExplorerClient));
            }
        }


        #endregion PROPERTIES




        #region COMMANDS


        public DelegateCommand SendMessageCommand { get; }


        public DelegateCommand ConnectCommand { get; }


        #endregion COMMANDS




        #region CONSTRUCTION





        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ClientWindowViewModel()
        {
            _Handler = new();
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
