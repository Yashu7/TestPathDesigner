using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestPathDesigner.App.Commands
{
    public interface NavigateAppCommands
    {
        public ICommand ExitAppCommand { get; set; }
    }
}
