using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model.Views
{
    public class OrderItemDetailed
    {
        public int id { get; set; }
        public int? product_id { get; set; }
        public int? quantity { get; set; }
        public int? current_quantity { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public string manufacturer{ get; set; }
    }
}
