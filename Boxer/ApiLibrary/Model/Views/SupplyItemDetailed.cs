using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model.Views
{
    public class SupplyItemDetailed
    {
        public int id { get; set; }
        public int? product_id { get; set; }
        public int? quantity { get; set; }
        public int? location_id { get; set; }
        public int? current_quantity { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public string manufacturer { get; set; }
        public string sector { get; set; }
        public string aisle { get; set; }
        public string unit { get; set; }
        public string level { get; set; }
        public string position { get; set; }
    }
}
