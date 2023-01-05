using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    class Product
    {
        public string sku { get; set; }
        public string name { get; set; }
        public Guid manufacturer_id { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public float weight { get; set; }
        public string size { get; set; }
        public float value { get; set; }
    }
}
