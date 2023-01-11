using ApiLibrary.Repo;
using Boxer.Model;
using Boxer.Navigation;
using Boxer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Boxer.Commands
{
    class LogoutCommand : CommandBase
    {
        private readonly MainMenuViewModel _viewModel;
        private readonly AccountStore _accountStore;
        private readonly INavigationService _navigationService;

        public LogoutCommand(MainMenuViewModel viewModel, AccountStore accountStore, INavigationService loginNavigationService)
        {
            _viewModel = viewModel;
            _accountStore = accountStore;
            _navigationService = loginNavigationService;
        }

        public override void Execute(object p)
        {
            _ = EmployeeProcessor.logoutEmployee(_accountStore.CurrentAccount.accessToken);
            _accountStore.CurrentAccount = null;
            _navigationService.Navigate();
        }
    }
}
