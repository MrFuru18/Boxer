using ApiLibrary.Model;
using ApiLibrary.Model.ToCreate;
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
    class EditSupplyCommand : CommandBase
    {

        private readonly INavigationService _navigationService;
        private Supply _supply;


        public EditSupplyCommand(INavigationService navigationService, Supply supply)
        {
            _navigationService = navigationService;
            _supply = supply;
        }

        public override void Execute(object p)
        {
            ToCreateSupply ord = ObjectComparerUtility.Convert<Supply, ToCreateSupply>(_supply);
            string result = SupplyProcessor.updateSupply(ord).Result;
            if (result == "OK")
            {

                _navigationService.Navigate();
            }
            else
                MessageBox.Show(result);
        }

        
    }
}
