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
    class ProductsViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        ModalNavigationStore _modalNavigationStore;
        public BindingList<Product> products { get; set; }
        private List<Product> _products { get; set; }
        private Product product = null;

        public ICommand NavigateBackCommand { get; }
        public ICommand NewProduct { get; }

        public ProductsViewModel(INavigationService suppliesMenuNavigationService, INavigationService addProductNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addProductNavigationService;
            _modalNavigationStore = modalNavigationStore;

            NavigateBackCommand = new NavigateCommand(suppliesMenuNavigationService);
            NewProduct = new NavigateCommand(addProductNavigationService);

            product = new Product();
            products = new BindingList<Product>(ProductProcessor.getAllProducts(product).Result);
            _products = new List<Product>(products);
        }
    }
}
