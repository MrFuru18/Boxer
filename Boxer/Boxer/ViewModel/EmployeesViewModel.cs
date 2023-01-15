using ApiLibrary.Model;
using ApiLibrary.Repo;
using Boxer.Commands;
using Boxer.Navigation;
using Boxer.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    class EmployeesViewModel : BaseViewModel
    {
        public BindingList<Employee> employees { get; set; }
        public List<Employee> _employees { get; set; }

        public ICommand NavigateBackCommand { get; }
        public ICommand NewEmployee { get; }



        public EmployeesViewModel(INavigationService adminMenuNavigationService, INavigationService addEmployeeNavigationService)
        {
            NavigateBackCommand = new NavigateCommand(adminMenuNavigationService);
            NewEmployee = new NavigateCommand(addEmployeeNavigationService);

            employees = new BindingList<Employee>(EmployeeProcessor.getAllEmployees().Result);
            _employees = new List<Employee>(employees);
        }
    }
}
