using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    public class CustomerAddress
    {
        public int? id { get; set; }
        public int? customer_id { get; set; }
        public string address_line_1 { get; set; }
        public string address_line_2 { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string postal_code { get; set; }
        public string country { get; set; }
    }
}
