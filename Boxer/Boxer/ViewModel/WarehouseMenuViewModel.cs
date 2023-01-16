using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    using BaseClass;
    using Boxer.Commands;
    using Boxer.Navigation;

    class WarehouseMenuViewModel : BaseViewModel
    {
        public ICommand NavigateBackCommand { get; }
        public ICommand NavigateInventoryCommand { get; }
        public ICommand NavigatePalletesCommand { get; }
        public ICommand NavigateShortagesCommand { get; }
        public ICommand NavigateLocationsCommand { get; }

        public WarehouseMenuViewModel(INavigationService mainMenuNavigationService, INavigationService locationsNavigationService)
        {
            NavigateBackCommand = new NavigateCommand(mainMenuNavigationService);
            NavigateLocationsCommand = new NavigateCommand(locationsNavigationService);
        }
    }
}
