using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ApiLibrary;
using Boxer.Navigation;
using Boxer.ViewModel;

namespace Boxer
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ClientHttp.InitializeClient();

            Navigate navigate = new Navigate();

            navigate.CurrentPage = new UserControl1ViewModel(navigate);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigate)
            };

            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
