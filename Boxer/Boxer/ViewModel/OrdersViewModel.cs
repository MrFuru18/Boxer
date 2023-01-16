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
    class OrdersViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        ModalNavigationStore _modalNavigationStore;

        public BindingList<Order> orders { get; set; }
        private List<Order> _orders { get; set; }
        private Order order = null;

        public ICommand NavigateBackCommand { get; }
        public ICommand NewOrder { get; }

        public OrdersViewModel(INavigationService ordersMenuNavigationService, INavigationService addOrderNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addOrderNavigationService;
            _modalNavigationStore = modalNavigationStore;

            NavigateBackCommand = new NavigateCommand(ordersMenuNavigationService);
            NewOrder = new NavigateCommand(addOrderNavigationService);

            order = new Order();
            orders = new BindingList<Order>(OrderProcessor.getAllOrders(order).Result);
            _orders = new List<Order>(orders);
        }
    }
}
