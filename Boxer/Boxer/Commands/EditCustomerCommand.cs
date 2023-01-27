using ApiLibrary.Model;
using ApiLibrary.Repo;
using Boxer.Model;
using Boxer.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Boxer.Commands
{
    class EditCustomerCommand : CommandBase
    {

        private readonly INavigationService _navigationService;
        private Customer _customer;
        private List<CustomerAddress> _customerAddresses;

        List<CustomerAddress> _toAddCustomerAddresses = new List<CustomerAddress>();
        List<CustomerAddress> _toDeleteCustomerAddresses = new List<CustomerAddress>();

        public EditCustomerCommand(INavigationService navigationService, Customer customer, List<CustomerAddress> customerAddresses)
        {
            _navigationService = navigationService;
            _customer = customer;
            _customerAddresses = customerAddresses;
        }

        public override void Execute(object p)
        {
            string result = CustomerProcessor.updateCustomer(_customer).Result;
            if (result == "OK")
            {
                checkAddresses();
                addAddresses();
                deleteAddresses();

                _navigationService.Navigate();
            }
            else
                MessageBox.Show(result);
        }

        private void checkAddresses()
        {
            CustomerAddress custAddr = new CustomerAddress();
            custAddr.customer_id = _customer.id;
            List<CustomerAddress> oldCustomerAddresses = new List<CustomerAddress>(CustomerProcessor.getCustomerAdresses(custAddr).Result);
            CustomerAddressNoId addr = new CustomerAddressNoId();
            _toDeleteCustomerAddresses.AddRange(oldCustomerAddresses);

            foreach (var address in _customerAddresses)
            {
                bool found = false;
                addr = ObjectComparerUtility.Convert<CustomerAddress, CustomerAddressNoId>(address);
                
                foreach (var address2 in _toDeleteCustomerAddresses)
                {
                    if (ObjectComparerUtility.ObjectsAreEqual(addr, ObjectComparerUtility.Convert<CustomerAddress, CustomerAddressNoId>(address2)))
                    {
                        found = true;
                        _toDeleteCustomerAddresses.Remove(address2);
                        break;
                    }
                }

                if (!found)
                    _toAddCustomerAddresses.Add(address);
            }

        }
        
        private void addAddresses()
        {
            foreach (var address in _toAddCustomerAddresses)
            {
                string result = CustomerProcessor.addCustomerAddress(address).Result;
                if (result != "Created")
                    MessageBox.Show(result);
            }
        }

        private void deleteAddresses()
        {
            foreach (var address in _toDeleteCustomerAddresses)
            {
                string result = CustomerProcessor.deleteCustomerAddress(address).Result;
                if (result != "OK")
                    MessageBox.Show(result);
            }
        }
    }
}
