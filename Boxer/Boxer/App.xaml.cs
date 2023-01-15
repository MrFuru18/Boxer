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
        private readonly ModalNavigationStore _modalNavigationStore;
        
        public App()
        {
            _accountStore = new AccountStore();
            _navigationStore = new NavigationStore();
            _modalNavigationStore = new ModalNavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            ClientHttp.InitializeClient();

            INavigationService navigationService = CreateLoginNavigationService();
            navigationService.Navigate();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore, _modalNavigationStore)
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
            return new TasksMenuViewModel(CreateMainMenuNavigationService());
        }

        private INavigationService CreateWarehouseMenuNavigationService()
        {
            return new NavigationService<WarehouseMenuViewModel>(_navigationStore, CreateWarehouseMenuViewModel);
        }
        private WarehouseMenuViewModel CreateWarehouseMenuViewModel()
        {
            return new WarehouseMenuViewModel(CreateMainMenuNavigationService());
        }

        private INavigationService CreateOrdersMenuNavigationService()
        {
            return new NavigationService<OrdersMenuViewModel>(_navigationStore, CreateOrdersMenuViewModel);
        }
        private OrdersMenuViewModel CreateOrdersMenuViewModel()
        {
            return new OrdersMenuViewModel(CreateMainMenuNavigationService());
        }
        private INavigationService CreateSuppliesMenuNavigationService()
        {
            return new NavigationService<SuppliesMenuViewModel>(_navigationStore, CreateSuppliesMenuViewModel);
        }
        private SuppliesMenuViewModel CreateSuppliesMenuViewModel()
        {
            return new SuppliesMenuViewModel(CreateMainMenuNavigationService());
        }

        private INavigationService CreateAdminMenuNavigationService()
        {
            return new NavigationService<AdminMenuViewModel>(_navigationStore, CreateAdminMenuViewModel);
        }
        private AdminMenuViewModel CreateAdminMenuViewModel()
        {
            return new AdminMenuViewModel(CreateMainMenuNavigationService());
        }

        private INavigationService CreateAddCustomerNavigationService()
        {
            return new ModalNavigationService<AddCustomerViewModel>(_modalNavigationStore, CreateAddCustomerViewModel);
        }
        private AddCustomerViewModel CreateAddCustomerViewModel()
        {
            return new AddCustomerViewModel(new CloseModalNavigationService(_modalNavigationStore));
        }

        private INavigationService CreateAddEmployeeNavigationService()
        {
            return new ModalNavigationService<AddEmployeeViewModel>(_modalNavigationStore, CreateAddEmployeeViewModel);
        }
        private AddEmployeeViewModel CreateAddEmployeeViewModel()
        {
            return new AddEmployeeViewModel(new CloseModalNavigationService(_modalNavigationStore));
        }

        private INavigationService CreateAddLocationNavigationService()
        {
            return new ModalNavigationService<AddLocationViewModel>(_modalNavigationStore, CreateAddLocationViewModel);
        }
        private AddLocationViewModel CreateAddLocationViewModel()
        {
            return new AddLocationViewModel(new CloseModalNavigationService(_modalNavigationStore));
        }

        private INavigationService CreateAddManufacturerNavigationService()
        {
            return new ModalNavigationService<AddManufacturerViewModel>(_modalNavigationStore, CreateAddManufacturerViewModel);
        }
        private AddManufacturerViewModel CreateAddManufacturerViewModel()
        {
            return new AddManufacturerViewModel(new CloseModalNavigationService(_modalNavigationStore));
        }
    }
}
