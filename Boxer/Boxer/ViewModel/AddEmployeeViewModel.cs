using Boxer.Commands;
using Boxer.Navigation;
using Boxer.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    class AddEmployeeViewModel : BaseViewModel
    {
        public string Password { get; set; }

        public ICommand AddEmployee { get; }

        public AddEmployeeViewModel(INavigationService navigationService)
        {
            AddEmployee = new NavigateCommand(navigationService);
        }
    }
}
