using ApiLibrary.Model;
using ApiLibrary.Repo;
using Boxer.Commands;
using Boxer.Navigation;
using Boxer.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    class CustomersViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        ModalNavigationStore _modalNavigationStore;
        public BindingList<Customer> customers { get; set; }
        private List<Customer> _customers { get; set; }
        private Customer customer = new Customer();

        public ICommand NavigateBackCommand { get; }
        public ICommand NewCustomer { get; }
        private ICommand _editCustomer;
        public ICommand EditCustomer
        {
            get
            {
                return _editCustomer ?? (_editCustomer = new RelayCommand((p) =>
                {
                    _modalNavigationStore.CurrentViewModel = new AddCustomerViewModel(new CloseModalNavigationService(_modalNavigationStore), SelectedCustomer);

                }, p => true));

            }
        }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                onPropertyChanged(nameof(SelectedCustomer));
            }
        }


        public CustomersViewModel(INavigationService ordersMenuNavigationService, INavigationService addCustomerNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addCustomerNavigationService;
            _modalNavigationStore = modalNavigationStore;

            NavigateBackCommand = new NavigateCommand(ordersMenuNavigationService);
            NewCustomer = new NavigateCommand(addCustomerNavigationService);

            customer = new Customer();
            customers = new BindingList<Customer>(CustomerProcessor.getAllCustomers(customer).Result);
            _customers = new List<Customer>(customers);

            if (_customers.Count > 0)
            {
                SelectedCustomer = _customers[0];
            }

        }
    }
}
