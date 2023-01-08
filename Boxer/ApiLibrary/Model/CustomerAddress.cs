using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    class CustomerAddress
    {
        public Guid customer_id { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string postalCode { get; set; }
        public string country { get; set; }
    }
}
