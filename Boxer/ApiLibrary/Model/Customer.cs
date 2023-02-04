using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    public class Customer
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }
}
