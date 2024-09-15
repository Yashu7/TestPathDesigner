using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPathDesigner.App.Services.Interfaces;

namespace TestPathDesigner.App.Services
{
    internal class AppService : IAppService
    {
        public void ExitApp()
        {
            App.Current.Shutdown();
        }
    }
}
