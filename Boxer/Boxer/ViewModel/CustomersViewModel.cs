﻿using ApiLibrary.Model;
using ApiLibrary.Repo;
using Boxer.Commands;
using Boxer.Navigation;
using Boxer.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Customer> customers { get; set; }
        private List<Customer> _customers { get; set; }
        private Customer customer = new Customer();

        public ObservableCollection<CustomerAddress> customer_addresses { get; set; }
        public List<CustomerAddress> _customer_addresses { get; set; }
        private CustomerAddress customerAddress = new CustomerAddress();



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
                
                if (SelectedCustomer != null)
                    loadCustomerAddresses();
            }
        }

        private void loadCustomerAddresses()
        {
            customerAddress.customer_id = SelectedCustomer.id;
            _customer_addresses = new List<CustomerAddress>(CustomerProcessor.getCustomerAdresses(customerAddress).Result);

            customer_addresses.Clear();
            foreach (var address in _customer_addresses)
                customer_addresses.Add(address);
        }


        public CustomersViewModel(INavigationService ordersMenuNavigationService, INavigationService addCustomerNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addCustomerNavigationService;
            _modalNavigationStore = modalNavigationStore;

            _modalNavigationStore.CurrentViewModelClosed += OnCurrentModalViewModelClosed;

            NavigateBackCommand = new NavigateCommand(ordersMenuNavigationService);
            NewCustomer = new NavigateCommand(addCustomerNavigationService);


            customer = new Customer();
            customers = new ObservableCollection<Customer>(CustomerProcessor.getAllCustomers(customer).Result);
            _customers = new List<Customer>(customers);

            customerAddress = new CustomerAddress();
            customer_addresses = new ObservableCollection<CustomerAddress>();

            if (_customers.Count > 0)
            {
                SelectedCustomer = _customers[0];
            }

        }
        
        private void OnCurrentModalViewModelClosed()
        {
            _customers = new List<Customer>(CustomerProcessor.getAllCustomers(customer).Result);

            customers.Clear();
            foreach (var cust in _customers)
                customers.Add(cust);

            if (_customers.Count > 0)
            {
                SelectedCustomer = _customers[0];
            }

        }

    }
}
