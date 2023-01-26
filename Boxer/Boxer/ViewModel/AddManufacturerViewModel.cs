using ApiLibrary.Model;
using Boxer.Commands;
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
    class AddManufacturerViewModel : BaseViewModel
    {
        private bool edit = false;
        public string HeaderText { get; set; }

        private Manufacturer _manufacturer = new Manufacturer();

        public ICommand CancelCommand { get; }
        public ICommand AddManufacturer { get; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                onPropertyChanged(nameof(Name));

                _manufacturer.name = Name;
            }
        }

        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                onPropertyChanged(nameof(Country));

                _manufacturer.country = Country;
            }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                onPropertyChanged(nameof(Phone));

                _manufacturer.phone = Phone;
            }
        }
        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                onPropertyChanged(nameof(Email));

                _manufacturer.email = Email;
            }
        }

        public AddManufacturerViewModel(INavigationService navigationService, Manufacturer manufacturer)
        {
            CancelCommand = new NavigateCommand(navigationService);
            AddManufacturer = new NavigateCommand(navigationService);

            _manufacturer = new Manufacturer();

            HeaderText = "Dodaj Producenta";
            if(manufacturer != null)
            {
                edit = true;
                HeaderText = "Edytuj Producenta";

                _manufacturer = manufacturer;
                Name = _manufacturer.name;
                Country = _manufacturer.country;
                Phone = _manufacturer.phone;
                Email = _manufacturer.email;
            }
        }
    }
}
