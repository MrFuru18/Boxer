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
    class AddManufacturerViewModel : BaseViewModel
    {
        public ICommand CancelCommand { get; }
        public ICommand AddManufacturer { get; }

        public AddManufacturerViewModel(INavigationService navigationService)
        {
            CancelCommand = new NavigateCommand(navigationService);
            AddManufacturer = new NavigateCommand(navigationService);
        }
    }
}
