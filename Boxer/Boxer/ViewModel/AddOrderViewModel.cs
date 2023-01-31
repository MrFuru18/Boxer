﻿using ApiLibrary.Model;
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
    class AddOrderViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public bool isNotEdit { get; set; }
        
        public string HeaderText { get; set; }

        private Order _order = new Order();

        public ObservableCollection<OrderItem> order_items { get; set; }
        private List<OrderItem> _order_items { get; set; }
        private OrderItem _orderItem = new OrderItem();

        public ObservableCollection<CustomerAddress> customer_addresses { get; set; }
        private List<CustomerAddress> _customer_addresses { get; set; }
        private CustomerAddress _customerAddress = new CustomerAddress();

        public ObservableCollection<Product> products { get; set; }
        private List<Product> _products { get; set; }
        private Product _product = new Product();


        public ICommand CancelCommand { get; }

        private ICommand _addOrder;
        public ICommand AddOrder
        {
            get
            {
                return _addOrder ?? (_addOrder = new RelayCommand((p) =>
                {
                    if (isNotEdit)
                    {
                        AddOrderCommand addCommand = new AddOrderCommand(_navigationService, _order, _order_items);
                        addCommand.Execute(true);
                    }
                    else
                    {
                        EditOrderCommand editCommand = new EditOrderCommand(_navigationService, _order, _order_items);
                        editCommand.Execute(true);
                    }


                }, p => true));

            }
        }

        private ICommand _addItem;
        public ICommand AddItem
        {
            get
            {
                return _addItem ?? (_addItem = new RelayCommand((p) =>
                {
                    if (_orderItem != null)
                    {
                        _orderItem.current_quantity = 0;
                        _order_items.Add(_orderItem);
                        refreshOrderItems();

                        _orderItem = new OrderItem();
                        _orderItem.order_id = _order.id;

                        ProductId = null;
                        Quantity = null ;
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

                _order.customer_address_id = Int32.TryParse(CustomerAddressId, out var tempVal) ? tempVal : (int?)null;
            }
        }

        private Product _selectedCustomerAddress;
        public Product SelectedCustomerAddress
        {
            get { return _selectedCustomerAddress; }
            set
            {
                _selectedCustomerAddress = value;
                onPropertyChanged(nameof(SelectedCustomerAddress));

                if (SelectedCustomerAddress != null)
                    CustomerAddressId = SelectedCustomerAddress.id.ToString();
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

                _orderItem.product_id = Int32.TryParse(ProductId, out var tempVal) ? tempVal : (int?)null;
            }
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                onPropertyChanged(nameof(SelectedProduct));

                if (SelectedProduct != null)
                    ProductId = SelectedProduct.id.ToString();
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

                _orderItem.quantity = Int32.TryParse(Quantity, out var tempVal) ? tempVal : (int?)null;
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

                if (SelectedOrderItem != null)
                    loadOrderItem();
            }
        }

        private void loadOrderItem()
        {
            ProductId = SelectedOrderItem.product_id.ToString();
            Quantity = SelectedOrderItem.quantity.ToString();
        }

        private void refreshOrderItems()
        {
            order_items.Clear();
            foreach (var item in _order_items)
                order_items.Add(item);
        }

        public AddOrderViewModel(INavigationService navigationService, Order order)
        {
            isNotEdit = true;
            _navigationService = navigationService;

            CancelCommand = new NavigateCommand(navigationService);

            products = new ObservableCollection<Product>(ProductProcessor.getAllProducts(_product).Result);
            _products = new List<Product>();

            customer_addresses = new ObservableCollection<CustomerAddress>(CustomerProcessor.getAllCustomerAddresses(_customerAddress).Result);
            _customer_addresses = new List<CustomerAddress>();

            _order = new Order();

            _orderItem = new OrderItem();
            order_items = new ObservableCollection<OrderItem>();
            _order_items = new List<OrderItem>();


            HeaderText = "Dodaj Zamówienie";
            if (order != null)
            {
                isNotEdit = false;
                HeaderText = "Edytuj Zamówienie";

                _order = order;
                CustomerAddressId = _order.customer_address_id.ToString();
                Remarks = _order.remarks;

                _orderItem.order_id = _order.id;

                order_items = new ObservableCollection<OrderItem>(OrderProcessor.getOrderItems(_orderItem).Result);
                _order_items.AddRange(order_items);
            }
            onPropertyChanged(nameof(isNotEdit));
        }
    }
}
