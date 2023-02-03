using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ApiLibrary;
using ApiLibrary.Model;
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
                DataContext = new MainViewModel(_navigationStore, _modalNavigationStore, _accountStore, CreateTasksMenuNavigationService(), CreateWarehouseMenuNavigationService(),
                CreateOrdersMenuNavigationService(), CreateSuppliesMenuNavigationService(), CreateAdminMenuNavigationService(),
                CreateLoginNavigationService())
            };

            MainWindow.Show();
            base.OnStartup(e);
        }


        //                                                                                                                          Login
        private INavigationService CreateLoginNavigationService()
        {
            return new NavigationService<LoginViewModel>(_navigationStore, CreateLoginViewModel);
        }
        private LoginViewModel CreateLoginViewModel()
        {
            return new LoginViewModel(_accountStore, CreateWarehouseMenuNavigationService());
        }


        //                                                                                                                          MenuPages
        /*
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
        */

        private INavigationService CreateTasksMenuNavigationService()
        {
            return new NavigationService<TasksMenuViewModel>(_navigationStore, CreateTasksMenuViewModel);
        }
        private TasksMenuViewModel CreateTasksMenuViewModel()
        {
            return new TasksMenuViewModel(CreateTasksOrdersNavigationService(), CreateTasksSuppliesNavigationService());
        }

        private INavigationService CreateWarehouseMenuNavigationService()
        {
            return new NavigationService<WarehouseMenuViewModel>(_navigationStore, CreateWarehouseMenuViewModel);
        }
        private WarehouseMenuViewModel CreateWarehouseMenuViewModel()
        {
            return new WarehouseMenuViewModel(CreateInventoryNavigationService(), CreateLocationsNavigationService());
        }

        private INavigationService CreateOrdersMenuNavigationService()
        {
            return new NavigationService<OrdersMenuViewModel>(_navigationStore, CreateOrdersMenuViewModel);
        }
        private OrdersMenuViewModel CreateOrdersMenuViewModel()
        {
            return new OrdersMenuViewModel(CreateOrdersNavigationService(), CreateCustomersNavigationService());
        }
        private INavigationService CreateSuppliesMenuNavigationService()
        {
            return new NavigationService<SuppliesMenuViewModel>(_navigationStore, CreateSuppliesMenuViewModel);
        }
        private SuppliesMenuViewModel CreateSuppliesMenuViewModel()
        {
            return new SuppliesMenuViewModel(CreateSuppliesNavigationService(), CreateProductsNavigationService(), 
                CreateManufacturersNavigationService());
        }

        private INavigationService CreateAdminMenuNavigationService()
        {
            return new NavigationService<AdminMenuViewModel>(_navigationStore, CreateAdminMenuViewModel);
        }
        private AdminMenuViewModel CreateAdminMenuViewModel()
        {
            return new AdminMenuViewModel(CreateEmployeesNavigationService());
        }


        //                                                                                                                          Views

        private INavigationService CreateCustomersNavigationService()
        {
            return new NavigationService<CustomersViewModel>(_navigationStore, CreateCustomersViewModel);
        }
        private CustomersViewModel CreateCustomersViewModel()
        {
            return new CustomersViewModel(CreateOrdersMenuNavigationService(), CreateAddCustomerNavigationService(), _modalNavigationStore);
        }

        private INavigationService CreateEmployeesNavigationService()
        {
            return new NavigationService<EmployeesViewModel>(_navigationStore, CreateEmployeesViewModel);
        }
        private EmployeesViewModel CreateEmployeesViewModel()
        {
            return new EmployeesViewModel(CreateAdminMenuNavigationService(), CreateAddEmployeeNavigationService(), _modalNavigationStore);
        }
        private INavigationService CreateInventoryNavigationService()
        {
            return new NavigationService<InventoryViewModel>(_navigationStore, CreateInventoryViewModel);
        }
        private InventoryViewModel CreateInventoryViewModel()
        {
            return new InventoryViewModel(CreateWarehouseMenuNavigationService(), CreateAddInventoryNavigationService(), _modalNavigationStore, _accountStore);
        }

        private INavigationService CreateLocationsNavigationService()
        {
            return new NavigationService<LocationsViewModel>(_navigationStore, CreateLocationsViewModel);
        }
        private LocationsViewModel CreateLocationsViewModel()
        {
            return new LocationsViewModel(CreateWarehouseMenuNavigationService(), CreateAddLocationNavigationService(), _modalNavigationStore);
        }

        private INavigationService CreateManufacturersNavigationService()
        {
            return new NavigationService<ManufacturersViewModel>(_navigationStore, CreateManufacturersViewModel);
        }
        private ManufacturersViewModel CreateManufacturersViewModel()
        {
            return new ManufacturersViewModel(CreateSuppliesMenuNavigationService(), CreateAddManufacturerNavigationService(), _modalNavigationStore);
        }

        private INavigationService CreateOrdersNavigationService()
        {
            return new NavigationService<OrdersViewModel>(_navigationStore, CreateOrdersViewModel);
        }
        private OrdersViewModel CreateOrdersViewModel()
        {
            return new OrdersViewModel(CreateOrdersMenuNavigationService(), CreateAddOrderNavigationService(), _modalNavigationStore);
        }

        private INavigationService CreateProductsNavigationService()
        {
            return new NavigationService<ProductsViewModel>(_navigationStore, CreateProductsViewModel);
        }
        private ProductsViewModel CreateProductsViewModel()
        {
            return new ProductsViewModel(CreateSuppliesMenuNavigationService(), CreateAddProductNavigationService(), _modalNavigationStore);
        }

        private INavigationService CreateSuppliesNavigationService()
        {
            return new NavigationService<SuppliesViewModel>(_navigationStore, CreateSuppliesViewModel);
        }
        private SuppliesViewModel CreateSuppliesViewModel()
        {
            return new SuppliesViewModel(CreateSuppliesMenuNavigationService(), CreateAddSupplyNavigationService(), _modalNavigationStore);
        }

        private INavigationService CreateTasksOrdersNavigationService()
        {
            return new NavigationService<TasksOrdersViewModel>(_navigationStore, CreateTasksOrdersViewModel);
        }
        private TasksOrdersViewModel CreateTasksOrdersViewModel()
        {
            return new TasksOrdersViewModel(CreateTasksMenuNavigationService(), CreateAddTaskOrderNavigationService(), _modalNavigationStore);
        }
        private INavigationService CreateTasksSuppliesNavigationService()
        {
            return new NavigationService<TasksSuppliesViewModel>(_navigationStore, CreateTasksSuppliesViewModel);
        }
        private TasksSuppliesViewModel CreateTasksSuppliesViewModel()
        {
            return new TasksSuppliesViewModel(CreateTasksMenuNavigationService(), CreateAddTaskSupplyNavigationService(), _modalNavigationStore);
        }


        //                                                                                                                          Add Views
        private INavigationService CreateAddCustomerNavigationService()
        {
            return new ModalNavigationService<AddCustomerViewModel>(_modalNavigationStore, CreateAddCustomerViewModel);
        }
        private AddCustomerViewModel CreateAddCustomerViewModel()
        {
            return new AddCustomerViewModel(new CloseModalNavigationService(_modalNavigationStore), null);
        }

        private INavigationService CreateAddEmployeeNavigationService()
        {
            return new ModalNavigationService<AddEmployeeViewModel>(_modalNavigationStore, CreateAddEmployeeViewModel);
        }
        private AddEmployeeViewModel CreateAddEmployeeViewModel()
        {
            return new AddEmployeeViewModel(new CloseModalNavigationService(_modalNavigationStore), null);
        }
        private INavigationService CreateAddInventoryNavigationService()
        {
            return new ModalNavigationService<AddInventoryViewModel>(_modalNavigationStore, CreateAddInventoryViewModel);
        }
        private AddInventoryViewModel CreateAddInventoryViewModel()
        {
            return new AddInventoryViewModel(new CloseModalNavigationService(_modalNavigationStore), _accountStore, null);
        }

        private INavigationService CreateAddLocationNavigationService()
        {
            return new ModalNavigationService<AddLocationViewModel>(_modalNavigationStore, CreateAddLocationViewModel);
        }
        private AddLocationViewModel CreateAddLocationViewModel()
        {
            return new AddLocationViewModel(new CloseModalNavigationService(_modalNavigationStore), null);
        }

        private INavigationService CreateAddManufacturerNavigationService()
        {
            return new ModalNavigationService<AddManufacturerViewModel>(_modalNavigationStore, CreateAddManufacturerViewModel);
        }
        private AddManufacturerViewModel CreateAddManufacturerViewModel()
        {
            return new AddManufacturerViewModel(new CloseModalNavigationService(_modalNavigationStore), null);
        }

        private INavigationService CreateAddOrderNavigationService()
        {
            return new ModalNavigationService<AddOrderViewModel>(_modalNavigationStore, CreateAddOrderViewModel);
        }
        private AddOrderViewModel CreateAddOrderViewModel()
        {
            return new AddOrderViewModel(new CloseModalNavigationService(_modalNavigationStore), null);
        }

        private INavigationService CreateAddProductNavigationService()
        {
            return new ModalNavigationService<AddProductViewModel>(_modalNavigationStore, CreateAddProductViewModel);
        }
        private AddProductViewModel CreateAddProductViewModel()
        {
            return new AddProductViewModel(new CloseModalNavigationService(_modalNavigationStore), null);
        }

        private INavigationService CreateAddSupplyNavigationService()
        {
            return new ModalNavigationService<AddSupplyViewModel>(_modalNavigationStore, CreateAddSupplyViewModel);
        }
        private AddSupplyViewModel CreateAddSupplyViewModel()
        {
            return new AddSupplyViewModel(new CloseModalNavigationService(_modalNavigationStore), null);
        }

        private INavigationService CreateAddTaskOrderNavigationService()
        {
            return new ModalNavigationService<AddTaskOrderViewModel>(_modalNavigationStore, CreateAddTaskOrderViewModel);
        }
        private AddTaskOrderViewModel CreateAddTaskOrderViewModel()
        {
            return new AddTaskOrderViewModel(new CloseModalNavigationService(_modalNavigationStore), null);
        }

        private INavigationService CreateAddTaskSupplyNavigationService()
        {
            return new ModalNavigationService<AddTaskSupplyViewModel>(_modalNavigationStore, CreateAddTaskSupplyViewModel);
        }
        private AddTaskSupplyViewModel CreateAddTaskSupplyViewModel()
        {
            return new AddTaskSupplyViewModel(new CloseModalNavigationService(_modalNavigationStore), null);
        }
    }
}
