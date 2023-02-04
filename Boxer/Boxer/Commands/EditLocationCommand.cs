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
            if (checkIfCorrect())
            {
                string result = LocationProcessor.updateLocation(_location).Result;
                if (result == "OK")
                    _navigationService.Navigate();
                else
                    MessageBox.Show(result);
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
            if (string.IsNullOrWhiteSpace(_location.level))
            {
                MessageBox.Show("Pozycja nie może być pusta");
                return false;
            }
            return true;
        }
    }
}
