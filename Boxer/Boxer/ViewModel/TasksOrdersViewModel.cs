using ApiLibrary.Model;
using ApiLibrary.Model.Views;
using ApiLibrary.Repo;
using Boxer.Commands;
using Boxer.Navigation;
using Boxer.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<Tasks> tasks { get; set; }
        private List<Tasks> _tasks { get; set; }
        private Tasks task = null;

        public ObservableCollection<TaskState> task_states { get; set; }
        public List<TaskState> _task_states { get; set; }
        private TaskState taskState = new TaskState();

        public List<OrderItemDetailed> _order_items { get; set; }
        public ObservableCollection<OrderItemDetailed> order_items { get; set; }

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
                {
                    loadTaskStates();
                    loadOrderItems();
                }
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

        private void loadOrderItems()
        {
            OrderItem ordIt = new OrderItem();
            ordIt.order_id = SelectedTask.order_id;
            order_items.Clear();
            if (ordIt.order_id != null)
            {
                _order_items = new List<OrderItemDetailed>(OrderProcessor.getOrderItemsDetailed(ordIt).Result);
                foreach (var item in _order_items)
                    order_items.Add(item);

            }
        }

        public TasksOrdersViewModel(INavigationService tasksMenuNavigationService, INavigationService addTaskNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addTaskNavigationService;
            _modalNavigationStore = modalNavigationStore;
            
            _modalNavigationStore.CurrentViewModelClosed += OnCurrentModalViewModelClosed;
            
            NavigateBackCommand = new NavigateCommand(tasksMenuNavigationService);
            NewTask = new NavigateCommand(addTaskNavigationService);

            task = new Tasks();
            tasks = new ObservableCollection<Tasks>();
            _tasks = new List<Tasks>(TaskProcessor.getAllTasks(task).Result);
            foreach (var t in _tasks)
                if (t.type == "order")
                    tasks.Add(t);

            taskState = new TaskState();
            task_states = new ObservableCollection<TaskState>();

            order_items = new ObservableCollection<OrderItemDetailed>();

            if (_tasks.Count > 0)
            {
                SelectedTask = _tasks[0];
            }
        }

        
        private void OnCurrentModalViewModelClosed()
        {
            _tasks = new List<Tasks>(TaskProcessor.getAllTasks(task).Result);

            tasks.Clear();
            foreach (var t in _tasks)
                if (t.type == "order")
                    tasks.Add(t);


            if (_tasks.Count > 0)
            {
                SelectedTask = _tasks[0];
            }
        }
    }
}
