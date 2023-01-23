using ApiLibrary.Model;
using ApiLibrary.Repo;
using Boxer.Commands;
using Boxer.Navigation;
using Boxer.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    class TasksViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        ModalNavigationStore _modalNavigationStore;

        public BindingList<Tasks> tasks { get; set; }
        private List<Tasks> _tasks { get; set; }
        private Tasks task = null;

        public ICommand NavigateBackCommand { get; }
        public ICommand NewTask { get; }
        private ICommand _editTask;
        public ICommand EditTask
        {
            get
            {
                return _editTask ?? (_editTask = new RelayCommand((p) =>
                {
                    _modalNavigationStore.CurrentViewModel = new AddTaskViewModel(new CloseModalNavigationService(_modalNavigationStore), SelectedTask);

                }, p => true));

            }
        }

        private Tasks _selectedTask;
        public Tasks SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                onPropertyChanged(nameof(SelectedTask));
            }
        }

        public TasksViewModel(INavigationService tasksMenuNavigationService, INavigationService addTaskNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addTaskNavigationService;
            _modalNavigationStore = modalNavigationStore;

            NavigateBackCommand = new NavigateCommand(tasksMenuNavigationService);
            NewTask = new NavigateCommand(addTaskNavigationService);

            task = new Tasks();
            tasks = new BindingList<Tasks>(TaskProcessor.getAllTasks(task).Result);
            _tasks = new List<Tasks>(tasks);

            if (_tasks.Count > 0)
            {
                SelectedTask = _tasks[0];
            }
        }
    }
}
