using ApiLibrary.Model;
using ApiLibrary.Model.Views;
using ApiLibrary.Repo;
using Boxer.Commands;
using Boxer.Model;
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

        private bool isNotEdit = true;
        
        public string HeaderText { get; set; }

        private Order _order = new Order();

        public ObservableCollection<OrderItemDetailed> order_items { get; set; }
        private List<OrderItemDetailed> _order_items { get; set; }
        private OrderItem _orderItem = new OrderItem();

        public ObservableCollection<CustomerAddress> customer_addresses { get; set; }
        private List<CustomerAddress> _customer_addresses { get; set; }
        private CustomerAddress _customerAddress = new CustomerAddress();

        public ObservableCollection<ProductDetailed> products { get; set; }
        private List<ProductDetailed> _products { get; set; }
        private Product _product = new Product();



        public ICommand CancelCommand { get; }

        private ICommand _addOrder;
        public ICommand AddOrder
        {
            get
            {
                return _addOrder ?? (_addOrder = new RelayCommand((p) =>
                {
                    List<OrderItem> ordItems = new List<OrderItem>();
                    foreach (var ordIt in _order_items)
                    {
                        ordItems.Add(ObjectComparerUtility.Convert<OrderItemDetailed, OrderItem>(ordIt));
                    }
                    if (isNotEdit)
                    {
                        AddOrderCommand addCommand = new AddOrderCommand(_navigationService, _order, ordItems);
                        addCommand.Execute(true);
                    }
                    else
                    {
                        EditOrderCommand editCommand = new EditOrderCommand(_navigationService, _order);
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
                    if (isNotEdit)
                    {
                        if (_orderItem != null)
                        {
                            _orderItem.current_quantity = 0;
                            OrderItemDetailed ordItDet = new OrderItemDetailed();
                            ordItDet = ObjectComparerUtility.Convert<OrderItem, OrderItemDetailed>(_orderItem);
                            foreach (var pr in _products)
                            {
                                if (ordItDet.product_id == pr.id)
                                {
                                    ordItDet.name = pr.name;
                                    ordItDet.sku = pr.sku;
                                    ordItDet.manufacturer = pr.manufacturer_name;
                                    break;
                                }
                            }

                            _order_items.Add(ordItDet);
                            refreshOrderItems();

                            _orderItem = new OrderItem();
                            _orderItem.order_id = _order.id;

                            ProductId = null;
                            Quantity = null;
                        }
                    }
                    else
                    {
                        if (_orderItem != null)
                        {
                            _orderItem.current_quantity = 0;
                            _orderItem.order_id = _order.id;

                            AddOrderItemCommand addItemCommand = new AddOrderItemCommand(_orderItem);
                            addItemCommand.Execute(true);

                            _orderItem = new OrderItem();
                            _orderItem.order_id = _order.id;

                            _order_items = new List<OrderItemDetailed>(OrderProcessor.getOrderItemsDetailed(_orderItem).Result);
                            refreshOrderItems();

                            ProductId = null;
                            Quantity = null;
                        }

                    }
                    

                }, p => true));

            }
        }
        private ICommand _editItem;
        public ICommand EditItem
        {
            get
            {
                return _editItem ?? (_editItem = new RelayCommand((p) =>
                {
                    if (isNotEdit)
                    {
                        if (SelectedOrderItem != null)
                        {
                            SelectedOrderItem.product_id = Int32.TryParse(ProductId, out var tempVal1) ? tempVal1 : (int?)null;
                            SelectedOrderItem.quantity = Int32.TryParse(Quantity, out var tempVal2) ? tempVal2 : (int?)null;
                            //SelectedOrderItem.order_id = _order.id;
                            refreshOrderItems();

                            _orderItem = new OrderItem();
                            _orderItem.order_id = _order.id;

                            ProductId = null;
                            Quantity = null;
                        }
                    }
                    else
                    {
                        if (SelectedOrderItem != null)
                        {
                            //SelectedOrderItem.product_id = Int32.TryParse(ProductId, out var tempVal1) ? tempVal1 : (int?)null;
                            SelectedOrderItem.quantity = Int32.TryParse(Quantity, out var tempVal2) ? tempVal2 : (int?)null;
                            //SelectedOrderItem.order_id = _order.id;

                            OrderItem ordIt = ObjectComparerUtility.Convert<OrderItemDetailed, OrderItem>(SelectedOrderItem);
                            ordIt.order_id = _order.id;

                            EditOrderItemCommand editItemCommand = new EditOrderItemCommand(ordIt);
                            editItemCommand.Execute(true);

                            _orderItem = new OrderItem();
                            _orderItem.order_id = _order.id;

                            _order_items = new List<OrderItemDetailed>(OrderProcessor.getOrderItemsDetailed(_orderItem).Result);
                            refreshOrderItems();

                            ProductId = null;
                            Quantity = null;
                        }

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

        private string _customerId;
        public string CustomerId
        {
            get { return _customerId; }
            set
            {
                _customerId = value;
                onPropertyChanged(nameof(CustomerId));

                filterCustomerAddresses();
            }
        }

        private void filterCustomerAddresses()
        {
            customer_addresses.Clear();
            foreach (var ca in _customer_addresses)
                customer_addresses.Add(ca);

            for (int i = customer_addresses.Count - 1; i >= 0; i--)
            {
                if (CustomerId == null)
                    CustomerId = "";
                if (!customer_addresses[i].customer_id.ToString().Contains(CustomerId))
                    customer_addresses.RemoveAt(i);
            }
        }

        private CustomerAddress _selectedCustomerAddress;
        public CustomerAddress SelectedCustomerAddress
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

        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            set
            {
                _productName = value;
                onPropertyChanged(nameof(ProductName));

                filterProducts();
            }
        }

        private string _sku;
        public string Sku
        {
            get { return _sku; }
            set
            {
                _sku = value;
                onPropertyChanged(nameof(Sku));

                filterProducts();
            }
        }


        private void filterProducts()
        {
            products.Clear();
            foreach (var pr in _products)
                products.Add(pr);

            for (int i = products.Count - 1; i >= 0; i--)
            {
                if (ProductName == null)
                    ProductName = "";
                if (Sku == null)
                    Sku = "";
                if ((!products[i].name.Contains(ProductName)) || (!products[i].sku.Contains(Sku)))
                    products.RemoveAt(i);
            }
        }

        private ProductDetailed _selectedProduct;
        public ProductDetailed SelectedProduct
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

        private OrderItemDetailed _selectedOrderItem;
        public OrderItemDetailed SelectedOrderItem
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
            _navigationService = navigationService;

            CancelCommand = new NavigateCommand(navigationService);

            products = new ObservableCollection<ProductDetailed>(ProductProcessor.getAllProductsDetailed(_product).Result);
            _products = new List<ProductDetailed>();
            _products.AddRange(products);

            customer_addresses = new ObservableCollection<CustomerAddress>(CustomerProcessor.getAllCustomerAddresses(_customerAddress).Result);
            _customer_addresses = new List<CustomerAddress>();
            _customer_addresses.AddRange(customer_addresses);

            _order = new Order();

            _orderItem = new OrderItem();
            order_items = new ObservableCollection<OrderItemDetailed>();
            _order_items = new List<OrderItemDetailed>();

            CustomerId = "";
            ProductName = "";
            Sku = "";

            HeaderText = "Dodaj Zamówienie";
            if (order != null)
            {
                isNotEdit = false;
                HeaderText = "Edytuj Zamówienie";

                _order = order;
                CustomerAddressId = _order.customer_address_id.ToString();
                Remarks = _order.remarks;

                _orderItem.order_id = _order.id;

                order_items = new ObservableCollection<OrderItemDetailed>(OrderProcessor.getOrderItemsDetailed(_orderItem).Result);
                _order_items.AddRange(order_items);
            }
            onPropertyChanged(nameof(isNotEdit));
        }
    }
}
