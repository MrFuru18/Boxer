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
    class AddProductViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private bool isNotEdit = true;
        public string HeaderText { get; set; }
        private Product _product = new Product();

        public ObservableCollection<Category> categories { get; set; }
        public ObservableCollection<Manufacturer> manufacturers { get; set; }
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

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                onPropertyChanged(nameof(SelectedCategory));

                if (SelectedCategory != null)
                    _product.category_id = SelectedCategory.id;
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
        private string _manufacturerName;
        public string ManufacturerName
        {
            get { return _manufacturerName; }
            set
            {
                _manufacturerName = value;
                onPropertyChanged(nameof(ManufacturerName));

                filter();
            }
        }

        private void filter()
        {
            manufacturers.Clear();
            foreach (var man in _manufacturers)
                manufacturers.Add(man);

            for (int i = manufacturers.Count - 1; i >= 0; i--)
            {
                if (ManufacturerName == null)
                    ManufacturerName = "";
                if (!manufacturers[i].name.Contains(ManufacturerName))
                    manufacturers.RemoveAt(i);
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

                if (SelectedManufacturer != null)
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
        public AddProductViewModel(INavigationService navigationService, ProductDetailed product)
        {
            _navigationService = navigationService;
            CancelCommand = new NavigateCommand(navigationService);

            categories = new ObservableCollection<Category>(ProductProcessor.getCategories(new Category()).Result);

            _manufacturers = new List<Manufacturer>();
            manufacturers = new ObservableCollection<Manufacturer>(ManufacturerProcessor.getAllManufacturers(_manufacturer).Result);
            _manufacturers.AddRange(manufacturers);

            _product = new Product();
            Size = Sizes.unassigned;

            HeaderText = "Dodaj Produkt";

            if (product != null)
            {
                isNotEdit = false;
                HeaderText = "Edytuj Produkt";

                _product = ObjectComparerUtility.Convert<ProductDetailed, Product>(product);
                Sku = _product.sku;
                Name = _product.name;
                ManufacturerId = _product.manufacturer_id.ToString();
                Weight = _product.weight.ToString();
                Value = _product.value.ToString();
                Description = _product.description;

                Category cat = new Category();
                cat.id = _product.category_id;
                cat = ProductProcessor.getCategory(cat).Result;
                SelectedCategory = cat;

                Size = (Sizes)Enum.Parse(typeof(Sizes), _product.size);
            }
        }
    }
}
