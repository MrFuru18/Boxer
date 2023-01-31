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
                MessageBox.Show(result);
        }

        private void addItems()
        {
            Supply sup = SupplyProcessor.getAllSupplies(new Supply()).Result.Last();
            foreach (var item in _supplyItems)
            {
                item.supply_id = sup.id;
                string result = SupplyProcessor.addSupplyItem(item).Result;
                if (result != "Created")
                {
                    MessageBox.Show(result);
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
                        if (result == "Created")
                        {

                        }
                        else
                            MessageBox.Show(r);
                    }

                }

            }
        }
    }
}
