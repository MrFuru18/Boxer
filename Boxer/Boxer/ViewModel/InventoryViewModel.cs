using ApiLibrary.Model;
using ApiLibrary.Repo;
using Boxer.Commands;
using Boxer.Navigation;
using Boxer.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    class InventoryViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        ModalNavigationStore _modalNavigationStore;

        public ObservableCollection<Inventory> inventoryList { get; set; }
        private List<Inventory> _inventoryList { get; set; }
        private Inventory inventory = null;

        public ICommand NavigateBackCommand { get; }
        public ICommand NewInventory { get; }
        private ICommand _editInventory;
        public ICommand EditInventory
        {
            get
            {
                return _editInventory ?? (_editInventory = new RelayCommand((p) =>
                {
                    _modalNavigationStore.CurrentViewModel = new AddInventoryViewModel(new CloseModalNavigationService(_modalNavigationStore), SelectedInventory);
                   
                }, p => true));

            }
        }

        private Inventory _selectedInventory;
        public Inventory SelectedInventory
        {
            get { return _selectedInventory; }
            set
            {
                _selectedInventory = value;
                onPropertyChanged(nameof(SelectedInventory));
            }
        }


        public InventoryViewModel(INavigationService warehouseMenuNavigationService, INavigationService addInventoryNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addInventoryNavigationService;
            _modalNavigationStore = modalNavigationStore;

            _modalNavigationStore.CurrentViewModelClosed += OnCurrentModalViewModelClosed;

            NavigateBackCommand = new NavigateCommand(warehouseMenuNavigationService);
            NewInventory = new NavigateCommand(addInventoryNavigationService);

            inventory = new Inventory();
            inventoryList = new ObservableCollection<Inventory>(InventoryProcessor.getAllInventory(inventory).Result);
            _inventoryList = new List<Inventory>(inventoryList);

            if (_inventoryList.Count > 0)
            {
                SelectedInventory = _inventoryList[0];
            }
        }

        
        private void OnCurrentModalViewModelClosed()
        {
            _inventoryList = new List<Inventory>(InventoryProcessor.getAllInventory(inventory).Result);

            inventoryList.Clear();
            foreach (var invent in _inventoryList)
                inventoryList.Add(invent);

            if (_inventoryList.Count > 0)
            {
                SelectedInventory = _inventoryList[0];
            }
        }
    }
}
