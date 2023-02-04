using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    public class Manufacturer
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }
}
