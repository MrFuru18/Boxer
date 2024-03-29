﻿using ApiLibrary.Model;
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
    class AddInventoryViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly AccountStore _accountStore;
        private bool isNotEdit = true;
        public string HeaderText { get; set; }

        private Inventory _inventory = new Inventory();

        public ObservableCollection<Location> locations { get; set; }
        private List<Location> _locations { get; set; }
        private Location _location = new Location();

        public ObservableCollection<ProductDetailed> products { get; set; }
        private List<ProductDetailed> _products { get; set; }
        private Product _product = new Product();

        public ICommand CancelCommand { get; }
        private ICommand _addInventory;
        public ICommand AddInventory
        {
            get
            {
                return _addInventory ?? (_addInventory = new RelayCommand((p) =>
                {
                    if (isNotEdit)
                    {
                        AddInventoryCommand addCommand = new AddInventoryCommand(_navigationService, _inventory);
                        addCommand.Execute(true);
                    }
                    else
                    {
                        EditInventoryCommand editCommand = new EditInventoryCommand(_navigationService, _inventory);
                        editCommand.Execute(true);
                    }


                }, p => true));

            }
        }

        private string _locationId;
        public string LocationId
        {
            get { return _locationId; }
            set
            {
                _locationId = value;
                onPropertyChanged(nameof(LocationId));

                _inventory.location_id = Int32.TryParse(LocationId, out var tempVal) ? tempVal : (int?)null;
            }
        }
        private string _locationSector;
        public string LocationSector
        {
            get { return _locationSector; }
            set
            {
                _locationSector = value;
                onPropertyChanged(nameof(LocationSector));

                filterLocations();
            }
        }
        private string _locationAisle;
        public string LocationAisle
        {
            get { return _locationAisle; }
            set
            {
                _locationAisle = value;
                onPropertyChanged(nameof(LocationAisle));

                filterLocations();
            }
        }
        private string _locationUnit;
        public string LocationUnit
        {
            get { return _locationUnit; }
            set
            {
                _locationUnit = value;
                onPropertyChanged(nameof(LocationUnit));

                filterLocations();
            }
        }
        private string _locationLevel;
        public string LocationLevel
        {
            get { return _locationLevel; }
            set
            {
                _locationLevel = value;
                onPropertyChanged(nameof(LocationLevel));

                filterLocations();
            }
        }

        private void filterLocations()
        {
            locations.Clear();
            foreach (var loc in _locations)
                locations.Add(loc);

            for (int i = locations.Count - 1; i >= 0; i--)
            {
                if (LocationSector == null)
                    LocationSector = "";
                if (LocationAisle == null)
                    LocationAisle = "";
                if (LocationUnit == null)
                    LocationUnit = "";
                if (LocationLevel == null)
                    LocationLevel = "";
                if ((!locations[i].sector.Contains(LocationSector)) || (!locations[i].aisle.Contains(LocationAisle)) || (!locations[i].unit.Contains(LocationUnit)) || (!locations[i].level.Contains(LocationLevel)))
                    locations.RemoveAt(i);
            }
        }

        private Location _selectedLocation;
        public Location SelectedLocation
        {
            get { return _selectedLocation; }
            set
            {
                _selectedLocation = value;
                onPropertyChanged(nameof(SelectedLocation));

                if (SelectedLocation != null)
                    LocationId = SelectedLocation.id.ToString();
            }
        }

        private string _productId;
        public string ProductId
        {
            get { return _productId; }
            set
            {
                _productId = value;
                onPropertyChanged(nameof(ProductId));

                _inventory.product_id = Int32.TryParse(ProductId, out var tempVal) ? tempVal : (int?)null;
            }
        }

        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            set
            {
                _productName = value;
                onPropertyChanged(nameof(ProductName));

                filterProducts();
            }
        }

        private string _sku;
        public string Sku
        {
            get { return _sku; }
            set
            {
                _sku = value;
                onPropertyChanged(nameof(Sku));

                filterProducts();
            }
        }

        private void filterProducts()
        {
            products.Clear();
            foreach (var pr in _products)
                products.Add(pr);

            for (int i = products.Count - 1; i >= 0; i--)
            {
                if (ProductName == null)
                    ProductName = "";
                if (Sku == null)
                    Sku = "";
                if ((!products[i].name.Contains(ProductName))||(!products[i].sku.Contains(Sku)))
                    products.RemoveAt(i);
            }
        }

        private ProductDetailed _selectedProduct;
        public ProductDetailed SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                onPropertyChanged(nameof(SelectedProduct));

                if (SelectedProduct != null)
                    ProductId = SelectedProduct.id.ToString();
            }
        }

        private string _quantity;
        public string Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                onPropertyChanged(nameof(Quantity));

                _inventory.quantity = Int32.TryParse(Quantity, out var tempVal) ? tempVal : (int?)null;
            }
        }

        private string _remarks;
        public string Remarks
        {
            get { return _remarks; }
            set
            {
                _remarks = value;
                onPropertyChanged(nameof(Remarks));

                _inventory.remarks = Remarks;
            }
        }

        public AddInventoryViewModel(INavigationService navigationService, AccountStore accountStore, Inventory inventory)
        {
            _navigationService = navigationService;
            _accountStore = accountStore;
            
            CancelCommand = new NavigateCommand(navigationService);

            locations = new ObservableCollection<Location>(LocationProcessor.getAllLocations(_location).Result);
            _locations = new List<Location>();
            _locations.AddRange(locations);

            products = new ObservableCollection<ProductDetailed>(ProductProcessor.getAllProductsDetailed(_product).Result);
            _products = new List<ProductDetailed>();
            _products.AddRange(products);

            _inventory = new Inventory();


            LocationSector = "";
            LocationAisle = "";
            LocationUnit = "";
            LocationLevel = "";
            ProductName = "";
            Sku = "";

            HeaderText = "Dodaj Do Stanów";
            if (inventory != null)
            {
                isNotEdit = false;
                HeaderText = "Edytuj Stany";

                _inventory = inventory;
                LocationId = _inventory.location_id.ToString();
                ProductId = _inventory.product_id.ToString();
                Quantity = _inventory.quantity.ToString();
                Remarks = _inventory.remarks;
            }
            _inventory.employee_id = _accountStore.CurrentAccount.id;
        }
    }
}
