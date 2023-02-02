using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model.Views
{
    public class ProductDetailed
    {
        public int? id { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public int? manufacturer_id { get; set; }
        public string manufacturer_name { get; set; }
        public int? category_id { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public float? weight { get; set; }
        public string size { get; set; }
        public float? value { get; set; }
    }
}
