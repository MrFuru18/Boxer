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
            ToCreateInventory inv = ObjectComparerUtility.Convert<Inventory, ToCreateInventory>(_inventory);
            string result = InventoryProcessor.addInventory(inv).Result;
            if (result == "Created")
            {
                _navigationService.Navigate();
            }
            else
                MessageBox.Show(result);
        }
    }
}
