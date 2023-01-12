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
        public ICommand NavigateProductsCommand { get; }
        public ICommand NavigateManufacturersCommand { get; }
        public ICommand NavigateSuppliesCommand { get; }

        public SuppliesMenuViewModel(INavigationService mainMenuNavigationService)
        {
            NavigateBackCommand = new NavigateCommand(mainMenuNavigationService);
        }
    }
}
