using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    class Pallet
    {
        public Guid id { get; set; }
        public string number { get; set; }
        public Guid supply_id { get; set; }
        public DateTime time { get; set; }
        public string description { get; set; }

    }
}
