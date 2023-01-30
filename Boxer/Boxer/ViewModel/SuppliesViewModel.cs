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
    class SuppliesViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        ModalNavigationStore _modalNavigationStore;

        public ObservableCollection<Supply> supplies { get; set; }
        private List<Supply> _supplies { get; set; }
        private Supply supply = null;

        public ObservableCollection<SupplyItem> supply_items { get; set; }
        public List<SupplyItem> _supply_items { get; set; }
        private SupplyItem supplyItem = new SupplyItem();

        public ICommand NavigateBackCommand { get; }
        public ICommand NewSupply { get; }
        private ICommand _editSupply;
        public ICommand EditSupply
        {
            get
            {
                return _editSupply ?? (_editSupply = new RelayCommand((p) =>
                {
                    _modalNavigationStore.CurrentViewModel = new AddSupplyViewModel(new CloseModalNavigationService(_modalNavigationStore), SelectedSupply);

                }, p => true));

            }
        }

        private Supply _selectedSupply;
        public Supply SelectedSupply
        {
            get { return _selectedSupply; }
            set
            {
                _selectedSupply = value;
                onPropertyChanged(nameof(SelectedSupply));

                if (SelectedSupply != null)
                    loadSupplyItems();
            }
        }

        private void loadSupplyItems()
        {
            supplyItem.supply_id = SelectedSupply.id;
            _supply_items = new List<SupplyItem>(SupplyProcessor.getSupplyItems(supplyItem).Result);

            supply_items.Clear();
            foreach (var item in _supply_items)
                supply_items.Add(item);
        }

        public SuppliesViewModel(INavigationService suppliesMenuNavigationService, INavigationService addSupplyNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addSupplyNavigationService;
            _modalNavigationStore = modalNavigationStore;
            
            _modalNavigationStore.CurrentViewModelClosed += OnCurrentModalViewModelClosed;
            
            NavigateBackCommand = new NavigateCommand(suppliesMenuNavigationService);
            NewSupply = new NavigateCommand(addSupplyNavigationService);

            supply = new Supply();
            supplies = new ObservableCollection<Supply>(SupplyProcessor.getAllSupplies(supply).Result);
            _supplies = new List<Supply>(supplies);

            supplyItem = new SupplyItem();
            supply_items = new ObservableCollection<SupplyItem>();

            if (_supplies.Count > 0)
            {
                SelectedSupply = _supplies[0];
            }
        }

        
        private void OnCurrentModalViewModelClosed()
        {
            _supplies = new List<Supply>(SupplyProcessor.getAllSupplies(supply).Result);

            supplies.Clear();
            foreach (var sup in _supplies)
                supplies.Add(sup);

            if (_supplies.Count > 0)
            {
                SelectedSupply = _supplies[0];
            }
        }
    }
}
