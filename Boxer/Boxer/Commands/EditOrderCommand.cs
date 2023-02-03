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


        public EditOrderCommand(INavigationService navigationService, Order order)
        {
            _navigationService = navigationService;
            _order = order;
        }

        public override void Execute(object p)
        {
            ToCreateOrder ord = ObjectComparerUtility.Convert<Order, ToCreateOrder>(_order);
            string result = OrderProcessor.updateOrder(ord).Result;
            if (result == "OK")
            {

                _navigationService.Navigate();
            }
            else
                MessageBox.Show(result);
        }

    }
}
