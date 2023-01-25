using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    public class Pallet
    {
        public int id { get; set; }
        public string number { get; set; }
        public int supply_id { get; set; }
        public DateTime time { get; set; }
        public string description { get; set; }

    }
}
