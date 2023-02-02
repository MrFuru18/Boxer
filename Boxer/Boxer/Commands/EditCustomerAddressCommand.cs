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
            string result = CustomerProcessor.updateCustomerAddress(_customerAddress).Result;
            if (result != "OK")
                MessageBox.Show(result);
        }

    }
}
