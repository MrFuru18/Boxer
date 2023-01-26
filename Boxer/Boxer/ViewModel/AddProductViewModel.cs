using ApiLibrary.Model;
using Boxer.Commands;
using Boxer.Model;
using Boxer.Navigation;
using Boxer.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    class AddProductViewModel : BaseViewModel
    {
        public string HeaderText { get; set; }
        private Product _product = new Product();

        public ICommand CancelCommand { get; }
        public ICommand AddProduct { get; }

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

                if (ManufacturerId != "")
                    _product.manufacturer_id = int.Parse(ManufacturerId);
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

                _product.weight = float.Parse(Weight);
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

                _product.value = float.Parse(Value);
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
            CancelCommand = new NavigateCommand(navigationService);
            AddProduct = new NavigateCommand(navigationService);

            _product = new Product();

            HeaderText = "Dodaj Produkt";

            if (product != null)
            {
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
