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
    class SuppliesMenuViewModel : BaseViewModel
    {
        public ICommand NavigateBackCommand { get; }
        public ICommand NavigateSuppliesCommand { get; }
        public ICommand NavigateProductsCommand { get; }
        public ICommand NavigateManufacturersCommand { get; }
 

        public SuppliesMenuViewModel(INavigationService mainMenuNavigationService, INavigationService suppliesNavigationService, INavigationService productsNavigationService,
            INavigationService manufacturersNavigationService)
        {
            NavigateBackCommand = new NavigateCommand(mainMenuNavigationService);
            NavigateSuppliesCommand = new NavigateCommand(suppliesNavigationService);
            NavigateProductsCommand = new NavigateCommand(productsNavigationService);
            //NavigateManufacturerCommand = new NavigateCommand(manufacturersNavigationService);
        }
    }
}
