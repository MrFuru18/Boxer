using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ApiLibrary;
using Boxer.Model;
using Boxer.Navigation;
using Boxer.ViewModel;

namespace Boxer
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly AccountStore _accountStore;

        private readonly NavigationStore _navigationStore;
        
        public App()
        {
            _accountStore = new AccountStore();
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            ClientHttp.InitializeClient();

            INavigationService navigationService = CreateLoginNavigationService();
            navigationService.Navigate();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };

            MainWindow.Show();
            base.OnStartup(e);
        }

        private INavigationService CreateLoginNavigationService()
        {
            return new NavigationService<LoginViewModel>(_navigationStore, CreateLoginViewModel);
        }
        private LoginViewModel CreateLoginViewModel()
        {
            return new LoginViewModel(_accountStore, CreateMainMenuNavigationService());
        }

        private INavigationService CreateMainMenuNavigationService()
        {
            return new NavigationService<MainMenuViewModel>(_navigationStore, CreateMainMenuViewModel);
        }
        private MainMenuViewModel CreateMainMenuViewModel()
        {
            return new MainMenuViewModel(_accountStore, CreateTasksMenuNavigationService(), CreateWarehouseMenuNavigationService(), 
                CreateOrdersMenuNavigationService(), CreateSuppliesMenuNavigationService(), CreateAdminMenuNavigationService(),
                CreateLoginNavigationService());
        }

        private INavigationService CreateTasksMenuNavigationService()
        {
            return new NavigationService<TasksMenuViewModel>(_navigationStore, CreateTasksMenuViewModel);
        }
        private TasksMenuViewModel CreateTasksMenuViewModel()
        {
            return new TasksMenuViewModel();
        }

        private INavigationService CreateWarehouseMenuNavigationService()
        {
            return new NavigationService<WarehouseMenuViewModel>(_navigationStore, CreateWarehouseMenuViewModel);
        }
        private WarehouseMenuViewModel CreateWarehouseMenuViewModel()
        {
            return new WarehouseMenuViewModel();
        }

        private INavigationService CreateOrdersMenuNavigationService()
        {
            return new NavigationService<OrdersMenuViewModel>(_navigationStore, CreateOrdersMenuViewModel);
        }
        private OrdersMenuViewModel CreateOrdersMenuViewModel()
        {
            return new OrdersMenuViewModel();
        }
        private INavigationService CreateSuppliesMenuNavigationService()
        {
            return new NavigationService<SuppliesMenuViewModel>(_navigationStore, CreateSuppliesMenuViewModel);
        }
        private SuppliesMenuViewModel CreateSuppliesMenuViewModel()
        {
            return new SuppliesMenuViewModel();
        }

        private INavigationService CreateAdminMenuNavigationService()
        {
            return new NavigationService<AdminMenuViewModel>(_navigationStore, CreateAdminMenuViewModel);
        }
        private AdminMenuViewModel CreateAdminMenuViewModel()
        {
            return new AdminMenuViewModel();
        }

    }
}
