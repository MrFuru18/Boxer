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
        INavigationService _navigationService;
        ModalNavigationStore _modalNavigationStore;
        private BindingList<Employee> employees { get; set; }
        private List<Employee> _employees { get; set; }
        private Employee employee = null;

        public ICommand NavigateBackCommand { get; }
        public ICommand NewEmployee { get; }

        private ICommand _editEmployee;
        public ICommand EditEmployee 
        {
            get
            {
                return _editEmployee ?? (_editEmployee= new RelayCommand((p) =>
                {
                    _modalNavigationStore.CurrentViewModel = new AddEmployeeViewModel(_navigationService, employee);

                }, p => true));

            }
        }

        public EmployeesViewModel(INavigationService adminMenuNavigationService, INavigationService addEmployeeNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addEmployeeNavigationService;
            _modalNavigationStore = modalNavigationStore;

            NavigateBackCommand = new NavigateCommand(adminMenuNavigationService);
            NewEmployee = new NavigateCommand(addEmployeeNavigationService);

            employees = new BindingList<Employee>(EmployeeProcessor.getAllEmployees().Result);
            _employees = new List<Employee>(employees);
        }
    }
}
