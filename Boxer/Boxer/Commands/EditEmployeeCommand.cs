using ApiLibrary.Model;
using ApiLibrary.Repo;
using Boxer.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Boxer.Commands
{
    class EditEmployeeCommand : CommandBase
    {

        private readonly INavigationService _navigationService;
        private Employee _employee;

        public EditEmployeeCommand(INavigationService navigationService, Employee employee)
        {
            _navigationService = navigationService;
            _employee = employee;
        }

        public override void Execute(object p)
        {
            if (checkIfCorrect())
            {
                string result = EmployeeProcessor.updateEmployee(_employee).Result;
                if (result == "OK")
                    _navigationService.Navigate();
                else
                    MessageBox.Show(result);
            }
        }

        private bool checkIfCorrect()
        {
            if (_employee.name == null)
            {
                MessageBox.Show("Imię pracownika nie może być puste");
                return false;
            }
            if (_employee.surname == null)
            {
                MessageBox.Show("Nazwisko pracownika nie może być puste");
                return false;
            }
            return true;
        }
    }
}
