using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TestPathDesigner.ConnectionStatusLibrary.Enums;
using TestPathDesigner.Testing;

namespace TestPathDesigner.App.ViewModels
{
    internal class MainPageViewModel : BaseViewModel
    {
        private DispatcherTimer _connectionStatusTimer;
        private ConnectionStatusEnum _connectionStatus;
        public ConnectionStatusEnum ConnectionStatus
        {
            set
            {
                _connectionStatus = value;
                OnPropertyChanged(nameof(ConnectionStatus));
            }
            get
            {
                return _connectionStatus;
            }
        }
        private ObservableCollection<TestModel> _createdPath = new ObservableCollection<TestModel>();
        public ObservableCollection<TestModel> CreatedPath
        {
            set
            {
                _createdPath = value;
                OnPropertyChanged(nameof(CreatedPath));
            }
            get
            { 
                return _createdPath;
            }
        }
        private ObservableCollection<string> _logs;
        public ObservableCollection<string> Logs
        {
            set
            {
                _logs = value;
                OnPropertyChanged(nameof(Logs));
            }
            get
            {
                return _logs;
            }
        }
        private string _appName;
        public string AppName
        {
            set
            {
                _appName = value;
                OnPropertyChanged(nameof(AppName));
            }
            get
            {
                return _appName;
            }
        }
        private string _elementName;
        public string ElementName
        {
            set
            {
                _elementName = value;
                OnPropertyChanged(nameof(ElementName));
            }
            get
            {
                return _elementName;
            }
        }
        private ElementTypeEnum _elementType;
        public ElementTypeEnum ElementType
        {
            set
            {
                _elementType = value;
                OnPropertyChanged(nameof(ElementType));
            }
            get
            {
                return _elementType;
            }
        }
        public List<ElementTypeEnum> Types
        {
            get
            {
                return new List<ElementTypeEnum>
                {
                    ElementTypeEnum.FindElementByName,
                    ElementTypeEnum.FindElementByAccessibilityId
                };
            }
        }
        public ICommand StartTestingCommand { get; set; }
        public ICommand AddNewTestCommand { get; set; }
        public MainPageViewModel()
        {
            InitializeProperties();
            InitializeCommands();
            InitializeTimers();
        }
        private void InitializeProperties()
        {
            Logs = new ObservableCollection<string>();
            ConnectionStatus = ConnectionStatusEnum.Disconnected;

            
        }
        private void InitializeTimers()
        {
            _connectionStatusTimer = new System.Windows.Threading.DispatcherTimer();
            _connectionStatusTimer.Tick += new EventHandler(CheckConnectionStatus);
            _connectionStatusTimer.Interval = TimeSpan.FromSeconds(1);
            _connectionStatusTimer.Start();
        }
        public void InitializeCommands()
        {
            StartTestingCommand = new RelayCommand(async () => await StartTesting());
            AddNewTestCommand = new RelayCommand(AddNewTest);
        }
        private async Task StartTesting()
        {
            var appiumTest = new AppiumTesting(AppName);
            await appiumTest.StartTesting(CreatedPath, LogToList);

        }
        private void AddNewTest()
        {
            CreatedPath.Add(new TestModel(ElementName,ElementType, ActionEnum.Click));
            ElementName = "";
        }
        private void LogToList(string element)
        {
            Add(element);
        }
        private void Add(string element)
        {
            Application.Current.Dispatcher.Invoke(() => Logs.Add(element));
        }
        private void CheckConnectionStatus(object sender, EventArgs e)
        {
            var rand = new Random();
            ConnectionStatusEnum status = (ConnectionStatusEnum)rand.Next(0, 4);
            ConnectionStatus = status;
            //CommandManager.InvalidateRequerySuggested();
        }

    }
}
