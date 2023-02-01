using ApiLibrary.Model;
using ApiLibrary.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Boxer.Commands
{
    public class AddOrderItemCommand : CommandBase
    {
        private OrderItem _orderItem;

        public AddOrderItemCommand(OrderItem orderItem)
        {
            _orderItem = orderItem;
        }

        public override void Execute(object p)
        {
            string result = OrderProcessor.addOrderItem(_orderItem).Result;
            if (result != "Created")
                MessageBox.Show(result);
        }
    }
}
