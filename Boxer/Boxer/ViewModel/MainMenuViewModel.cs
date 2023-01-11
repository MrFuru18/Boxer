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
        public ICommand Logout { get; }

        public MainMenuViewModel(AccountStore accountStore, INavigationService userControl1NavigationService, INavigationService userControl2NavigationService, INavigationService loginNavigationService)
        {
            NavigateSearchPageCommand = new NavigateCommand(userControl1NavigationService);
            NavigateTasksPannelPageCommand = new NavigateCommand(userControl2NavigationService);
            Logout = new LogoutCommand(this, accountStore, loginNavigationService);
        }
    }
}
