using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxer.ViewModel
{
    using BaseClass;
    using Boxer.Navigation;
    using System.Windows.Input;

    class UserControl1ViewModel : BaseViewModel
    {
        Navigate _navigate;

        public UserControl1ViewModel(Navigate navigate)
        {
            _navigate = navigate;
        }

        private ICommand _navigateToUserControl2;
        public ICommand NavigateToUserControl2
        {
            get
            {
                return _navigateToUserControl2 ?? (_navigateToUserControl2 = new RelayCommand((p) =>
                {
                    _navigate.CurrentPage = new UserControl2ViewModel(_navigate);
                }, p => true));
            }
        }
        
    }
}
