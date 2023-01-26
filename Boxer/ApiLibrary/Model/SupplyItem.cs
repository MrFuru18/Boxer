using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    public class SupplyItem
    {
        public int id { get; set; }
        public int supply_id { get; set; }
        public int? product_id { get; set; }
        public int? quantity { get; set; }
        public int? location_id { get; set; }
    }
}
