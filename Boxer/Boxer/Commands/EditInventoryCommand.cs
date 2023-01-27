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
            string result = InventoryProcessor.updateInventory(_inventory).Result;
            if (result == "OK")
                _navigationService.Navigate();
            else
                MessageBox.Show(result);
        }
    }
}
