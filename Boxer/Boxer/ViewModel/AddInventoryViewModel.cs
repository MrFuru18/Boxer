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
using System.Windows.Input;

namespace Boxer.ViewModel
{
    class AddInventoryViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private bool isNotEdit = true;
        public string HeaderText { get; set; }

        private Inventory _inventory = new Inventory();

        public ObservableCollection<Location> locations { get; set; }
        private List<Location> _locations { get; set; }
        private Location _location = new Location();

        public ObservableCollection<Product> products { get; set; }
        private List<Product> _products { get; set; }
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

        public AddInventoryViewModel(INavigationService navigationService, Inventory inventory)
        {
            _navigationService = navigationService;
            
            CancelCommand = new NavigateCommand(navigationService);

            locations = new ObservableCollection<Location>(LocationProcessor.getAllLocations(_location).Result);
            _locations = new List<Location>();

            products = new ObservableCollection<Product>(ProductProcessor.getAllProducts(_product).Result);
            _products = new List<Product>();

            _inventory = new Inventory();

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
        }
    }
}
