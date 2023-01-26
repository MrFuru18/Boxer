using ApiLibrary.Model;
using Boxer.Commands;
using Boxer.Model;
using Boxer.Navigation;
using Boxer.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    class AddLocationViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        private bool isNotEdit = true;
        public string HeaderText { get; set; }

        private Location _location = new Location();

        public ICommand CancelCommand { get; }
        private ICommand _addLocation;
        public ICommand AddLocation
        {
            get
            {
                return _addLocation ?? (_addLocation = new RelayCommand((p) =>
                {
                    if (isNotEdit)
                    {
                        AddLocationCommand addLocationCommand = new AddLocationCommand(_navigationService, _location);
                        addLocationCommand.Execute(true);
                    }
                    else
                    {
                        EditLocationCommand editLocationCommand = new EditLocationCommand(_navigationService, _location);
                        editLocationCommand.Execute(true);
                    }
                        

                }, p => true));

            }
        }

        private string _sector;
        public string Sector
        {
            get { return _sector; }
            set
            {
                _sector = value;
                onPropertyChanged(nameof(Sector));

                _location.sector = Sector;
            }
        }

        private string _aisle;
        public string Aisle
        {
            get { return _aisle; }
            set
            {
                _aisle = value;
                onPropertyChanged(nameof(Aisle));

                _location.aisle = Aisle;
            }
        }

        private string _unit;
        public string Unit
        {
            get { return _unit; }
            set
            {
                _unit = value;
                onPropertyChanged(nameof(Unit));

                _location.unit = Unit;
            }
        }

        private string _level;
        public string Level
        {
            get { return _level; }
            set
            {
                _level = value;
                onPropertyChanged(nameof(Level));

                _location.level = Level;
            }
        }

        private string _position;
        public string Position
        {
            get { return _position; }
            set
            {
                _position = value;
                onPropertyChanged(nameof(Position));

                _location.position = Position;
            }
        }

        private Sizes _size;
        public Sizes Size
        {
            get { return _size; }
            set
            {
                _size = value;
                onPropertyChanged(nameof(Size));

                _location.size = Size.ToString();
            }
        }

        private Availabilities _availability;
        public Availabilities Availability
        {
            get { return _availability; }
            set
            {
                _availability = value;
                onPropertyChanged(nameof(Availability));

                _location.availability = Availability.ToString();
            }
        }

        public AddLocationViewModel(INavigationService navigationService, Location location)
        {
            _navigationService = navigationService;
            CancelCommand = new NavigateCommand(navigationService);

            _location = new Location();


            _location.size = Size.ToString();
            _location.availability = Availability.ToString();

            HeaderText = "Dodaj Lokalizację";
            if (location != null)
            {
                isNotEdit = false;
                HeaderText = "Edytuj Lokalizację";

                _location = location;
                Sector = _location.sector;
                Aisle = _location.aisle;
                Level = _location.level;
                Unit = _location.unit;
                Position = _location.position;
                Size = (Sizes)Enum.Parse(typeof(Sizes), _location.size);
                Availability = (Availabilities)Enum.Parse(typeof(Availabilities), _location.availability);
            }

        }
    }
}
