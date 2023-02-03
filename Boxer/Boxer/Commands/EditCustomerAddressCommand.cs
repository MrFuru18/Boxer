using ApiLibrary.Model;
using ApiLibrary.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Boxer.Commands
{
    class EditCustomerAddressCommand : CommandBase
    {
        private CustomerAddress _customerAddress;
        public EditCustomerAddressCommand(CustomerAddress customerAddresses)
        {
            _customerAddress = customerAddresses;
        }

        public override void Execute(object p)
        {
            if (checkIfCorrect())
            {

                string result = CustomerProcessor.updateCustomerAddress(_customerAddress).Result;
                if (result != "OK")
                    MessageBox.Show(result);
            }
        }

        private bool checkIfCorrect()
        {

            if (_customerAddress.customer_id != null)
            {
                Customer c = new Customer();
                c.id = _customerAddress.customer_id;
                c = CustomerProcessor.getCustomer(c).Result;
                if (c == null)
                {
                    MessageBox.Show("Klienta o tym id nie znaleziono");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Adres nie odnosił się do żadnego klienta");
                return false;
            }
            return true;
        }

    }
}
