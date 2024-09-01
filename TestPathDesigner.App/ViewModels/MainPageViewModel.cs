using CommunityToolkit.Mvvm.Input;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestPathDesigner.Testing;

namespace TestPathDesigner.App.ViewModels
{
    internal class MainPageViewModel : BaseViewModel
    {
        private List<TestModel> elementsToTest = new List<TestModel>();
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
            Logs = new ObservableCollection<string>();
            StartTestingCommand = new RelayCommand(async () => await StartTesting());
            AddNewTestCommand = new RelayCommand(AddNewTest);
        }
        public async Task StartTesting()
        {
            var appiumTest = new AppiumTesting(AppName);
            await appiumTest.StartTesting(elementsToTest, LogToList);
        } 
        public void AddNewTest()
        {
            elementsToTest.Add(new TestModel(ElementName,ElementType));
            ElementName = "";
        }
        public void LogToList(string element)
        {
            Add(element);
        }
        public void Add(string element)
        {
            Application.Current.Dispatcher.Invoke(() => Logs.Add(element));
        }

    }
}
