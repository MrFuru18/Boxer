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
            string result = EmployeeProcessor.addEmployee(_employee).Result;
            if (result == "Created")
                _navigationService.Navigate();
            else
                MessageBox.Show(result);
        }
    }
}
