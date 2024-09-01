using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public MainPageViewModel()
        {
            Title = "Hello world";
        }
    }
}
