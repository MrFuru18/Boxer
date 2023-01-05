using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    class Shortage
    {
        public Guid product_id { get; set; }
        public Guid location_id { get; set; }
        public int quantity { get; set; }
        public string type { get; set; }
        public DateTime time { get; set; }
    }
}
