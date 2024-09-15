using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System.Configuration;
using System.Data;
using System.Windows;
using TestPathDesigner.App.Services;
using TestPathDesigner.App.Services.Interfaces;
using TestPathDesigner.App.ViewModels;

namespace TestPathDesigner.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(
                    new ServiceCollection()
                    .AddTransient<MainPageViewModel>()
                    .AddTransient<IConnectionService, ConnectionService>()
                    .AddTransient<IAppService, AppService>()
                    .BuildServiceProvider());
        }
    }

}
