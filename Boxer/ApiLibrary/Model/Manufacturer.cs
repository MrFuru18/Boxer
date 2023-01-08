using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    class Manufacturer
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }
}
