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
    

    class AdminMenuViewModel : BaseViewModel
    {
        public ICommand NavigateBackCommand { get; }
        public ICommand NavigateAddEmployeeCommand { get; }

        public AdminMenuViewModel(INavigationService mainMenuNavigationService)
        {
            NavigateBackCommand = new NavigateCommand(mainMenuNavigationService);
        }
    }
}