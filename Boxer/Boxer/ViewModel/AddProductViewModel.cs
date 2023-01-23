﻿using ApiLibrary.Model;
using Boxer.Commands;
using Boxer.Navigation;
using Boxer.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Boxer.ViewModel
{
    class AddProductViewModel : BaseViewModel
    {
        public string HeaderText { get; set; }
        public ICommand CancelCommand { get; }
        public ICommand AddProduct { get; }

        public AddProductViewModel(INavigationService navigationService, Product product)
        {
            CancelCommand = new NavigateCommand(navigationService);
            AddProduct = new NavigateCommand(navigationService);

            HeaderText = "Dodaj Produkt";

            if (product != null)
            {
                HeaderText = "Edytuj Produkt";
            }
        }
    }
}
