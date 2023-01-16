using ApiLibrary.Model;
using ApiLibrary.Repo;
using Boxer.Commands;
using Boxer.Navigation;
using Boxer.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    class InventoryViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        ModalNavigationStore _modalNavigationStore;

        public BindingList<Inventory> inventoryList { get; set; }
        private List<Inventory> _inventoryList { get; set; }
        private Inventory inventory = null;

        public ICommand NavigateBackCommand { get; }
        public ICommand NewInventory { get; }

        public InventoryViewModel(INavigationService warehouseMenuNavigationService, INavigationService addInventoryNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addInventoryNavigationService;
            _modalNavigationStore = modalNavigationStore;

            NavigateBackCommand = new NavigateCommand(warehouseMenuNavigationService);
            NewInventory = new NavigateCommand(addInventoryNavigationService);

            inventory = new Inventory();
            inventoryList = new BindingList<Inventory>(InventoryProcessor.getAllInventory(inventory).Result);
            _inventoryList = new List<Inventory>(inventoryList);
        }
    }
}
