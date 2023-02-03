using ApiLibrary.Model;
using ApiLibrary.Repo;
using Boxer.Commands;
using Boxer.Navigation;
using Boxer.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    class EmployeesViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        ModalNavigationStore _modalNavigationStore;
        public ObservableCollection<Employee> employees { get; set; }
        private List<Employee> _employees { get; set; }

        public ICommand NavigateBackCommand { get; }
        public ICommand NewEmployee { get; }

        private ICommand _editEmployee;
        public ICommand EditEmployee 
        {
            get
            {
                return _editEmployee ?? (_editEmployee= new RelayCommand((p) =>
                {
                    _modalNavigationStore.CurrentViewModel = new AddEmployeeViewModel(new CloseModalNavigationService(_modalNavigationStore), SelectedEmployee);

                }, p => true));

            }
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                onPropertyChanged(nameof(SelectedEmployee));
            }
        }


        public EmployeesViewModel(INavigationService adminMenuNavigationService, INavigationService addEmployeeNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addEmployeeNavigationService;
            _modalNavigationStore = modalNavigationStore;

            _modalNavigationStore.CurrentViewModelClosed += OnCurrentModalViewModelClosed;

            NavigateBackCommand = new NavigateCommand(adminMenuNavigationService);
            NewEmployee = new NavigateCommand(addEmployeeNavigationService);

            employees = new ObservableCollection<Employee>(EmployeeProcessor.getAllEmployees().Result);
            _employees = new List<Employee>(employees);
            
            if (_employees.Count > 0)
            {
                SelectedEmployee = _employees[0];
            }
        }

        private void OnCurrentModalViewModelClosed()
        {
            _employees = new List<Employee>(EmployeeProcessor.getAllEmployees().Result);

            employees.Clear();
            foreach (var employ in _employees)
                employees.Add(employ);

            if (_employees.Count > 0)
            {
                SelectedEmployee = _employees[0];
            }
        }

    }
}
