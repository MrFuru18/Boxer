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

    class LoginViewModel : BaseViewModel
    {
        private string _uid;
        public string Uid
        {
            get { return _uid; }
            set
            {
                _uid = value;
                onPropertyChanged(nameof(Uid));
            }
        }

        public string Password { get; set; }

        public ICommand Login { get; }

        public LoginViewModel(AccountStore accountStore, INavigationService mainMenuNavigationService)
        {
            Login = new LoginCommand(this, accountStore, mainMenuNavigationService);
          
        }
    }
}
