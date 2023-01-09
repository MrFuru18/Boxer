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

    class UserControl1ViewModel : BaseViewModel
    {
        public ICommand NavigateUserControl2Command { get; }

        public UserControl1ViewModel(INavigationService userControl2NavigationService)
        {
            NavigateUserControl2Command = new NavigateCommand(userControl2NavigationService);
        }        
    }
}
