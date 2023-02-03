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
            if (checkIfCorrect())
            {
                string result = ProductProcessor.addProduct(_product).Result;
                if (result == "Created")
                    _navigationService.Navigate();
                else
                    MessageBox.Show(result);

            }
        }

        private bool checkIfCorrect()
        {

            if (_product.sku == null)
            {
                MessageBox.Show("SKU nie może być puste");
                return false;
            }
            if (_product.name == null)
            {
                MessageBox.Show("Nazwa nie może być pusta");
                return false;
            }
            if (_product.manufacturer_id == null)
            {
                MessageBox.Show("Id producenta nie może być puste");
                return false;
            }

            List<Product> prods = new List<Product>(ProductProcessor.getAllProducts(new Product()).Result);
            foreach (var pr in prods)
                if (pr.sku == _product.sku)
                {
                    MessageBox.Show("SKU już istnieje");
                    return false;
                }

            List<Manufacturer> manList = ManufacturerProcessor.getAllManufacturers(new Manufacturer()).Result;
            bool manExist = false;
            foreach (var man in manList)
                if (man.id == _product.manufacturer_id)
                    manExist = true;

            if (!manExist)
            {
                MessageBox.Show("Id producenta nie istnieje");
                return false;
            }

            return true;


        }
    }
}
