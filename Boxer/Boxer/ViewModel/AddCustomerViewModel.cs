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
using System.Windows;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    class AddCustomerViewModel : BaseViewModel
    {
        private bool edit = false;
        public string HeaderText { get; set; }

        private Customer _customer = new Customer();

        public BindingList<CustomerAddress> customer_addresses { get; set; }
        private List<CustomerAddress> _customer_addresses { get; set; }
        private CustomerAddress _customerAddress = new CustomerAddress();

        public ICommand CancelCommand { get; }
        public ICommand AddCustomer { get; }

        private ICommand _addAddress;
        public ICommand AddAddress
        {
            get
            {
                return _addAddress ?? (_addAddress = new RelayCommand((p) =>
                {
                    if (_customerAddress != null)
                    {
                        _customer_addresses.Add(_customerAddress);
                        refreshCustomerAdresses();

                        _customerAddress = new CustomerAddress();
                        _customerAddress.customer_id = _customer.id;

                        AddressLine1 = "";
                        AddressLine2 = "";
                        City = "";
                        Country = "";
                        Region = "";
                        PostalCode = "";
                    }

                }, p => true));

            }
        }
        private ICommand _deleteAddress;
        public ICommand DeleteAddress
        {
            get
            {
                return _deleteAddress ?? (_deleteAddress = new RelayCommand((p) =>
                {
                    if (SelectedCustomerAddress != null)
                    {
                        _customer_addresses.Remove(SelectedCustomerAddress);
                        refreshCustomerAdresses();
                    }
                }, p => true));

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

                _customer.name = Name;
            }
        }

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                onPropertyChanged(nameof(Surname));

                _customer.surname = Surname;
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

                _customer.email = Email;
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

                _customer.phone = Phone;
            }
        }

        private string _addressLine1;
        public string AddressLine1
        {
            get { return _addressLine1; }
            set
            {
                _addressLine1 = value;
                onPropertyChanged(nameof(AddressLine1));

                _customerAddress.address_line_1 = AddressLine1;
            }
        }

        private string _addressLine2;
        public string AddressLine2
        {
            get { return _addressLine2; }
            set
            {
                _addressLine2 = value;
                onPropertyChanged(nameof(AddressLine2));

                _customerAddress.address_line_2 = AddressLine2;
            }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                onPropertyChanged(nameof(City));

                _customerAddress.city = City;
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

                _customerAddress.country = Country;
            }
        }

        private string _region;
        public string Region
        {
            get { return _region; }
            set
            {
                _region = value;
                onPropertyChanged(nameof(Region));

                _customerAddress.region = Region;
            }
        }

        private string _postalCode;
        public string PostalCode
        {
            get { return _postalCode; }
            set
            {
                _postalCode = value;
                onPropertyChanged(nameof(PostalCode));

                _customerAddress.postal_code = PostalCode;
            }
        }

        private CustomerAddress _selectedCustomerAddress;
        public CustomerAddress SelectedCustomerAddress
        {
            get { return _selectedCustomerAddress; }
            set
            {
                _selectedCustomerAddress = value;
                onPropertyChanged(nameof(SelectedCustomerAddress));

                loadCustomerAddress();
            }
        }

        private void loadCustomerAddress()
        {
            if (SelectedCustomerAddress != null)
            {
                AddressLine1 = SelectedCustomerAddress.address_line_1;
                AddressLine2 = SelectedCustomerAddress.address_line_2;
                Country = SelectedCustomerAddress.country;
                Region = SelectedCustomerAddress.region;
                PostalCode = SelectedCustomerAddress.postal_code;
            }
        }

        private void refreshCustomerAdresses()
        {
            customer_addresses.Clear();
            foreach (var address in _customer_addresses)
                customer_addresses.Add(address);
        }


        public AddCustomerViewModel(INavigationService navigationService, Customer customer)
        {
            CancelCommand = new NavigateCommand(navigationService);
            AddCustomer = new NavigateCommand(navigationService);

            _customer = new Customer();

            _customerAddress = new CustomerAddress();
            customer_addresses = new BindingList<CustomerAddress>();
            _customer_addresses = new List<CustomerAddress>();

            HeaderText = "Dodaj Klienta";
            if (customer != null)
            {
                edit = true;
                HeaderText = "Edytuj Klienta";

                _customer = customer;
                Name = _customer.name;
                Surname = _customer.surname;
                Email = _customer.email;
                Phone = _customer.phone;

                _customerAddress.customer_id = _customer.id;

                customer_addresses = new BindingList<CustomerAddress>(CustomerProcessor.getCustomerAdresses(_customerAddress).Result);
                _customer_addresses.AddRange(customer_addresses);
            }
        }
    }
}
