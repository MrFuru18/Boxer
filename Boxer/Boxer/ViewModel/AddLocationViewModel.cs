using ApiLibrary.Model;
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
    class AddLocationViewModel : BaseViewModel
    {
        public string HeaderText { get; set; }
        public ICommand CancelCommand { get; }
        public ICommand AddLocation { get; }

        public AddLocationViewModel(INavigationService navigationService, Location location)
        {
            CancelCommand = new NavigateCommand(navigationService);
            AddLocation = new NavigateCommand(navigationService);

            HeaderText = "Dodaj Lokalizację";
            if (location != null)
            {
                HeaderText = "Edytuj Lokalizację";
            }
        }
    }
}
