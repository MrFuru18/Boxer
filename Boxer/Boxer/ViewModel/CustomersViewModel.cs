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
        private Customer customer = null;

        public ICommand NavigateBackCommand { get; }
        public ICommand NewCustomer { get; }

        public CustomersViewModel(INavigationService ordersMenuNavigationService, INavigationService addCustomerNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addCustomerNavigationService;
            _modalNavigationStore = modalNavigationStore;

            NavigateBackCommand = new NavigateCommand(ordersMenuNavigationService);
            NewCustomer = new NavigateCommand(addCustomerNavigationService);

            customer = new Customer();
            customers = new BindingList<Customer>(CustomerProcessor.getAllCustomers(customer).Result);
            _customers = new List<Customer>(customers);

        }
    }
}
