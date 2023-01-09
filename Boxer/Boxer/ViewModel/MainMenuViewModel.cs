using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Boxer.Navigation;
using Boxer.Commands;

namespace Boxer.ViewModel
{
    using BaseClass;
   
    class MainMenuViewModel : BaseViewModel
    {
        public ICommand NavigateSearchPageCommand { get; }
        public ICommand NavigateTasksPannelPageCommand { get; }

        public MainMenuViewModel(INavigationService userControl1NavigationService, INavigationService userControl2NavigationService)
        {
            NavigateSearchPageCommand = new NavigateCommand(userControl1NavigationService);
            NavigateTasksPannelPageCommand = new NavigateCommand(userControl2NavigationService);
        }
    }
}
