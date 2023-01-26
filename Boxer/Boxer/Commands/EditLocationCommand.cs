using ApiLibrary.Model;
using ApiLibrary.Repo;
using Boxer.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Boxer.Commands
{
    class EditLocationCommand : CommandBase
    {
        private readonly INavigationService _navigationService;
        private Location _location;

        public EditLocationCommand(INavigationService navigationService, Location location)
        {
            _navigationService = navigationService;
            _location = location;
        }

        public override void Execute(object p)
        {
            string result = LocationProcessor.updateLocation(_location).Result;
            MessageBox.Show(result);

            _navigationService.Navigate();
        }
    }
}
