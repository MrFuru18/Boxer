using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    public class OrderItem
    {
        public int id { get; set; }
        public int  order_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
    }
}
