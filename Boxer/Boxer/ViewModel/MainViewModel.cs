using ApiLibrary.Repo;
using ApiLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    using BaseClass;

    class MainViewModel : BaseViewModel
    {
        private ICommand _loadData;
        public ICommand LoadData
        {
            get
            {
                return _loadData ?? (_loadData = new RelayCommand((p) =>
                {
                    List<Employee> employees = new List<Employee>();
                    employees = EmployeeProcessor.getAllEmployees().Result;

                    Console.WriteLine(employees);
                },p => true));
            }
        }
    }
}
