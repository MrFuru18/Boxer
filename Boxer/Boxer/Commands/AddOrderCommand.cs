using ApiLibrary.Model;
using ApiLibrary.Model.ToCreate;
using ApiLibrary.Repo;
using Boxer.Model;
using Boxer.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Boxer.Commands
{
    class AddOrderCommand : CommandBase
    {

        private readonly INavigationService _navigationService;
        private Order _order;
        private List<OrderItem> _orderItems;

        public AddOrderCommand(INavigationService navigationService, Order order, List<OrderItem> orderItems)
        {
            _navigationService = navigationService;
            _order = order;
            _orderItems = orderItems;
        }

        public override void Execute(object p)
        {
            if (checkIfCorrect())
            {
                ToCreateOrder ord = ObjectComparerUtility.Convert<Order, ToCreateOrder>(_order);
                string result = OrderProcessor.addOrder(ord).Result;
                if (result == "Created")
                {
                    addItems();

                    _navigationService.Navigate();
                }
                else
                    MessageBox.Show(result);
            }
        }

        private void addItems()
        {
            Order ord = OrderProcessor.getAllOrders(new Order()).Result.Last();
            foreach (var item in _orderItems)
            {
                if (checkIfCorrectItem(item))
                {
                    item.order_id = ord.id;
                    string result = OrderProcessor.addOrderItem(item).Result;
                    if (result != "Created")
                        MessageBox.Show(result);
                }
            }
        }

        private bool checkIfCorrect()
        {

            if (_order.customer_address_id == null)
            {
                MessageBox.Show("Id adresu klienta nie może być puste");
                return false;
            }
            CustomerAddress c = new CustomerAddress();
            c.id = _order.customer_address_id;
            c = CustomerProcessor.getCustomerAdress(c).Result;
            if (c == null)
            {
                MessageBox.Show("Adresu o tym id nie znaleziono");
                return false;
            }
            return true;
        }
        private bool checkIfCorrectItem(OrderItem _orderItem)
        {
            if (_orderItem.quantity == null)
            {
                MessageBox.Show("Ilość produktów nie może być pusta");
                return false;
            }


            if (_orderItem.product_id == null)
            {
                MessageBox.Show("Id produktu nie może być puste");
                return false;
            }

            List<Product> prodList = ProductProcessor.getAllProducts(new Product()).Result;
            bool prodExist = false;
            foreach (var prod in prodList)
                if (prod.id == _orderItem.product_id)
                    prodExist = true;

            if (!prodExist)
            {
                MessageBox.Show("Id produktu nie istnieje");
                return false;
            }

            return true;
        }
    }
}
