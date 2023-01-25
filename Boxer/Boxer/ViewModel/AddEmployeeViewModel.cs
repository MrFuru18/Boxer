using ApiLibrary.Model;
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
        public string HeaderText { get; set; }

        public ICommand CancelCommand { get; }
        public ICommand AddEmployee { get; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                onPropertyChanged(nameof(Name));
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
            }
        }

        public string Password { get; set; }

        public AddEmployeeViewModel(INavigationService navigationService, Employee employee)
        {
            CancelCommand = new NavigateCommand(navigationService);
            AddEmployee = new NavigateCommand(navigationService);

            HeaderText = "Dodaj Pracownika";

            if (employee != null)
            {
                HeaderText = "Edytuj Pracownika";
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
