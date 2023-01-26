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
    class AddInventoryViewModel : BaseViewModel
    {
        public string HeaderText { get; set; }

        private Inventory _inventory = new Inventory();
        public ICommand CancelCommand { get; }
        public ICommand AddInventory { get; }

        private string _locationId;
        public string LocationId
        {
            get { return _locationId; }
            set
            {
                _locationId = value;
                onPropertyChanged(nameof(LocationId));

                if (LocationId != "")
                    _inventory.location_id = int.Parse(LocationId);
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

                if (ProductId != "")
                    _inventory.product_id = int.Parse(ProductId);
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

                if (Quantity != "")
                    _inventory.quantity = int.Parse(Quantity);
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
            CancelCommand = new NavigateCommand(navigationService);
            AddInventory = new NavigateCommand(navigationService);

            _inventory = new Inventory();

            HeaderText = "Dodaj Do Stanów";
            if (inventory != null)
            {
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
