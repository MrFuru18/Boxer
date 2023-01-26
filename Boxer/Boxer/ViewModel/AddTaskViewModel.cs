using ApiLibrary.Model;
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
    class AddTaskViewModel : BaseViewModel
    {
        public string HeaderText { get; set; }

        private Tasks _task = new Tasks();

        public ICommand CancelCommand { get; }
        public ICommand AddTask { get; }

        private string _employeeId;
        public string EmployeeId
        {
            get { return _employeeId; }
            set
            {
                _employeeId = value;
                onPropertyChanged(nameof(EmployeeId));

                if (EmployeeId != "")
                    _task.employee_id = int.Parse(EmployeeId);
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


        public AddTaskViewModel(INavigationService navigationService, Tasks task)
        {
            CancelCommand = new NavigateCommand(navigationService);
            AddTask = new NavigateCommand(navigationService);

            _task = new Tasks();

            HeaderText = "Dodaj Zadanie";
            if (task != null){
                HeaderText = "Edytuj Zadanie";

                _task = task;
                EmployeeId = _task.employee_id.ToString();
                Remarks = _task.remarks;
            }
        }
    }
}
