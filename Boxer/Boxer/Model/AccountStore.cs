using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxer.Model
{
    class AccountStore
    {
        private Account _currentAccount;
        public Account CurrentAccount
        {
            get => _currentAccount;
            set
            {
                _currentAccount = value;
            }
        }

        public bool IsLoggedIn => _currentAccount != null;

        public bool IsAdmin => _currentAccount?.permissions == "admin";

        public bool IsManagement => (_currentAccount?.permissions != "worker") && (_currentAccount?.permissions != null);

    }
}
