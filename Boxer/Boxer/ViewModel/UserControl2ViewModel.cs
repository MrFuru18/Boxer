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

    class UserControl2ViewModel : BaseViewModel
    {
        public ICommand NavigateUserControl1Command { get; }

        public UserControl2ViewModel(INavigationService userControl1NavigationService)
        {
            NavigateUserControl1Command = new NavigateCommand(userControl1NavigationService);
        }
    }
}
