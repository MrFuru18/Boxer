using ApiLibrary.Model;
using ApiLibrary.Repo;
using Boxer.Commands;
using Boxer.Navigation;
using Boxer.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    class SuppliesViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        ModalNavigationStore _modalNavigationStore;

        public BindingList<Supply> supplies { get; set; }
        private List<Supply> _supplies { get; set; }
        private Supply supply = null;

        public ICommand NavigateBackCommand { get; }
        public ICommand NewSupply { get; }

        public SuppliesViewModel(INavigationService suppliesMenuNavigationService, INavigationService addSupplyNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addSupplyNavigationService;
            _modalNavigationStore = modalNavigationStore;

            NavigateBackCommand = new NavigateCommand(suppliesMenuNavigationService);
            NewSupply = new NavigateCommand(addSupplyNavigationService);

            supply = new Supply();
            supplies = new BindingList<Supply>(SupplyProcessor.getAllSupplies(supply).Result);
            _supplies = new List<Supply>(supplies);
        }
    }
}
