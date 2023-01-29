﻿using ApiLibrary.Model;
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
        private List<SupplyItem> _supplyItems;

        List<SupplyItem> _toAddSupplyItems = new List<SupplyItem>();
        List<SupplyItem> _toDeleteSupplyItems = new List<SupplyItem>();

        public EditSupplyCommand(INavigationService navigationService, Supply supply, List<SupplyItem> supplyItems)
        {
            _navigationService = navigationService;
            _supply = supply;
            _supplyItems = supplyItems;
        }

        public override void Execute(object p)
        {
            ToCreateSupply ord = ObjectComparerUtility.Convert<Supply, ToCreateSupply>(_supply);
            string result = SupplyProcessor.updateSupply(ord).Result;
            if (result == "OK")
            {
                checkItems();
                addItems();
                deleteItems();

                _navigationService.Navigate();
            }
            else
                MessageBox.Show(result);
        }

        private void checkItems()
        {
            SupplyItem ordIt = new SupplyItem();
            ordIt.supply_id = _supply.id;
            List<SupplyItem> oldSupplyItems = new List<SupplyItem>(SupplyProcessor.getSupplyItems(ordIt).Result);
            SupplyItemNoId addr = new SupplyItemNoId();
            _toDeleteSupplyItems.AddRange(oldSupplyItems);

            foreach (var item in _supplyItems)
            {
                bool found = false;
                addr = ObjectComparerUtility.Convert<SupplyItem, SupplyItemNoId>(item);

                foreach (var item2 in _toDeleteSupplyItems)
                {
                    if (ObjectComparerUtility.ObjectsAreEqual(addr, ObjectComparerUtility.Convert<SupplyItem, SupplyItemNoId>(item2)))
                    {
                        found = true;
                        _toDeleteSupplyItems.Remove(item2);
                        break;
                    }
                }

                if (!found)
                    _toAddSupplyItems.Add(item);
            }

        }

        private void addItems()
        {
            foreach (var item in _toAddSupplyItems)
            {
                string result = SupplyProcessor.addSupplyItem(item).Result;
                if (result != "Created")
                    MessageBox.Show(result);
            }
        }

        private void deleteItems()
        {
            foreach (var item in _toDeleteSupplyItems)
            {
                string result = SupplyProcessor.deleteSupplyItem(item).Result;
                if (result != "OK")
                    MessageBox.Show(result);
            }
        }
    }
}