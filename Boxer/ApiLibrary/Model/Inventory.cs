using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    class Inventory
    {
        public Guid location_id { get; set; }
        public Guid product_id { get; set; }
        public Guid pallet_id { get; set; }
        public int quantity { get; set; }
        public string remarks { get; set; }
        public DateTime time { get; set; }
        public Guid employee_id { get; set; }
    }
}
