﻿using ApiLibrary.Model;
using ApiLibrary.Repo;
using Boxer.Commands;
using Boxer.Navigation;
using Boxer.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    class LocationsViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        ModalNavigationStore _modalNavigationStore;

        public BindingList<Location> locations { get; set; }
        private List<Location> _locations { get; set; }
        private Location location = null;

        public ICommand NavigateBackCommand { get; }
        public ICommand NewLocation { get; }
        private ICommand _editLocation;
        public ICommand EditLocation
        {
            get
            {
                return _editLocation ?? (_editLocation = new RelayCommand((p) =>
                {
                    _modalNavigationStore.CurrentViewModel = new AddLocationViewModel(new CloseModalNavigationService(_modalNavigationStore), SelectedLocation);

                }, p => true));

            }
        }

        private Location _selectedLocation;
        public Location SelectedLocation
        {
            get { return _selectedLocation; }
            set
            {
                _selectedLocation = value;
                onPropertyChanged(nameof(SelectedLocation));
            }
        }


        public LocationsViewModel(INavigationService warehouseMenuNavigationService, INavigationService addLocationNavigationService, ModalNavigationStore modalNavigationStore)
        {
            _navigationService = addLocationNavigationService;
            _modalNavigationStore = modalNavigationStore;

            NavigateBackCommand = new NavigateCommand(warehouseMenuNavigationService);
            NewLocation = new NavigateCommand(addLocationNavigationService);

            location = new Location();
            locations = new BindingList<Location>(LocationProcessor.getAllLocations(location).Result);
            _locations = new List<Location>(locations);

            if (_locations.Count > 0)
            {
                SelectedLocation = _locations[0];
            }
        }
    }
}