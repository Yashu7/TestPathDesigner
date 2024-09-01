using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestPathDesigner.Testing;

namespace TestPathDesigner.App.ViewModels
{
    internal class MainPageViewModel : BaseViewModel
    {
        private string _title;
        public string Title
        {
            set {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
            get
            {
                return _title;
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
        public ICommand StartTesting { get; set; }
        public MainPageViewModel()
        {
            Title = "Hello world";
            StartTesting = new RelayCommand(() =>
            {
                var appiumTest = new AppiumTesting(AppName);
                List<string> elements = new List<string>()
                {
                    "Jeden",
                    "Plus",
                    "Dwa",
                    "Równa się"
                };
                appiumTest.StartTesting(elements);
            });
        }

    }
}
