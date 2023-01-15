using Boxer.Commands;
using Boxer.Navigation;
using Boxer.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    class AddCustomerViewModel : BaseViewModel
    {
        public ICommand AddCustomer { get; }

        public AddCustomerViewModel(INavigationService navigationService)
        {
            AddCustomer = new NavigateCommand(navigationService);
        }
    }
}
