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

    class UserControl2ViewModel : BaseViewModel
    {
        Navigate _navigate;

        public UserControl2ViewModel(Navigate navigate)
        {
            _navigate = navigate;
        }

        private ICommand _navigateToUserControl1;
        public ICommand NavigateToUserControl1
        {
            get
            {
                return _navigateToUserControl1 ?? (_navigateToUserControl1 = new RelayCommand((p) =>
                {
                    _navigate.CurrentPage = new UserControl1ViewModel(_navigate);
                }, p => true));
            }
        }
    }
}
