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
            if (checkIfCorrect())
            {
                string result = LocationProcessor.addLocation(_location).Result;
                if (result == "Created")
                    _navigationService.Navigate();
                else
                    MessageBox.Show("Nie udało się dodać lokalizacji");
            }
        }

        private bool checkIfCorrect()
        {
            if (string.IsNullOrWhiteSpace(_location.sector))
            {
                MessageBox.Show("Sektor nie może być pusty");
                return false;
            }
            if (string.IsNullOrWhiteSpace(_location.aisle))
            {
                MessageBox.Show("Alejka nie może być pusta");
                return false;
            }
            if (string.IsNullOrWhiteSpace(_location.unit))
            {
                MessageBox.Show("Oznaczenie regału nie może być puste");
                return false;
            }
            if (string.IsNullOrWhiteSpace(_location.level))
            {
                MessageBox.Show("Poziom nie może być pusty");
                return false;
            }
            if (string.IsNullOrWhiteSpace(_location.position))
            {
                MessageBox.Show("Pozycja nie może być pusta");
                return false;
            }
            return true;
        }
    }
}
