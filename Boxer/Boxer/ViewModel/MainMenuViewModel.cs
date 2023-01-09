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
        public ICommand NavigateEmployeesPageCommand { get; }
        public ICommand NavigateLocationsPageCommand { get; }

        public MainMenuViewModel(INavigationService userControl1NavigationService, INavigationService userControl2NavigationService)
        {
            NavigateEmployeesPageCommand = new NavigateCommand(userControl1NavigationService);
            NavigateLocationsPageCommand = new NavigateCommand(userControl2NavigationService);
        }
    }
}
