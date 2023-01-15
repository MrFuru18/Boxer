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
    class OrdersViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        ModalNavigationStore _modalNavigationStore;

        public ICommand NavigateBackCommand { get; }
        public ICommand NewOrder { get; }

        public OrdersViewModel(INavigationService ordersMenuNavigationService, INavigationService addOrderNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addOrderNavigationService;
            _modalNavigationStore = modalNavigationStore;

            NavigateBackCommand = new NavigateCommand(ordersMenuNavigationService);
            NewOrder = new NavigateCommand(addOrderNavigationService);

        }
    }
}
