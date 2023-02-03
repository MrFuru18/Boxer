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
                    MessageBox.Show(result);
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

            if (_employee.uid == null)
            {
                MessageBox.Show("Uid nie może być puste");
                return false;
            }
            if (_employee.password == null)
            {
                MessageBox.Show("Hasło nie może być puste");
                return false;
            }
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
