using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    public class RelocationItem
    {
        public int task_id { get; set; }
        public int inventory_id { get; set; }
        public int location_id { get; set; }
        public int quantity { get; set; }
    }
}
