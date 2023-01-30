using ApiLibrary.Model;
using Boxer.Commands;
using Boxer.Model;
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
    class AddTaskRelocationViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private bool isNotEdit = true;
        public string HeaderText { get; set; }

        //public bool TypeRelocationChosen { get; set; }

        private Tasks _task = new Tasks();
        /*
        public ObservableCollection<RelocationItem> relocation_items { get; set; }
        private List<RelocationItem> _relocation_items { get; set; }
        private RelocationItem _relocationItem = new RelocationItem();
        */
        public ObservableCollection<Employee> employees { get; set; }
        public ObservableCollection<Order> orders { get; set; }

        public ICommand CancelCommand { get; }
        public ICommand AddTask { get; }
        /*private ICommand _addItem;
        public ICommand AddItem
        {
            get
            {
                return _addItem ?? (_addItem = new RelayCommand((p) =>
                {
                    if (_relocationItem != null)
                    {
                        _relocation_items.Add(_relocationItem);
                        refreshRelocationItems();

                        _relocationItem = new RelocationItem();
                        _relocationItem.task_id = _task.id;

                        InventoryId = null;
                        LocationId = null;
                        Quantity = null;

                    }

                }, p => true));

            }
        }


        private ICommand _deleteItem;
        public ICommand DeleteItem
        {
            get
            {
                return _deleteItem ?? (_deleteItem = new RelayCommand((p) =>
                {
                    if (SelectedRelocationItem != null)
                    {
                        _relocation_items.Remove(SelectedRelocationItem);
                        refreshRelocationItems();
                    }
                }, p => true));

            }
        }
        */

        private string _employeeId;
        public string EmployeeId
        {
            get { return _employeeId; }
            set
            {
                _employeeId = value;
                onPropertyChanged(nameof(EmployeeId));

                _task.employee_id = Int32.TryParse(EmployeeId, out var tempVal) ? tempVal : (int?)null;
            }
        }

        private string _remarks;
        public string Remarks
        {
            get { return _remarks; }
            set
            {
                _remarks = value;
                onPropertyChanged(nameof(Remarks));

                _task.remarks = Remarks;
            }
        }
        /*
        private TaskTypes _taskType;
        public TaskTypes TaskType
        {
            get { return _taskType; }
            set
            {
                _taskType = value;
                onPropertyChanged(nameof(TaskType));

                setVisibility();
                _task.type = TaskType.ToString();
                _relocation_items.Clear();
                refreshRelocationItems();
            }
        }
        private void setVisibility()
        {
            TypeOrderChosen = false;
            TypeSupplyChosen = false;
            //TypeRelocationChosen = false;
            if (TaskType == TaskTypes.order)
            {
                TypeOrderChosen = true;
            }
            if (TaskType == TaskTypes.supply)
            {
                TypeSupplyChosen = true;
            }
            if (TaskType == TaskTypes.relocation)
            {
                TypeRelocationChosen = true;
            }
            onPropertyChanged(nameof(TypeOrderChosen));
            onPropertyChanged(nameof(TypeSupplyChosen));
            //onPropertyChanged(nameof(TypeRelocationChosen));
        }
        */

        /*
        private string _inventoryId;
        public string InventoryId
        {
            get { return _inventoryId; }
            set
            {
                _inventoryId = value;
                onPropertyChanged(nameof(InventoryId));

                _relocationItem.inventory_id = Int32.TryParse(InventoryId, out var tempVal) ? tempVal : (int?)null;
            }
        }

        private string _locationId;
        public string LocationId
        {
            get { return _locationId; }
            set
            {
                _locationId = value;
                onPropertyChanged(nameof(LocationId));

                _relocationItem.location_id = Int32.TryParse(LocationId, out var tempVal) ? tempVal : (int?)null;
            }
        }
        
        private string _quantity;
        public string Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                onPropertyChanged(nameof(Quantity));

                _relocationItem.quantity = Int32.TryParse(Quantity, out var tempVal) ? tempVal : (int?)null;
            }
        }
        */
        /*
        private RelocationItem _selectedRelocationItem;
        public RelocationItem SelectedRelocationItem
        {
            get { return _selectedRelocationItem; }
            set
            {
                _selectedRelocationItem = value;
                onPropertyChanged(nameof(SelectedRelocationItem));

                loadRelocationItem();
            }
        }
        private void loadRelocationItem()
        {
            if (SelectedRelocationItem != null)
            {
                InventoryId = SelectedRelocationItem.inventory_id.ToString();
                LocationId = SelectedRelocationItem.location_id.ToString();
                Quantity = SelectedRelocationItem.quantity.ToString();
            }
        }

        private void refreshRelocationItems()
        {
            relocation_items.Clear();
            foreach (var item in _relocation_items)
                relocation_items.Add(item);
        }
        
        */


        public AddTaskRelocationViewModel(INavigationService navigationService, Tasks task)
        {
            _navigationService = navigationService;
            CancelCommand = new NavigateCommand(navigationService);
            AddTask = new NavigateCommand(navigationService);

            _task = new Tasks();

            employees = new ObservableCollection<Employee>();

            orders = new ObservableCollection<Order>();

            /*
            _relocationItem = new OrderItem();
            relocation_items = new ObservableCollection<RelocationItem>();
            _relocation_items = new List<OrderItem>();
            */

            HeaderText = "Dodaj Zadanie";
            if (task != null)
            {
                isNotEdit = false;
                HeaderText = "Edytuj Zadanie";

                _task = task;
                EmployeeId = _task.employee_id.ToString();
                Remarks = _task.remarks;
                //TaskType = (TaskTypes)Enum.Parse(typeof(TaskTypes), _task.type);
            }
        }
    }
}
