using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    class SupplyItem
    {
        public Guid supply_id { get; set; }
        public Guid product_id { get; set; }
        public int quantity { get; set; }
        public Guid location_id { get; set; }
    }
}
