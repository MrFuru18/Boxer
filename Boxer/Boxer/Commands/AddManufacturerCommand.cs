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
            if (checkIfCorrect())
            {
                string result = ManufacturerProcessor.addManufacturer(_manufacturer).Result;
                if (result == "Created")
                    _navigationService.Navigate();
                else
                    MessageBox.Show(result);

            }
        }

        private bool checkIfCorrect()
        {
            
            if (string.IsNullOrWhiteSpace(_manufacturer.name))
            {
                MessageBox.Show("Nazwa producenta nie może być pusta");
                return false;
            }
            return true;
        }
    }
}
