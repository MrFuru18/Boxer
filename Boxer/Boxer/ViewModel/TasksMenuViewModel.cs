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
        ICommand NavigateAddTaskCommand { get; }

        public TasksMenuViewModel()
        {

        }
    }
}
