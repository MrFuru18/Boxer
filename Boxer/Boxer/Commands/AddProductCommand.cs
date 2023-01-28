using ApiLibrary.Model;
using ApiLibrary.Repo;
using Boxer.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Boxer.Commands
{
    class AddProductCommand : CommandBase
    {

        private readonly INavigationService _navigationService;
        private Product _product;

        public AddProductCommand(INavigationService navigationService, Product product)
        {
            _navigationService = navigationService;
            _product = product;
        }

        public override void Execute(object p)
        {
            string result = ProductProcessor.addProduct(_product).Result;
            if (result == "Created")
                _navigationService.Navigate();
            else
                MessageBox.Show(result);
        }
    }
}
