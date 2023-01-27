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
    class AddManufacturerCommand : CommandBase
    {

        private readonly INavigationService _navigationService;
        private Manufacturer _manufacturer;

        public AddManufacturerCommand(INavigationService navigationService, Manufacturer manufacturer)
        {
            _navigationService = navigationService;
            _manufacturer = manufacturer;
        }

        public override void Execute(object p)
        {
            string result = ManufacturerProcessor.addManufacturer(_manufacturer).Result;
            if (result == "Created")
                _navigationService.Navigate();
            else
                MessageBox.Show(result);
        }
    }
}
