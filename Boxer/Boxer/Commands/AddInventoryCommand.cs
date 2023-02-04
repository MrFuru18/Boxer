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
    class AddInventoryCommand : CommandBase
    {

        private readonly INavigationService _navigationService;
        private Inventory _inventory;

        public AddInventoryCommand(INavigationService navigationService, Inventory inventory)
        {
            _navigationService = navigationService;
            _inventory = inventory;
        }

        public override void Execute(object p)
        {
            if (checkIfCorrect())
            {
                ToCreateInventory inv = ObjectComparerUtility.Convert<Inventory, ToCreateInventory>(_inventory);
                string result = InventoryProcessor.addInventory(inv).Result;
                if (result == "Created")
                {
                    _navigationService.Navigate();
                }
                else
                    MessageBox.Show("Nie udało się dodać artykułu do stanów");
            }
        }

        private bool checkIfCorrect()
        {
            if ((_inventory.location_id == null) || (_inventory.location_id == 0))
            {
                MessageBox.Show("Id lokalizacji nie może być puste");
                return false;
            }
            if ((_inventory.product_id == null)||(_inventory.product_id == 0))
            {
                MessageBox.Show("Id produktu nie może być puste");
                return false;
            }

            List<Location> locList = LocationProcessor.getAllLocations(new Location()).Result;
            bool locExist = false;
            foreach (var loc in locList)
                if (loc.id == _inventory.location_id)
                    locExist = true;

            if (!locExist)
            {
                MessageBox.Show("Lokalizacja nie istnieje");
                return false;
            }

            List<Product> prodList = ProductProcessor.getAllProducts(new Product()).Result;
            bool prodExist = false;
            foreach (var prod in prodList)
                if (prod.id == _inventory.product_id)
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
