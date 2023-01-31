using ApiLibrary.Model;
using ApiLibrary.Model.Views;
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
    class ProductsViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        ModalNavigationStore _modalNavigationStore;
        public ObservableCollection<ProductDetailed> products { get; set; }
        private List<ProductDetailed> _products { get; set; }
        private Product product = null;

        public ICommand NavigateBackCommand { get; }
        public ICommand NewProduct { get; }
        private ICommand _editProduct;
        public ICommand EditProduct
        {
            get
            {
                return _editProduct ?? (_editProduct = new RelayCommand((p) =>
                {
                    _modalNavigationStore.CurrentViewModel = new AddProductViewModel(new CloseModalNavigationService(_modalNavigationStore), SelectedProduct);

                }, p => true));

            }
        }

        private ProductDetailed _selectedProduct;
        public ProductDetailed SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                onPropertyChanged(nameof(SelectedProduct));
            }
        }


        public ProductsViewModel(INavigationService suppliesMenuNavigationService, INavigationService addProductNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addProductNavigationService;
            _modalNavigationStore = modalNavigationStore;

            _modalNavigationStore.CurrentViewModelClosed += OnCurrentModalViewModelClosed;

            NavigateBackCommand = new NavigateCommand(suppliesMenuNavigationService);
            NewProduct = new NavigateCommand(addProductNavigationService);

            product = new Product();
            products = new ObservableCollection<ProductDetailed>(ProductProcessor.getAllProductsDetailed(product).Result);
            _products = new List<ProductDetailed>(products);

            if (_products.Count > 0)
            {
                SelectedProduct = _products[0];
            }
        }

        private void OnCurrentModalViewModelClosed()
        {
            _products = new List<ProductDetailed>(ProductProcessor.getAllProductsDetailed(product).Result);

            products.Clear();
            foreach (var prod in _products)
                products.Add(prod);

            if (_products.Count > 0)
            {
                SelectedProduct = _products[0];
            }
        }
    }
}
