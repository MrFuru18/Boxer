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
        public ICommand NavigateSuppliesCommand { get; }
        public ICommand NavigateProductsCommand { get; }
        public ICommand NavigateManufacturersCommand { get; }
 

        public SuppliesMenuViewModel(INavigationService suppliesNavigationService, INavigationService productsNavigationService, INavigationService manufacturersNavigationService)
        {
            NavigateSuppliesCommand = new NavigateCommand(suppliesNavigationService);
            NavigateProductsCommand = new NavigateCommand(productsNavigationService);
            NavigateManufacturersCommand = new NavigateCommand(manufacturersNavigationService);
        }
    }
}
