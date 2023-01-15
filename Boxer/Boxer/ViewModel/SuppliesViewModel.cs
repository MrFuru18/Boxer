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
    class SuppliesViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        ModalNavigationStore _modalNavigationStore;

        public ICommand NavigateBackCommand { get; }
        public ICommand NewSupply { get; }

        public SuppliesViewModel(INavigationService suppliesMenuNavigationService, INavigationService addSupplyNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addSupplyNavigationService;
            _modalNavigationStore = modalNavigationStore;

            NavigateBackCommand = new NavigateCommand(suppliesMenuNavigationService);
            NewSupply = new NavigateCommand(addSupplyNavigationService);

        }
    }
}
