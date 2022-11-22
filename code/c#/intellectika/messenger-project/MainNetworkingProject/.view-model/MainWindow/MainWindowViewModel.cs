﻿using Prism.Commands;
using Tools.Toolbox;

namespace MainNetworkingProject.ViewModel.MainWindow
{
    /// <summary>
    /// View-model of the main window;
    /// <br />
    /// Вью-модель основного окна;
    /// </summary>
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {



        #region COMMANDS


        public DelegateCommand LaunchClientCommand { get; }
        public DelegateCommand LaunchServiceCommand { get; }
        public DelegateCommand KillServiceCommand { get; }
        public DelegateCommand ClearFoldersCommand { get; }


        #endregion COMMANDS





        #region XTRACTION


        /// <summary>
        /// Extract networking and tools libraries.
        /// <br />
        /// Вытянуть файлы библиотек "tools" и "networking".
        /// </summary>
        private void ExtractLibraries()
        {
            try
            {
                /// Toolbox

                // target;
                FileInfo dllTargetInfo = new(@"C:\Users\i.doronov\source\repos\computer-science-learning\code\c#\tools\toolbox\Toolbox\bin\Debug\net6.0\Toolbox.dll");
                // destination (precomplied dll folder);
                List<DirectoryInfo> toolsExtractorDestinations = new();
                toolsExtractorDestinations.Add(new DirectoryInfo(@"C:\Users\i.doronov\source\repos\computer-science-learning\code\c#\tools\precompiled-dll"));
                // extracting;
                LibraryExtractor toolsLibraryExtractor = new(dllTargetInfo, toolsExtractorDestinations);
                toolsLibraryExtractor.Extract();


                /// NET

                // target;
                FileInfo targetInfo = new("C:\\Users\\i.doronov\\source\\repos\\computer-science-learning\\code\\c#\\" +
        "it-step-courses\\exams\\2. network-exam\\NetworkingAuxiliaryLibrary\\bin\\Release\\net6.0\\NetworkingAuxiliaryLibrary.dll");
                // destination 1 (client);
                List<DirectoryInfo> extractorDestinations = new();
                extractorDestinations.Add(new DirectoryInfo("C:\\Users\\i.doronov\\source\\repos\\computer-science-learning" +
                    "\\code\\c#\\it-step-courses\\exams\\2. network-exam\\ReversedClient\\.net"));
                // destination 2 (service);
                extractorDestinations.Add(new DirectoryInfo("C:\\Users\\i.doronov\\source\\repos\\computer-science-learning" +
        "\\code\\c#\\it-step-courses\\exams\\2. network-exam\\ReversedService\\.net"));
                // extracting;
                LibraryExtractor networkingLibraryExtractor = new(targetInfo, extractorDestinations);
                networkingLibraryExtractor.Extract();
            }
            catch { }
        }


        #endregion XTRACTION





        #region CONSTRUCTION




        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public MainWindowViewModel()
        {
            ExtractLibraries();

            LaunchClientCommand = new(OnLaunchClientButtonClickAsync);
            LaunchServiceCommand = new(OnLaunchServiceButtonClickAsync);
            KillServiceCommand = new(OnKillServiceButtonClick);
            ClearFoldersCommand = new(OnClearFoldersButtonClickInParallel);
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