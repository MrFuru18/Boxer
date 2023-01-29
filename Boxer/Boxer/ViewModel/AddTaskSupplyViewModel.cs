using ApiLibrary.Model;
using Boxer.Commands;
using Boxer.Model;
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
    class AddTaskSupplyViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private bool isNotEdit = true;
        public string HeaderText { get; set; }

        private Tasks _task = new Tasks();
        public BindingList<Employee> employees { get; set; }
        public BindingList<Supply> supplies { get; set; }

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

            employees = new BindingList<Employee>();

            supplies = new BindingList<Supply>();

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
