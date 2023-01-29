using ApiLibrary.Model;
using ApiLibrary.Repo;
using Boxer.Model;
using Boxer.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Boxer.Commands
{
    class AddLocationCommand : CommandBase
    {

        private readonly INavigationService _navigationService;
        private Location _location;

        public AddLocationCommand(INavigationService navigationService, Location location)
        {
            _navigationService = navigationService;
            _location = location;
        }

        public override void Execute(object p)
        {
            string result = LocationProcessor.addLocation(_location).Result;
            if (result == "Created")
                _navigationService.Navigate();
            else
                MessageBox.Show(result);
        }
    }
}
