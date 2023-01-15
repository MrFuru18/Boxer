using ApiLibrary.Model;
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
        public string HeaderText { get; set; }
        public string Password { get; set; }

        public ICommand CancelCommand { get; }
        public ICommand AddEmployee { get; }

        public AddEmployeeViewModel(INavigationService navigationService, Employee employee)
        {
            CancelCommand = new NavigateCommand(navigationService);
            AddEmployee = new NavigateCommand(navigationService);

            HeaderText = "Dodaj Pracownika";
            if (employee != null)
            {
                HeaderText = "Edytuj Pracownika";
            }
        }
    }
}
