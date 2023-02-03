using ApiLibrary.Model;
using ApiLibrary.Repo;
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
    class AddTaskSupplyViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private bool isNotEdit = true;
        public string HeaderText { get; set; }

        private Tasks _task = new Tasks();
        public ObservableCollection<Employee> employees { get; set; }
        private List<Employee> _employees { get; set; }
        private Employee _employee = new Employee();
        public ObservableCollection<Supply> supplies { get; set; }
        private List<Supply> _supplies { get; set; }
        private Supply _supply = new Supply();

        public ICommand CancelCommand { get; }
        private ICommand _addTask;
        public ICommand AddTask
        {
            get
            {
                return _addTask ?? (_addTask = new RelayCommand((p) =>
                {
                    if (isNotEdit)
                    {
                        AddTaskCommand addCommand = new AddTaskCommand(_navigationService, _task);
                        addCommand.Execute(true);
                    }
                    else
                    {
                        EditTaskCommand editCommand = new EditTaskCommand(_navigationService, _task);
                        editCommand.Execute(true);
                    }


                }, p => true));

            }
        }

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

        private string _uid;
        public string Uid
        {
            get { return _uid; }
            set
            {
                _uid = value;
                onPropertyChanged(nameof(Uid));

                filterUid();
            }
        }

        private void filterUid()
        {
            employees.Clear();
            foreach (var pr in _employees)
                employees.Add(pr);

            for (int i = employees.Count - 1; i >= 0; i--)
            {
                if (Uid == null)
                    Uid = "";
                if (!employees[i].uid.Contains(Uid))
                    employees.RemoveAt(i);
            }
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                onPropertyChanged(nameof(SelectedEmployee));

                if (SelectedEmployee != null)
                    EmployeeId = SelectedEmployee.id.ToString();
            }
        }

        private string _supplyId;
        public string SupplyId
        {
            get { return _supplyId; }
            set
            {
                _supplyId = value;
                onPropertyChanged(nameof(SupplyId));

                _task.supply_id = Int32.TryParse(SupplyId, out var tempVal) ? tempVal : (int?)null;
            }
        }

        private Supply _selectedSupply;
        public Supply SelectedSupply
        {
            get { return _selectedSupply; }
            set
            {
                _selectedSupply = value;
                onPropertyChanged(nameof(SelectedSupply));

                if (SelectedSupply != null)
                    SupplyId = SelectedSupply.id.ToString();
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
        public AddTaskSupplyViewModel(INavigationService navigationService, Tasks task)
        {
            _navigationService = navigationService;
            CancelCommand = new NavigateCommand(navigationService);

            _task = new Tasks();
            _task.type = TaskTypes.supply.ToString();

            employees = new ObservableCollection<Employee>(EmployeeProcessor.getAllEmployees().Result);
            _employees = new List<Employee>();
            _employees.AddRange(employees);

            supplies = new ObservableCollection<Supply>(SupplyProcessor.getSuppliesNotConnected(_supply).Result);

            Uid = "";
            HeaderText = "Dodaj Zadanie";
            if (task != null)
            {
                isNotEdit = false;
                HeaderText = "Edytuj Zadanie";

                _task = task;
                EmployeeId = _task.employee_id.ToString();
                SupplyId = task.supply_id.ToString();
                Remarks = _task.remarks;
            }
        }
    }
}
