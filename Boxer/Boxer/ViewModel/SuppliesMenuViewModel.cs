using Boxer.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    class SuppliesMenuViewModel : BaseViewModel
    {
        ICommand NavigateProductsCommand { get; }
        ICommand NavigateManufacturersCommand { get; }
        ICommand NavigateSuppliesCommand { get; }

        public SuppliesMenuViewModel()
        {

        }
    }
}
