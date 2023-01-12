using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    using BaseClass;

    class WarehouseMenuViewModel : BaseViewModel
    {
        public ICommand NavigateAddProductToLocationCommand { get; }
        public ICommand NavigateAddProductToPalletCommand { get; }
        public ICommand NavigateAddPalletCommand { get; }
        public ICommand NavigateAddLocationCommand { get; }

        public WarehouseMenuViewModel()
        {

        }
    }
}
