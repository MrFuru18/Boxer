using ApiLibrary.Repo;
using ApiLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    using BaseClass;
    using Boxer.Commands;
    using Boxer.Model;
    using Boxer.Navigation;

    class MainViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly AccountStore _accountStore;

        public BaseViewModel CurrentPage => _navigationStore.CurrentViewModel;
        public BaseViewModel CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public bool IsModalOpen => _modalNavigationStore.IsOpen;

        public bool LoggedIn => _accountStore.IsLoggedIn;
       public string Uid => _accountStore.CurrentAccount?.uid;

        public ICommand NavigateTasksPannelPageCommand { get; }
        public ICommand NavigateWarehousePannelPageCommand { get; }
        public ICommand NavigateOrdersPannelPageCommand { get; }
        public ICommand NavigateSuppliesPannelPageCommand { get; }
        public ICommand NavigateAdminPannelPageCommand { get; }

        public ICommand Logout { get; }

        public MainViewModel(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore,
            AccountStore accountStore, INavigationService tasksMenuNavigationService,
            INavigationService warehouseMenuNavigationService, INavigationService ordersMenuNavigationService,
            INavigationService suppliesMenuNavigationService, INavigationService adminMenuNavigationService, INavigationService loginNavigationService)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _accountStore = accountStore;

            _navigationStore.CurrentPageChanged += OnCurrentPageChanged;
            _modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;

            NavigateTasksPannelPageCommand = new NavigateCommand(tasksMenuNavigationService);
            NavigateWarehousePannelPageCommand = new NavigateCommand(warehouseMenuNavigationService);
            NavigateOrdersPannelPageCommand = new NavigateCommand(ordersMenuNavigationService);
            NavigateSuppliesPannelPageCommand = new NavigateCommand(suppliesMenuNavigationService);
            NavigateAdminPannelPageCommand = new NavigateCommand(adminMenuNavigationService);
            Logout = new LogoutCommand(accountStore, loginNavigationService);
        }

        private void OnCurrentPageChanged()
        {
            onPropertyChanged(nameof(CurrentPage));
            onPropertyChanged(nameof(LoggedIn));
            onPropertyChanged(nameof(Uid));
        }
        private void OnCurrentModalViewModelChanged()
        {
            onPropertyChanged(nameof(CurrentModalViewModel));
            onPropertyChanged(nameof(IsModalOpen));
        }

        private ICommand _loadData;
        public ICommand LoadData
        {
            get
            {
                return _loadData ?? (_loadData = new RelayCommand((p) =>
                {
                    List<Employee> employees = new List<Employee>();
                    employees = EmployeeProcessor.getAllEmployees().Result;

                    Console.WriteLine(employees);
                },p => true));
            }
        }
    }
}
