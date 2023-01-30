using ApiLibrary.Model;
using ApiLibrary.Repo;
using Boxer.Commands;
using Boxer.Model;
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
    class AddProductViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private bool isNotEdit = true;
        public string HeaderText { get; set; }
        private Product _product = new Product();

        public BindingList<Manufacturer> manufacturers { get; set; }
        private List<Manufacturer> _manufacturers { get; set; }
        private Manufacturer _manufacturer = new Manufacturer();

        public ICommand CancelCommand { get; }

        private ICommand _addProduct;
        public ICommand AddProduct
        {
            get
            {
                return _addProduct ?? (_addProduct = new RelayCommand((p) =>
                {
                    if (isNotEdit)
                    {
                        AddProductCommand addCommand = new AddProductCommand(_navigationService, _product);
                        addCommand.Execute(true);
                    }
                    else
                    {
                        EditProductCommand editCommand = new EditProductCommand(_navigationService, _product);
                        editCommand.Execute(true);
                    }


                }, p => true));

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

                _product.sku = Sku;
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                onPropertyChanged(nameof(Name));

                _product.name = Name;
            }
        }

        private string _manufacturerId;
        public string ManufacturerId
        {
            get { return _manufacturerId; }
            set
            {
                _manufacturerId = value;
                onPropertyChanged(nameof(ManufacturerId));

                _product.manufacturer_id = Int32.TryParse(ManufacturerId, out var tempVal) ? tempVal : (int?)null;
            }
        }

        private Manufacturer _selectedManufacturer;
        public Manufacturer SelectedManufacturer
        {
            get { return _selectedManufacturer; }
            set
            {
                _selectedManufacturer = value;
                onPropertyChanged(nameof(SelectedManufacturer));

                ManufacturerId = SelectedManufacturer.id.ToString();
            }
        }

        private string _weight;
        public string Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                onPropertyChanged(nameof(Weight));

                _product.weight = float.TryParse(Weight, out var tempVal) ? tempVal : (float?)null;
            }
        }

        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                onPropertyChanged(nameof(Value));

                _product.value = float.TryParse(Value, out var tempVal) ? tempVal : (float?)null;
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                onPropertyChanged(nameof(Description));

                _product.description = Description;
            }
        }

        private Sizes _size;
        public Sizes Size
        {
            get { return _size; }
            set
            {
                _size = value;
                onPropertyChanged(nameof(Size));

                _product.size = Size.ToString();
            }
        }
        public AddProductViewModel(INavigationService navigationService, Product product)
        {
            _navigationService = navigationService;
            CancelCommand = new NavigateCommand(navigationService);

            manufacturers = new BindingList<Manufacturer>(ManufacturerProcessor.getAllManufacturers(_manufacturer).Result);
            _manufacturers = new List<Manufacturer>();

            _product = new Product();
            Size = Sizes.unassigned;

            HeaderText = "Dodaj Produkt";

            if (product != null)
            {
                isNotEdit = false;
                HeaderText = "Edytuj Produkt";

                _product = product;
                Sku = _product.sku;
                Name = _product.name;
                ManufacturerId = _product.manufacturer_id.ToString();
                Weight = _product.weight.ToString();
                Value = _product.value.ToString();
                Description = _product.description;
                
                Size = (Sizes)Enum.Parse(typeof(Sizes), _product.size);
            }
        }
    }
}
