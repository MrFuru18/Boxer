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

        private void addItems()
        {
            Order ord = OrderProcessor.getAllOrders(new Order()).Result.Last();
            foreach (var item in _orderItems)
            {
                item.order_id = ord.id;
                string result = OrderProcessor.addOrderItem(item).Result;
                if (result != "Created")
                    MessageBox.Show(result);
            }
        }
    }
}
