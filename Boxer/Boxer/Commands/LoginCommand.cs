using ApiLibrary.Model;
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
    class LoginCommand : CommandBase
    {
        private readonly LoginViewModel _viewModel;
        private readonly AccountStore _accountStore;
        private readonly INavigationService _navigationService;

        public LoginCommand(LoginViewModel viewModel, AccountStore accountStore, INavigationService mainMenuNavigationService)
        {
            _viewModel = viewModel;
            _accountStore = accountStore;
            _navigationService = mainMenuNavigationService;
        }
        public override void Execute(object p)
        {
            LoginModel loginModel = new LoginModel()
            {
                uid = _viewModel.Uid,
                password = _viewModel.Password
            };

            if (string.IsNullOrWhiteSpace(loginModel.uid) || string.IsNullOrWhiteSpace(loginModel.password))
            {
                MessageBox.Show("Pola nie mogą być puste");
            }
            else
            {
                Account account = new Account();
                Access access = new Access();

                try
                {
                    access = EmployeeProcessor.loginEmployee(loginModel).Result;

                    if (access.success == "yes")
                    {
                        account.accessToken = access.accessToken;
                        account.uid = loginModel.uid;
                        Employee emp = new Employee();
                        emp.uid = loginModel.uid;
                        emp = EmployeeProcessor.getEmployee(emp).Result;
                        account.id = emp.id;
                        account.permissions = emp.permissions;

                        _accountStore.CurrentAccount = account;

                        if (_accountStore.IsLoggedIn)
                            _navigationService.Navigate();
                        else
                            MessageBox.Show("Coś poszło nie tak");
                    }
                    else
                    {
                        MessageBox.Show("Błędne dane logowania");
                    }
                }
                catch
                {
                    MessageBox.Show("Coś poszło nie tak");
                }

            }

           
        }
    }
}
