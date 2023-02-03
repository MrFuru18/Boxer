using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Boxer.Navigation;
using Boxer.Commands;
using Boxer.Model;

namespace Boxer.ViewModel
{
    using BaseClass;

    class MainMenuViewModel : BaseViewModel
    {
        public ICommand NavigateTasksPannelPageCommand { get; }
        public ICommand NavigateWarehousePannelPageCommand { get; }
        public ICommand NavigateOrdersPannelPageCommand { get; }
        public ICommand NavigateSuppliesPannelPageCommand { get; }
        public ICommand NavigateAdminPannelPageCommand { get; }
        public ICommand Logout { get; }

        public MainMenuViewModel(AccountStore accountStore, INavigationService tasksMenuNavigationService, 
            INavigationService warehouseMenuNavigationService, INavigationService ordersMenuNavigationService,
            INavigationService suppliesMenuNavigationService, INavigationService adminMenuNavigationService, INavigationService loginNavigationService)
        {
            NavigateTasksPannelPageCommand = new NavigateCommand(tasksMenuNavigationService);
            NavigateWarehousePannelPageCommand = new NavigateCommand(warehouseMenuNavigationService);
            NavigateOrdersPannelPageCommand = new NavigateCommand(ordersMenuNavigationService);
            NavigateSuppliesPannelPageCommand = new NavigateCommand(suppliesMenuNavigationService);
            NavigateAdminPannelPageCommand = new NavigateCommand(adminMenuNavigationService);
            Logout = new LogoutCommand(accountStore, loginNavigationService);
        }
    }
}
