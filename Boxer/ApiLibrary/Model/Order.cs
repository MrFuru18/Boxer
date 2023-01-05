using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    class Order
    {
        public Guid customer_address_id { get; set; }
        public string remarks { get; set; }
        public DateTime time { get; set; }
    }
}
