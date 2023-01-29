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
    class AddTaskCommand : CommandBase
    {
        private readonly INavigationService _navigationService;
        private Tasks _task;
        private List<RelocationItem> _relocationItems;

        public AddTaskCommand(INavigationService navigationService, Tasks task)
        {
            _navigationService = navigationService;
            _task = task;
        }

        public override void Execute(object p)
        {
            string result = TaskProcessor.addTask(_task).Result;
            if (result == "Created")
            {
                /*
                if (_task.type == "relocation")
                    addItems();
                */
                _navigationService.Navigate();
            }
            else
                MessageBox.Show(result);
        }

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
    }
}
