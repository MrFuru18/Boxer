using Boxer.Commands;
using Boxer.Navigation;
using Boxer.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    class AddInventoryViewModel : BaseViewModel
    {
        public string HeaderText { get; set; }
        public ICommand CancelCommand { get; }
        public ICommand AddInventory { get; }

        public AddInventoryViewModel(INavigationService navigationService)
        {
            CancelCommand = new NavigateCommand(navigationService);
            AddInventory = new NavigateCommand(navigationService);

            HeaderText = "Dodaj Do Stanów";
        }
    }
}
