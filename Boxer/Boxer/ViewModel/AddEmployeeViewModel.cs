using ApiLibrary.Model;
using ApiLibrary.Model.ToCreate;
using Boxer.Commands;
using Boxer.Model;
using Boxer.Navigation;
using Boxer.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private readonly INavigationService _navigationService;
        public bool isNotEdit { get; set; }
        public string HeaderText { get; set; }

        Employee _employee = new Employee();
        public ICommand CancelCommand { get; }
        private ICommand _addEmployee;
        public ICommand AddEmployee
        {
            get
            {
                return _addEmployee ?? (_addEmployee = new RelayCommand((p) =>
                {
                    if (isNotEdit)
                    {
                        ToCreateEmployee toCreateEmployee = ObjectComparerUtility.Convert<Employee, ToCreateEmployee>(_employee);
                        toCreateEmployee.password = Password;
                        AddEmployeeCommand addCommand = new AddEmployeeCommand(_navigationService, toCreateEmployee);
                        addCommand.Execute(true);
                    }
                    else
                    {
                        EditEmployeeCommand editCommand = new EditEmployeeCommand(_navigationService, _employee);
                        editCommand.Execute(true);
                    }


                }, p => true));

            }
        }


        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                onPropertyChanged(nameof(Name));

                _employee.name = Name;
            }
        }

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                onPropertyChanged(nameof(Surname));

                _employee.surname = Surname;
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                onPropertyChanged(nameof(Email));

                _employee.email = Email;
            }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                onPropertyChanged(nameof(Phone));

                _employee.phone = Phone;
            }
        }

        private string _uid;
        public string Uid
        {
            get { return _uid; }
            set
            {
                _uid = value;
                onPropertyChanged(nameof(Uid));

                _employee.uid = Uid;
            }
        }

        private Permissions _permission;
        public Permissions Permission
        {
            get { return _permission; }
            set
            {
                _permission = value;
                onPropertyChanged(nameof(Permission));

                _employee.permissions = Permission.ToString();
            }
        }

        public string Password { get; set; }

        public AddEmployeeViewModel(INavigationService navigationService, Employee employee)
        {
            isNotEdit = true;
            _navigationService = navigationService;

            CancelCommand = new NavigateCommand(navigationService);

            _employee = new Employee();
            Permission = Permissions.worker;

            HeaderText = "Dodaj Pracownika";

            if (employee != null)
            {
                isNotEdit = false;
                HeaderText = "Edytuj Pracownika";

                _employee = employee;
                Name = employee.name;
                Surname = employee.surname;
                Email = employee.email;
                Phone = employee.phone;
                Uid = employee.uid;
                Permission = (Permissions)Enum.Parse(typeof(Permissions), employee.permissions);
            }
        }
    }
}
