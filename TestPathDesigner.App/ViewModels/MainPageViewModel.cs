using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using Services;
using Services.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TestPathDesigner.App.Commands;
using TestPathDesigner.ConnectionStatusLibrary.Enums;
using TestPathDesigner.Testing;
using TestPathDesigner.Testing.Models;

namespace TestPathDesigner.App.ViewModels
{
    internal class MainPageViewModel : BaseViewModel, ICRUDCommands
    {
        private DispatcherTimer _connectionStatusTimer;
        private ConnectionStatusEnum _connectionStatus;
        private IConnectionService _connectionStatusService;
        private bool _isAppSet;
        public bool IsAppSet
        {
            set
            {
                _isAppSet = value;
                OnPropertyChanged(nameof(IsAppSet));
            }
            get
            {
                return _isAppSet;
            }
        }
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
        private AppModel _appModelSelected;
        public AppModel AppModelSelected
        {
            set
            {
                _appModelSelected = value;
                OnPropertyChanged(nameof(AppModelSelected));
            }
            get 
            { 
                return _appModelSelected;
            }
        }
        private ObservableCollection<AppModel> _appModels = new ObservableCollection<AppModel>();
        public ObservableCollection<AppModel> AppModels
        {
            set
            {
                _appModels = value;
                OnPropertyChanged(nameof(AppModels));
            }
            get
            { 
                return _appModels;
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
                AppModelSelected = new AppModel();
                AppModelSelected.PackageFamilyName = value;
                OnPropertyChanged(nameof(AppName));
            }
            get
            {
                return AppModelSelected?.PackageFamilyName + "!app";
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
                    ElementTypeEnum.FindElementByAccessibilityId,
                    ElementTypeEnum.Printscreen,
                    ElementTypeEnum.Wait
                };
            }
        }
        private ActionEnum _actionType;
        public ActionEnum ActionType
        {
            set
            {
                _actionType = value;
                OnPropertyChanged(nameof(ActionType));
            }
            get
            {
                return _actionType;
            }
        }
        public List<ActionEnum> Actions
        {
            get
            {
                return new List<ActionEnum>
                {
                    ActionEnum.Click,
                    ActionEnum.Wait,
                    ActionEnum.Printscreen
                };
            }
        }
        public ICommand StartTestingCommand { get; set; }
        public ICommand AddCommand { get; set;}
        public ICommand ReadCommand { get; set;}
        public ICommand UpdateCommand {get; set;}
        public ICommand DeleteCommand { get; set; }
        public ICommand SetAppCommand { get; set; }
        public ICommand ExportPathCommand { get; set; }
        public ICommand ImportPathCommand { get; set; }

        public MainPageViewModel()
        {
            InitializeProperties();
            InitializeCommands();
            InitializeTimers();
            _connectionStatusService = Ioc.Default.GetService<IConnectionService>();
            // PowerShell Process Start
            Process process = new Process();
            process.StartInfo.FileName = "powershell.exe";
            process.StartInfo.Arguments = $"-command \"(Get-AppxPackage | Where-Object {{ $_.IsFramework -eq $false }} | Select-Object Name, PackageFamilyName)\"";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            // Read Result
            string output = process.StandardOutput.ReadToEnd();

            AppModels = new ObservableCollection<AppModel>();
            var serializer = new SerializationService<List<AppModel>>();
            string[] lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var supaLine = System.Text.RegularExpressions.Regex.Split(line.Trim(), @"\s{2,}");
                AppModels.Add(new AppModel(supaLine.First(), supaLine.Last()));
            }

            // Process End
            process.WaitForExit();
         }
        private void InitializeProperties()
        {
            IsAppSet = false;
            Logs = new ObservableCollection<string>();
            ConnectionStatus = ConnectionStatusEnum.Disconnected;
        }
        private void InitializeTimers()
        {
            _connectionStatusTimer = new System.Windows.Threading.DispatcherTimer();
            _connectionStatusTimer.Tick += new EventHandler(CheckConnectionStatus);
            _connectionStatusTimer.Interval = TimeSpan.FromSeconds(10);
            _connectionStatusTimer.Start();
        }
        public void InitializeCommands()
        {
            StartTestingCommand = new RelayCommand(async () => await StartTesting());
            AddCommand = new RelayCommand(AddTest);
            DeleteCommand = new RelayCommand<object>(DeleteTest);
            SetAppCommand = new RelayCommand(SetApp);
            ExportPathCommand = new RelayCommand(ExportPath);
            ImportPathCommand = new RelayCommand(ImportPath);
        }
        private void ExportPath()
        {
            var serializer = new SerializationService<ObservableCollection<TestModel>>();
            serializer.SerializeObject(CreatedPath);
        }
        private void ImportPath()
        {
            var serializer = new SerializationService<ObservableCollection<TestModel>>();
            var output = serializer.DeserializeObject();
            CreatedPath = new ObservableCollection<TestModel>(output);
        }
        private void SetApp()
        {
            IsAppSet = true;
        }
        private async Task StartTesting()
        {
            var appiumTest = new AppiumTesting(AppName);
            await appiumTest.StartTesting(CreatedPath, LogToList);

        }
        private void DeleteTest(object item)
        {
            CreatedPath.Remove(item as TestModel);
        }
        private void AddTest()
        {
            CreatedPath.Add(new TestModel(ElementName,ElementType, ActionType));
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
            var isConnected = _connectionStatusService.IsConnected("", $@"http://127.0.0.1:4723");
            ConnectionStatusEnum status = isConnected ? ConnectionStatusEnum.Connected : ConnectionStatusEnum.Disconnected;
            ConnectionStatus = status;
            //CommandManager.InvalidateRequerySuggested();
        }

    }
}
