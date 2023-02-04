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
    class AddSupplyCommand : CommandBase
    {

        private readonly INavigationService _navigationService;
        private Supply _supply;
        private List<SupplyItem> _supplyItems;

        public AddSupplyCommand(INavigationService navigationService, Supply supply, List<SupplyItem> supplyItems)
        {
            _navigationService = navigationService;
            _supply = supply;
            _supplyItems = supplyItems;
        }

        public override void Execute(object p)
        {
            ToCreateSupply sup = ObjectComparerUtility.Convert<Supply, ToCreateSupply>(_supply);
            string result = SupplyProcessor.addSupply(sup).Result;
            if (result == "Created")
            {
                addItems();

                _navigationService.Navigate();
            }
            else
                MessageBox.Show("Nie udało się dodać dostawy");
        }

        private void addItems()
        {
            Supply sup = SupplyProcessor.getAllSupplies(new Supply()).Result.Last();
            foreach (var item in _supplyItems)
            {
                item.supply_id = sup.id;

                if (checkIfCorrect(item))
                {
                    string result = SupplyProcessor.addSupplyItem(item).Result;
                    if (result != "Created")
                    {
                        MessageBox.Show("Nie udało się dodać artykułu do dostawy");
                    }
                    else
                    {
                        Inventory inv = new Inventory();
                        inv.product_id = item.product_id;
                        List<Inventory> inventoryList = new List<Inventory>(InventoryProcessor.getInventoryWhereProduct(inv).Result);
                        bool exist = false;
                        foreach (var el in inventoryList)
                        {
                            if (el.location_id == item.location_id)
                                exist = true;
                        }
                        if (exist == false)
                        {
                            ToCreateInventory invent = new ToCreateInventory();
                            invent.location_id = item.location_id;
                            invent.product_id = item.product_id;
                            invent.quantity = 0;
                            invent.remarks = "Auto Generated";
                            string r = InventoryProcessor.addInventory(invent).Result;
                            if (result != "Created")
                                MessageBox.Show("Nie udało się dodać powiązania z lokalizacją");
                        }

                    }

                }
                

            }
        }

        private bool checkIfCorrect(SupplyItem _supIt)
        {

            if ((_supIt.location_id == null) || (_supIt.location_id == 0))
            {
                MessageBox.Show("Id lokalizacji nie może być puste");
                return false;
            }
            if ((_supIt.product_id == null) || (_supIt.product_id == 0))
            {
                MessageBox.Show("Id produktu nie może być puste");
                return false;
            }

            List<Location> locList = LocationProcessor.getAllLocations(new Location()).Result;
            bool locExist = false;
            foreach (var loc in locList)
                if (loc.id == _supIt.location_id)
                    locExist = true;

            if (!locExist)
            {
                MessageBox.Show("Lokalizacja nie istnieje");
                return false;
            }

            List<Product> prodList = ProductProcessor.getAllProducts(new Product()).Result;
            bool prodExist = false;
            foreach (var prod in prodList)
                if (prod.id == _supIt.product_id)
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
