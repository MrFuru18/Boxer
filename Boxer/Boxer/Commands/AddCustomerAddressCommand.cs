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
            string result = CustomerProcessor.addCustomerAddress(_customerAddress).Result;
            if (result != "Created")
                MessageBox.Show(result);
        }

    }
}
