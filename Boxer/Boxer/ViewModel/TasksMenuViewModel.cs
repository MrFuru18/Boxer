using Boxer.Commands;
using Boxer.Navigation;
using Boxer.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    class TasksMenuViewModel : BaseViewModel
    {
        public ICommand NavigateTasksOrdersCommand { get; }
        public ICommand NavigateTasksSuppliesCommand { get; }
        //public ICommand NavigateTasksRelocationsCommand { get; }

        public TasksMenuViewModel(INavigationService tasksOrdersNavigationService, INavigationService tasksSuppliesNavigationService)
        {
            NavigateTasksOrdersCommand = new NavigateCommand(tasksOrdersNavigationService);
            NavigateTasksSuppliesCommand = new NavigateCommand(tasksSuppliesNavigationService);
            //NavigateTasksRelocationsCommand = new NavigateCommand(tasksNavigationService);
        }
    }
}
