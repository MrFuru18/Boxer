using ApiLibrary.Model;
using ApiLibrary.Model.ToCreate;
using ApiLibrary.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Boxer.Commands
{
    public class AddSupplyItemCommand : CommandBase
    {
        private SupplyItem _supplyItem;

        public AddSupplyItemCommand(SupplyItem supplyItem)
        {
            _supplyItem = supplyItem;
        }

        public override void Execute(object p)
        {
            string result = SupplyProcessor.addSupplyItem(_supplyItem).Result;
            if (result != "Created")
            {
                MessageBox.Show(result);
            }
            else
            {
                Inventory inv = new Inventory();
                inv.product_id = _supplyItem.product_id;
                List<Inventory> inventoryList = new List<Inventory>(InventoryProcessor.getInventoryWhereProduct(inv).Result);
                bool exist = false;
                foreach (var el in inventoryList)
                {
                    if (el.location_id == _supplyItem.location_id)
                        exist = true;
                }
                if (exist == false)
                {
                    ToCreateInventory invent = new ToCreateInventory();
                    invent.location_id = _supplyItem.location_id;
                    invent.product_id = _supplyItem.product_id;
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
