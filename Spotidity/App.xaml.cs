using Spotidity.Models;
using Spotidity.Stores;
using Spotidity.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Spotidity
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        

        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore navigationStore = NavigationStore.GetInstace();
            //navigationStore.CurrentViewModel = new CategoryTableViewModel(navigationStore);
            navigationStore.CurrentViewModel = new HomeViewModel(new Credentials("", ""));


            MainWindow = new MainWindow(navigationStore);
            MainWindow.Show();
            base.OnStartup(e);
        }

    }
}
