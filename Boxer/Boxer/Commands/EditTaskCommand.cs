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
        private List<RelocationItem> _relocationItems;
        /*
        List<RelocationItem> _toAddRelocationItems = new List<RelocationItem>();
        List<RelocationItem> _toDeleteRelocationItems = new List<RelocationItem>();
        */
        public EditTaskCommand(INavigationService navigationService, Tasks task, List<RelocationItem> relocationItems)
        {
            _navigationService = navigationService;
            _task = task;
            _relocationItems = relocationItems;
        }

        public override void Execute(object p)
        {
            string result = TaskProcessor.updateTask(_task).Result;
            if (result == "OK")
            {
                _navigationService.Navigate();
            }
            else
                MessageBox.Show(result);
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
