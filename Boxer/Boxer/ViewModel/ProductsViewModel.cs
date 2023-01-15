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
    class ProductsViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        ModalNavigationStore _modalNavigationStore;

        public ICommand NavigateBackCommand { get; }
        public ICommand NewOrder { get; }

        public ProductsViewModel(INavigationService suppliesMenuNavigationService, INavigationService addProductNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addProductNavigationService;
            _modalNavigationStore = modalNavigationStore;

            NavigateBackCommand = new NavigateCommand(suppliesMenuNavigationService);
            NewOrder = new NavigateCommand(addProductNavigationService);

        }
    }
}
