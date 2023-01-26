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
using System.Windows.Input;

namespace Boxer.ViewModel
{
    class AddSupplyViewModel : BaseViewModel
    {
        private bool edit = false;
        public string HeaderText { get; set; }
        private Supply _supply = new Supply();

        public BindingList<SupplyItem> supply_items { get; set; }
        private List<SupplyItem> _supply_items { get; set; }
        private SupplyItem _supplyItem = new SupplyItem();

        private Product _product = new Product();

        public ICommand CancelCommand { get; }
        public ICommand AddSupply { get; }

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

                        ProductId = "";
                        Quantity = "";
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

                if (ProductId != "")
                    _supplyItem.product_id = int.Parse(ProductId.ToString());
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
                    _supplyItem.quantity = int.Parse(Quantity);
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
            CancelCommand = new NavigateCommand(navigationService);
            AddSupply = new NavigateCommand(navigationService);

            _supply = new Supply();

            _supplyItem = new SupplyItem();
            supply_items = new BindingList<SupplyItem>();
            _supply_items = new List<SupplyItem>();

            _product = new Product();

            HeaderText = "Dodaj Dostawę";
            if (supply != null)
            {
                edit = true;
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
