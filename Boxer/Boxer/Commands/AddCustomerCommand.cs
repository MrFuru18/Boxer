﻿using ApiLibrary.Model;
using ApiLibrary.Repo;
using Boxer.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Boxer.Commands
{
    class AddCustomerCommand : CommandBase
    {

        private readonly INavigationService _navigationService;
        private Customer _customer;
        private List<CustomerAddress> _customerAddresses;

        public AddCustomerCommand(INavigationService navigationService, Customer customer, List<CustomerAddress> customerAddresses)
        {
            _navigationService = navigationService;
            _customer = customer;
            _customerAddresses = customerAddresses;
        }

        public override void Execute(object p)
        {
            string result = CustomerProcessor.addCustomer(_customer).Result;
            if (result == "Created")
            {

                _navigationService.Navigate();
            }
            else
                MessageBox.Show(result);
        }

        private void addAddresses()
        {
            foreach (var address in _customerAddresses)
            {
                string result = CustomerProcessor.addCustomer(_customer).Result;
                if (result != "Created")
                {
                    MessageBox.Show(result);
                }
            }
        }
    }
}
