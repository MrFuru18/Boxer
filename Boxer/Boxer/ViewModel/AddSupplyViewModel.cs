﻿using ApiLibrary.Model;
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
    class AddSupplyViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private bool isNotEdit = true;
        public string HeaderText { get; set; }
        private Supply _supply = new Supply();

        public BindingList<SupplyItem> supply_items { get; set; }
        private List<SupplyItem> _supply_items { get; set; }
        private SupplyItem _supplyItem = new SupplyItem();

        public BindingList<Location> locations { get; set; }
        private List<Location> _locations { get; set; }
        private Location _location = new Location();

        public BindingList<Product> products { get; set; }
        private List<Product> _products { get; set; }
        private Product _product = new Product();

        public ICommand CancelCommand { get; }

        private ICommand _addSupply;
        public ICommand AddSupply
        {
            get
            {
                return _addSupply ?? (_addSupply = new RelayCommand((p) =>
                {
                    if (isNotEdit)
                    {
                        AddSupplyCommand addCommand = new AddSupplyCommand(_navigationService, _supply, _supply_items);
                        addCommand.Execute(true);
                    }
                    else
                    {
                        EditSupplyCommand editCommand = new EditSupplyCommand(_navigationService, _supply, _supply_items);
                        editCommand.Execute(true);
                    }


                }, p => true));

            }
        }

        private ICommand _addItem;
        public ICommand AddItem
        {
            get
            {
                return _addItem ?? (_addItem = new RelayCommand((p) =>
                {
                    if (_supplyItem != null)
                    {
                        _supply_items.Add(_supplyItem);
                        refreshSupplyItems();

                        _supplyItem = new SupplyItem();
                        _supplyItem.supply_id = _supply.id;

                        ProductId = null;
                        Quantity = null;
                        LocationId = null;
                    }

                }, p => true));

            }
        }


        private ICommand _deleteItem;
        public ICommand DeleteItem
        {
            get
            {
                return _deleteItem ?? (_deleteItem = new RelayCommand((p) =>
                {
                    if (SelectedSupplyItem != null)
                    {
                        _supply_items.Remove(SelectedSupplyItem);
                        refreshSupplyItems();
                    }
                }, p => true));

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

                _supplyItem.product_id = Int32.TryParse(ProductId, out var tempVal) ? tempVal : (int?)null;
            }
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                onPropertyChanged(nameof(SelectedProduct));

                ProductId = SelectedProduct.id.ToString();
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

                _supplyItem.location_id = Int32.TryParse(LocationId, out var tempVal) ? tempVal : (int?)null;
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

                LocationId = SelectedLocation.id.ToString();
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

                _supplyItem.quantity = Int32.TryParse(Quantity, out var tempVal) ? tempVal : (int?)null;
            }
        }

        private SupplyItem _selectedSupplyItem;
        public SupplyItem SelectedSupplyItem
        {
            get { return _selectedSupplyItem; }
            set
            {
                _selectedSupplyItem = value;
                onPropertyChanged(nameof(SelectedSupplyItem));

                loadSupplyItem();
            }
        }

        private void loadSupplyItem()
        {
            if (SelectedSupplyItem != null)
            {
                ProductId = SelectedSupplyItem.product_id.ToString();
                LocationId = _selectedSupplyItem.location_id.ToString();
                Quantity = SelectedSupplyItem.quantity.ToString();
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

                _supply.remarks = Remarks;
            }
        }

        private void refreshSupplyItems()
        {
            supply_items.Clear();
            foreach (var item in _supply_items)
                supply_items.Add(item);
        }

        public AddSupplyViewModel(INavigationService navigationService, Supply supply)
        {
            _navigationService = navigationService;
            CancelCommand = new NavigateCommand(navigationService);

            locations = new BindingList<Location>(LocationProcessor.getAllLocations(_location).Result);
            _locations = new List<Location>();

            products = new BindingList<Product>(ProductProcessor.getAllProducts(_product).Result);
            _products = new List<Product>();

            _supply = new Supply();

            _supplyItem = new SupplyItem();
            supply_items = new BindingList<SupplyItem>();
            _supply_items = new List<SupplyItem>();

            _product = new Product();

            HeaderText = "Dodaj Dostawę";
            if (supply != null)
            {
                isNotEdit = false;
                HeaderText = "Edytuj Dostawę";

                _supply = supply;
                Remarks = _supply.remarks;

                _supplyItem.supply_id = _supply.id;

                supply_items = new BindingList<SupplyItem>(SupplyProcessor.getSupplyItems(_supplyItem).Result);
                _supply_items.AddRange(supply_items);
            }
        }

    }
}
