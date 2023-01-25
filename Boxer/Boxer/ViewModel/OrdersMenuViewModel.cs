﻿using Boxer.Commands;
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
    class OrdersMenuViewModel : BaseViewModel
    {
        public ICommand NavigateBackCommand { get; }
        public ICommand NavigateOrdersCommand { get; }
        public ICommand NavigateClientsCommand { get; }

        public OrdersMenuViewModel(INavigationService mainMenuNavigationService, INavigationService ordersNavigationService, INavigationService customersNavigationService)
        {
            NavigateBackCommand = new NavigateCommand(mainMenuNavigationService);
            NavigateOrdersCommand = new NavigateCommand(ordersNavigationService);
            NavigateClientsCommand = new NavigateCommand(customersNavigationService);
        }
    }
}
