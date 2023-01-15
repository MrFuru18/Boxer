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
        public string HeaderText { get; set; }
        public ICommand CancelCommand { get; }
        public ICommand AddCustomer { get; }
        public ICommand EditCustomer { get; }

        public AddCustomerViewModel(INavigationService navigationService)
        {
            CancelCommand = new NavigateCommand(navigationService);
            AddCustomer = new NavigateCommand(navigationService);
            EditCustomer = new NavigateCommand(navigationService);

            HeaderText = "Dodaj Klienta";
        }
    }
}
