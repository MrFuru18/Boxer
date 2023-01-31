using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model.ToCreate
{
    public class ToCreateInventory
    {
        public int id { get; set; }
        public int? location_id { get; set; }
        public int? product_id { get; set; }
        //public int? pallet_id { get; set; }
        public int? quantity { get; set; }
        public string remarks { get; set; }
        public int? employee_id { get; set; }
    }
}
