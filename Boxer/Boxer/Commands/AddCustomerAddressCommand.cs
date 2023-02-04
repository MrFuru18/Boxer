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
    class AddCustomerAddressCommand : CommandBase
    {
        private CustomerAddress _customerAddress;
        public AddCustomerAddressCommand(CustomerAddress customerAddresses)
        {
            _customerAddress = customerAddresses;
        }

        public override void Execute(object p)
        {
            if (checkIfCorrect())
            {
                string result = CustomerProcessor.addCustomerAddress(_customerAddress).Result;
                if (result != "Created")
                    MessageBox.Show("Nie udało się dodać adresu klienta");
            }
        }

        private bool checkIfCorrect()
        {

            if (_customerAddress.customer_id == null)
            {
                MessageBox.Show("Adres nie odnosił się do żadnego klienta");
                return false;
            }
            return true;
        }

    }
}
