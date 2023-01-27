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
    class EditOrderCommand : CommandBase
    {

        private readonly INavigationService _navigationService;
        private Order _order;
        private List<OrderItem> _orderItems;

        List<OrderItem> _toAddOrderItems = new List<OrderItem>();
        List<OrderItem> _toDeleteOrderItems = new List<OrderItem>();

        public EditOrderCommand(INavigationService navigationService, Order order, List<OrderItem> orderItems)
        {
            _navigationService = navigationService;
            _order = order;
            _orderItems = orderItems;
        }

        public override void Execute(object p)
        {
            ToCreateOrder ord = ObjectComparerUtility.Convert<Order, ToCreateOrder>(_order);
            string result = OrderProcessor.updateOrder(ord).Result;
            if (result == "OK")
            {
                checkItems();
                addItems();
                deleteItems();

                _navigationService.Navigate();
            }
            else
                MessageBox.Show(result);
        }

        private void checkItems()
        {
            OrderItem ordIt = new OrderItem();
            ordIt.order_id = _order.id;
            List<OrderItem> oldOrderItems = new List<OrderItem>(OrderProcessor.getOrderItems(ordIt).Result);
            OrderItemNoId addr = new OrderItemNoId();
            _toDeleteOrderItems.AddRange(oldOrderItems);

            foreach (var item in _orderItems)
            {
                bool found = false;
                addr = ObjectComparerUtility.Convert<OrderItem, OrderItemNoId>(item);

                foreach (var item2 in _toDeleteOrderItems)
                {
                    if (ObjectComparerUtility.ObjectsAreEqual(addr, ObjectComparerUtility.Convert<OrderItem, OrderItemNoId>(item2)))
                    {
                        found = true;
                        _toDeleteOrderItems.Remove(item2);
                        break;
                    }
                }

                if (!found)
                    _toAddOrderItems.Add(item);
            }

        }

        private void addItems()
        {
            foreach (var item in _toAddOrderItems)
            {
                string result = OrderProcessor.addOrderItem(item).Result;
                if (result != "Created")
                    MessageBox.Show(result);
            }
        }

        private void deleteItems()
        {
            foreach (var item in _toDeleteOrderItems)
            {
                string result = OrderProcessor.deleteOrderItem(item).Result;
                if (result != "OK")
                    MessageBox.Show(result);
            }
        }
    }
}
