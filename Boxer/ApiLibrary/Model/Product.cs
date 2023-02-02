using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    public class Product
    {
        public int? id { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public int? manufacturer_id { get; set; }
        public int? category_id { get; set; }
        public string description { get; set; }
        public float? weight { get; set; }
        public string size { get; set; }
        public float? value { get; set; }
    }
}
