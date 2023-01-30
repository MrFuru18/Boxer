using ApiLibrary.Model;
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
    class OrdersViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        ModalNavigationStore _modalNavigationStore;

        public ObservableCollection<Order> orders { get; set; }
        private List<Order> _orders { get; set; }
        private Order order = null;

        public ObservableCollection<OrderItem> order_items { get; set; }
        public List<OrderItem> _order_items { get; set; }
        private OrderItem orderItem = new OrderItem();

        public ICommand NavigateBackCommand { get; }
        public ICommand NewOrder { get; }
        private ICommand _editOrder;
        public ICommand EditOrder
        {
            get
            {
                return _editOrder ?? (_editOrder = new RelayCommand((p) =>
                {
                    _modalNavigationStore.CurrentViewModel = new AddOrderViewModel(new CloseModalNavigationService(_modalNavigationStore), SelectedOrder);

                }, p => true));

            }
        }


        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                _selectedOrder = value;
                onPropertyChanged(nameof(SelectedOrder));

                if (SelectedOrder != null)
                    loadOrderItems();
            }
        }

        private void loadOrderItems()
        {
            orderItem.order_id = SelectedOrder.id;
            _order_items = new List<OrderItem>(OrderProcessor.getOrderItems(orderItem).Result);

            order_items.Clear();
            foreach (var item in _order_items)
                order_items.Add(item);
        }

        public OrdersViewModel(INavigationService ordersMenuNavigationService, INavigationService addOrderNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addOrderNavigationService;
            _modalNavigationStore = modalNavigationStore;

            _modalNavigationStore.CurrentViewModelClosed += OnCurrentModalViewModelClosed;

            NavigateBackCommand = new NavigateCommand(ordersMenuNavigationService);
            NewOrder = new NavigateCommand(addOrderNavigationService);

            order = new Order();
            orders = new ObservableCollection<Order>(OrderProcessor.getAllOrders(order).Result);
            _orders = new List<Order>(orders);

            orderItem = new OrderItem();
            order_items = new ObservableCollection<OrderItem>();

            if (_orders.Count > 0)
            {
                SelectedOrder = _orders[0];
            }
        }

        
        private void OnCurrentModalViewModelClosed()
        {
            _orders = new List<Order>(OrderProcessor.getAllOrders(order).Result);

            orders.Clear();
            foreach (var ord in _orders)
                orders.Add(ord);

            if (_orders.Count > 0)
            {
                SelectedOrder = _orders[0];
            }
        }
    }
}
