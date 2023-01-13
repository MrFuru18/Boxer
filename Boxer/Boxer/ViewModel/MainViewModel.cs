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
    using Boxer.Navigation;

    class MainViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigtionStore _modalNavigationStore;

        public BaseViewModel CurrentPage => _navigationStore.CurrentPage;
        public BaseViewModel CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public bool IsModalOpen => _modalNavigationStore.IsOpen;

        public MainViewModel(NavigationStore navigationStore, ModalNavigtionStore modalNavigationStore)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;

            _navigationStore.CurrentPageChanged += OnCurrentPageChanged;
            _modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;
        }

        private void OnCurrentPageChanged()
        {
            onPropertyChanged(nameof(CurrentPage));
        }
        private void OnCurrentModalViewModelChanged()
        {
            onPropertyChanged(nameof(CurrentModalViewModel));
            onPropertyChanged(nameof(IsModalOpen));
        }

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
