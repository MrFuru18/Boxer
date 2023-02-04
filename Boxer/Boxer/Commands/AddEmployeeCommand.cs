using ApiLibrary.Model;
using ApiLibrary.Model.ToCreate;
using ApiLibrary.Repo;
using Boxer.Model;
using Boxer.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Boxer.Commands
{
    class AddEmployeeCommand : CommandBase
    {

        private readonly INavigationService _navigationService;
        private ToCreateEmployee _employee;

        public AddEmployeeCommand(INavigationService navigationService, ToCreateEmployee employee)
        {
            _navigationService = navigationService;
            _employee = employee;
        }

        public override void Execute(object p)
        {
            if (checkIfCorrect())
            {
                string result = EmployeeProcessor.addEmployee(_employee).Result;
                if (result == "Created")
                    _navigationService.Navigate();
                else
                    MessageBox.Show("Nie udało się dodać pracownika");
            } 
            
        }

        private bool checkIfCorrect()
        {
            List<Employee> emps = EmployeeProcessor.getAllEmployees().Result;
            foreach (var e in emps)
                if (e.uid == _employee.uid){
                    MessageBox.Show("Pracownik o tym Uid już istnieje");
                    return false;
                }

            if (string.IsNullOrWhiteSpace(_employee.uid))
            {
                MessageBox.Show("Uid nie może być puste");
                return false;
            }
            if (string.IsNullOrWhiteSpace(_employee.password))
            {
                MessageBox.Show("Hasło nie może być puste");
                return false;
            }
            if (string.IsNullOrWhiteSpace(_employee.name))
            {
                MessageBox.Show("Imię pracownika nie może być puste");
                return false;
            }
            if (string.IsNullOrWhiteSpace(_employee.surname))
            {
                MessageBox.Show("Nazwisko pracownika nie może być puste");
                return false;
            }
            return true;
        }
    }
}
