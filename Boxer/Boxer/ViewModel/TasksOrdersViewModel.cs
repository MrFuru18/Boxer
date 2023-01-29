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
    class TasksOrdersViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        ModalNavigationStore _modalNavigationStore;

        public BindingList<Tasks> tasks { get; set; }
        private List<Tasks> _tasks { get; set; }
        private Tasks task = null;

        public BindingList<TaskState> task_states { get; set; }
        public List<TaskState> _task_states { get; set; }
        private TaskState taskState = new TaskState();

        public ICommand NavigateBackCommand { get; }
        public ICommand NewTask { get; }
        private ICommand _editTask;
        public ICommand EditTask
        {
            get
            {
                return _editTask ?? (_editTask = new RelayCommand((p) =>
                {
                    _modalNavigationStore.CurrentViewModel = new AddTaskOrderViewModel(new CloseModalNavigationService(_modalNavigationStore), SelectedTask);

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

                if (SelectedTask != null)
                    loadTaskStates();
            }
        }

        private void loadTaskStates()
        {
            taskState.task_id = SelectedTask.id;
            _task_states = new List<TaskState>(TaskProcessor.getTaskStates(taskState).Result);

            task_states.Clear();
            foreach (var item in _task_states)
                task_states.Add(item);
        }

        public TasksOrdersViewModel(INavigationService tasksMenuNavigationService, INavigationService addTaskNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addTaskNavigationService;
            _modalNavigationStore = modalNavigationStore;
            
            _modalNavigationStore.CurrentViewModelClosed += OnCurrentModalViewModelClosed;
            
            NavigateBackCommand = new NavigateCommand(tasksMenuNavigationService);
            NewTask = new NavigateCommand(addTaskNavigationService);

            task = new Tasks();
            tasks = new BindingList<Tasks>(TaskProcessor.getAllTasks(task).Result);
            _tasks = new List<Tasks>(tasks);

            taskState = new TaskState();
            task_states = new BindingList<TaskState>();

            if (_tasks.Count > 0)
            {
                SelectedTask = _tasks[0];
            }
        }

        
        private void OnCurrentModalViewModelClosed()
        {
            _tasks = new List<Tasks>(TaskProcessor.getAllTasks(task).Result);

            tasks.Clear();
            foreach (var tas in _tasks)
                tasks.Add(tas);

            if (_tasks.Count > 0)
            {
                SelectedTask = _tasks[0];
            }
        }
    }
}
