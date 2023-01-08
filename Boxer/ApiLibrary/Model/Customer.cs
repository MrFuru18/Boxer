using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    class Customer
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }
}
