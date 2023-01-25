﻿using Boxer.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxer.Navigation
{
    public interface INavigationStore
    {
        BaseViewModel CurrentViewModel { set; }
    }
}
