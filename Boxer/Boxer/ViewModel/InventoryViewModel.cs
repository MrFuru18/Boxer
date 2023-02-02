using ApiLibrary.Model;
using ApiLibrary.Model.Views;
using ApiLibrary.Repo;
using Boxer.Commands;
using Boxer.Model;
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
        private readonly AccountStore _accountStore;

        public ObservableCollection<Inventory> inventoryList { get; set; }
        private List<Inventory> _inventoryList { get; set; }
        private Inventory inventory = null;

        private List<ProductDetailed> _products { get; set; }
        public ObservableCollection<ProductDetailed> products { get; set; }

        public ICommand NavigateBackCommand { get; }
        public ICommand NewInventory { get; }
        private ICommand _editInventory;
        public ICommand EditInventory
        {
            get
            {
                return _editInventory ?? (_editInventory = new RelayCommand((p) =>
                {
                    _modalNavigationStore.CurrentViewModel = new AddInventoryViewModel(new CloseModalNavigationService(_modalNavigationStore), _accountStore, SelectedInventory);
                   
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

                if (SelectedInventory != null)
                    loadProducts();
            }
        }

        private void loadProducts()
        {
            products.Clear();
            foreach (var p in _products)
                if (p.id == SelectedInventory.product_id)
                {
                    products.Add(p);
                    break;
                }

        }


        public InventoryViewModel(INavigationService warehouseMenuNavigationService, INavigationService addInventoryNavigationService, ModalNavigationStore modalNavigationStore, AccountStore accountStore)
        {
            _navigationService = addInventoryNavigationService;
            _modalNavigationStore = modalNavigationStore;
            _accountStore = accountStore;

            _modalNavigationStore.CurrentViewModelClosed += OnCurrentModalViewModelClosed;

            NavigateBackCommand = new NavigateCommand(warehouseMenuNavigationService);
            NewInventory = new NavigateCommand(addInventoryNavigationService);

            inventory = new Inventory();
            inventoryList = new ObservableCollection<Inventory>(InventoryProcessor.getAllInventory(inventory).Result);
            _inventoryList = new List<Inventory>(inventoryList);

            _products = new List<ProductDetailed>(ProductProcessor.getAllProductsDetailed(new Product()).Result);
            products = new ObservableCollection<ProductDetailed>();


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
