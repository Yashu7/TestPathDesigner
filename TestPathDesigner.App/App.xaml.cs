using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
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
            // Register services
            Ioc.Default.ConfigureServices(
                    new ServiceCollection()
                    .AddTransient<MainPageViewModel>()
                    .BuildServiceProvider());

            //StartupUri = new Uri(@"Views\MainPage.xaml");
        }
    }

}
