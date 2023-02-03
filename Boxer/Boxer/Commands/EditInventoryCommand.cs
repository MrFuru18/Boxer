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
    public class EditInventoryCommand : CommandBase
    {

        private readonly INavigationService _navigationService;
        private Inventory _inventory;

        public EditInventoryCommand(INavigationService navigationService, Inventory inventory)
        {
            _navigationService = navigationService;
            _inventory = inventory;
        }

        public override void Execute(object p)
        {
            ToCreateInventory inv = ObjectComparerUtility.Convert<Inventory, ToCreateInventory>(_inventory);
            string result = InventoryProcessor.updateInventory(inv).Result;
            if (result == "OK")
                _navigationService.Navigate();
            else
                MessageBox.Show(result);

        }
    }
}
