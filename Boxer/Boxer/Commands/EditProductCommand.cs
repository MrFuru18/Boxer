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
    class EditProductCommand : CommandBase
    {
        private readonly INavigationService _navigationService;
        private Product _product;

        public EditProductCommand(INavigationService navigationService, Product product)
        {
            _navigationService = navigationService;
            _product = product;
        }

        public override void Execute(object p)
        {
            string result = ProductProcessor.updateProduct(_product).Result;
            if (result == "OK")
                _navigationService.Navigate();
            else
                MessageBox.Show(result);
        }
    }
}
