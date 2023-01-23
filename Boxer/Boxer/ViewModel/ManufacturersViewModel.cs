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
    class ManufacturersViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        ModalNavigationStore _modalNavigationStore;

        public BindingList<Manufacturer> manufacturers { get; set; }
        private List<Manufacturer> _manufacturers { get; set; }
        private Manufacturer manufacturer = null;


        public ICommand NavigateBackCommand { get; }
        public ICommand NewManufacturer { get; }
        private ICommand _editManufacturer;
        public ICommand EditManufacturer
        {
            get
            {
                return _editManufacturer ?? (_editManufacturer = new RelayCommand((p) =>
                {
                    _modalNavigationStore.CurrentViewModel = new AddManufacturerViewModel(new CloseModalNavigationService(_modalNavigationStore), SelectedManufacturer);

                }, p => true));

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
            }
        }

        public ManufacturersViewModel(INavigationService suppliesMenuNavigationService, INavigationService addManufacturerNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addManufacturerNavigationService;
            _modalNavigationStore = modalNavigationStore;

            NavigateBackCommand = new NavigateCommand(suppliesMenuNavigationService);
            NewManufacturer = new NavigateCommand(addManufacturerNavigationService);

            manufacturer = new Manufacturer();
            manufacturers = new BindingList<Manufacturer>(ManufacturerProcessor.getAllManufacturers(manufacturer).Result);
            _manufacturers = new List<Manufacturer>(manufacturers);

            if (_manufacturers.Count > 0)
            {
                SelectedManufacturer = _manufacturers[0];
            }
        }
    }
}
