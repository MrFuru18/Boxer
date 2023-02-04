using ApiLibrary.Model;
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
                addAddresses();

                _navigationService.Navigate();
            }
            else
                MessageBox.Show("Nie udało się dodać klienta");
        }

        private void addAddresses()
        {
            Customer cust = CustomerProcessor.getAllCustomers(new Customer()).Result.Last();
            foreach (var address in _customerAddresses)
            {
                address.customer_id = cust.id;
                string result = CustomerProcessor.addCustomerAddress(address).Result;
                if (result != "Created")
                    MessageBox.Show("Nie udało się dodać adresu klienta");
            }
        }
    }
}
