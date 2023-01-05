using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    class OrderItem
    {
        public Guid  order_id { get; set; }
        public Guid product_id { get; set; }
        public int quantity { get; set; }
    }
}
