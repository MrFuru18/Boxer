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
    class EditManufacturerCommand : CommandBase
    {

        private readonly INavigationService _navigationService;
        private Manufacturer _manufacturer;

        public EditManufacturerCommand(INavigationService navigationService, Manufacturer manufacturer)
        {
            _navigationService = navigationService;
            _manufacturer = manufacturer;
        }

        public override void Execute(object p)
        {
            string result = ManufacturerProcessor.updateManufacturer(_manufacturer).Result;
            if (result == "OK")
                _navigationService.Navigate();
            else
                MessageBox.Show(result);
        }
    }
}
