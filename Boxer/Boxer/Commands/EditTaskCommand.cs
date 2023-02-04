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
    class EditTaskCommand : CommandBase
    {

        private readonly INavigationService _navigationService;
        private Tasks _task;
        
        /*
        private List<RelocationItem> _relocationItems;
        
        List<RelocationItem> _toAddRelocationItems = new List<RelocationItem>();
        List<RelocationItem> _toDeleteRelocationItems = new List<RelocationItem>();
        */
        public EditTaskCommand(INavigationService navigationService, Tasks task)
        {
            _navigationService = navigationService;
            _task = task;
        }

        public override void Execute(object p)
        {
            if (checkIfCorrect())
            {
                string result = TaskProcessor.updateTask(_task).Result;
                if (result == "OK")
                {
                    _navigationService.Navigate();
                }
                else
                    MessageBox.Show("Nie udało się edytować zadania");
            }
        }

        private bool checkIfCorrect()
        {

            if ((_task.employee_id == null) || (_task.employee_id == 0))
            {
                MessageBox.Show("Id pracownika nie może być puste");
                return false;
            }
            List<Employee> employees = EmployeeProcessor.getAllEmployees().Result;
            bool employeeExist = false;
            foreach (var emp in employees)
                if (emp.id == _task.employee_id)
                    employeeExist = true;

            if (!employeeExist)
            {
                MessageBox.Show("Pracownik o tym id nie istnieje");
                return false;
            }
            return true;
        }

        /*
        private void addItems()
        {
            Tasks ta = TaskProcessor.getAllTasks(new Tasks()).Result.Last();
            foreach (var item in _relocationItems)
            {
                item.task_id = ta.id;
                string result = TaskProcessor.addRelocationItem(item).Result;
                if (result != "Created")
                    MessageBox.Show(result);
            }
        }
        */
    }
}
