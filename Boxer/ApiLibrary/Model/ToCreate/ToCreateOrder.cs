using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model.ToCreate
{
    public class ToCreateOrder
    {
        public int id { get; set; }
        public int? customer_address_id { get; set; }
        public string remarks { get; set; }
    }
}
