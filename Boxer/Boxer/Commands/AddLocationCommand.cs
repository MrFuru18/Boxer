﻿using ApiLibrary.Model;
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
                    MessageBox.Show(result);
            }
        }

        private bool checkIfCorrect()
        {
            if (_location.sector == null)
            {
                MessageBox.Show("Sektor nie może być pusty");
                return false;
            }
            if (_location.aisle == null)
            {
                MessageBox.Show("Alejka nie może być pusta");
                return false;
            }
            if (_location.unit == null)
            {
                MessageBox.Show("Oznaczenie regału nie może być puste");
                return false;
            }
            if (_location.level == null)
            {
                MessageBox.Show("Poziom nie może być pusty");
                return false;
            }
            if (_location.position == null)
            {
                MessageBox.Show("Pozycja nie może być pusta");
                return false;
            }
            return true;
        }
    }
}
