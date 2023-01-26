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
    class AddOrderViewModel : BaseViewModel
    {
        private bool edit = false;
        public string HeaderText { get; set; }

        private Order _order = new Order();

        public BindingList<OrderItem> order_items { get; set; }
        private List<OrderItem> _order_items { get; set; }
        private OrderItem _orderItem = new OrderItem();

        private CustomerAddress _customerAddress = new CustomerAddress();

        private Product _product = new Product();

        public ICommand CancelCommand { get; }
        public ICommand AddOrder { get; }

        private ICommand _addItem;
        public ICommand AddItem
        {
            get
            {
                return _addItem ?? (_addItem = new RelayCommand((p) =>
                {
                    if (_orderItem != null)
                    {
                        _order_items.Add(_orderItem);
                        refreshOrderItems();

                        _orderItem = new OrderItem();
                        _orderItem.order_id = _order.id;

                        ProductId = "";
                        Quantity = "";
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
                    if (SelectedOrderItem != null)
                    {
                        _order_items.Remove(SelectedOrderItem);
                        refreshOrderItems();
                    }
                }, p => true));

            }
        }

        private string _customerAddressId;
        public string CustomerAddressId
        {
            get { return _customerAddressId; }
            set
            {
                _customerAddressId = value;
                onPropertyChanged(nameof(CustomerAddressId));

                if (CustomerAddressId != "")
                    _order.customer_address_id = int.Parse(CustomerAddressId);
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

                _order.remarks = Remarks;
            }
        }

        private string _productId;
        public string ProductId
        {
            get { return _productId; }
            set
            {
                _productId = value;
                onPropertyChanged(nameof(ProductId));

                if (ProductId != "")
                    _orderItem.product_id = int.Parse(ProductId.ToString());
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

                if (Quantity != "")
                    _orderItem.quantity = int.Parse(Quantity);
            }
        }

        private OrderItem _selectedOrderItem;
        public OrderItem SelectedOrderItem
        {
            get { return _selectedOrderItem; }
            set
            {
                _selectedOrderItem = value;
                onPropertyChanged(nameof(SelectedOrderItem));

                loadOrderItem();
            }
        }

        private void loadOrderItem()
        {
            if (SelectedOrderItem != null)
            {
                ProductId = SelectedOrderItem.product_id.ToString();
                Quantity = SelectedOrderItem.quantity.ToString();
            }
        }

        private void refreshOrderItems()
        {
            order_items.Clear();
            foreach (var item in _order_items)
                order_items.Add(item);
        }

        public AddOrderViewModel(INavigationService navigationService, Order order)
        {
            CancelCommand = new NavigateCommand(navigationService);
            AddOrder = new NavigateCommand(navigationService);

            _order = new Order();

            _orderItem = new OrderItem();
            order_items = new BindingList<OrderItem>();
            _order_items = new List<OrderItem>();

            _customerAddress = new CustomerAddress();

            _product = new Product();

            HeaderText = "Dodaj Zamówienie";
            if (order != null)
            {
                edit = true;
                HeaderText = "Edytuj Zamówienie";

                _order = order;
                CustomerAddressId = _order.customer_address_id.ToString();
                Remarks = _order.remarks;

                _orderItem.order_id = _order.id;

                order_items = new BindingList<OrderItem>(OrderProcessor.getOrderItems(_orderItem).Result);
                _order_items.AddRange(order_items);
            }
        }
    }
}
