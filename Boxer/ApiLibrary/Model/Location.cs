using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    class Location
    {
        public string sector { get; set; }
        public string aisle { get; set; }
        public string unit { get; set; }
        public string level { get; set; }
        public string position { get; set; }
        public string size { get; set; }
        public string availability { get; set; }
        public Boolean palletSpace { get; set; }
    }
}
