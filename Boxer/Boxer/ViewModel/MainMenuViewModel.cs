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
        public ICommand NavigateSearchPageCommand { get; }
        public ICommand NavigateTasksPannelPageCommand { get; }
        public ICommand NavigateWarehousePannelPageCommand { get; }
        public ICommand NavigateAdminPannelPageCommand { get; }
        public ICommand Logout { get; }

        public MainMenuViewModel(AccountStore accountStore, INavigationService warehouseMenuNavigationService, INavigationService adminMenuNavigationService, INavigationService loginNavigationService)
        {
            NavigateWarehousePannelPageCommand = new NavigateCommand(warehouseMenuNavigationService);
            NavigateAdminPannelPageCommand = new NavigateCommand(adminMenuNavigationService);
            Logout = new LogoutCommand(this, accountStore, loginNavigationService);
        }
    }
}
