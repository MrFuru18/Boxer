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
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            ClientHttp.InitializeClient();

            INavigationService navigationService = CreateMainMenuNavigationService();
            navigationService.Navigate();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };

            MainWindow.Show();
            base.OnStartup(e);
        }

        private INavigationService CreateMainMenuNavigationService()
        {
            return new NavigationService<MainMenuViewModel>(_navigationStore, CreateMainMenuViewModel);
        }
        private MainMenuViewModel CreateMainMenuViewModel()
        {
            return new MainMenuViewModel(CreateUserControl1NavigationService(), CreateUserControl2NavigationService());
        }

        private INavigationService CreateUserControl1NavigationService()
        {
            return new NavigationService<UserControl1ViewModel>(_navigationStore, CreateUserControl1ViewModel);
        }
        private UserControl1ViewModel CreateUserControl1ViewModel()
        {
            return new UserControl1ViewModel(CreateUserControl2NavigationService());
        }
        
        private INavigationService CreateUserControl2NavigationService()
        {
            return new NavigationService<UserControl2ViewModel>(_navigationStore, CreateUserControl2ViewModel);
        }
        private UserControl2ViewModel CreateUserControl2ViewModel()
        {
            return new UserControl2ViewModel(CreateUserControl1NavigationService());
        }
    }
}
