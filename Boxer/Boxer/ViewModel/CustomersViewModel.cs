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
using System.Windows;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    class CustomersViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        ModalNavigationStore _modalNavigationStore;

        public ICommand NavigateBackCommand { get; }
        public ICommand NewCustomer { get; }

        public CustomersViewModel(INavigationService ordersMenuNavigationService, INavigationService addCustomerNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addCustomerNavigationService;
            _modalNavigationStore = modalNavigationStore;

            NavigateBackCommand = new NavigateCommand(ordersMenuNavigationService);
            NewCustomer = new NavigateCommand(addCustomerNavigationService);

        }
    }
}
