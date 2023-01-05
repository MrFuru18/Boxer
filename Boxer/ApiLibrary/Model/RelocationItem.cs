using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    class RelocationItem
    {
        public Guid task_id { get; set; }
        public Guid inventory_id { get; set; }
        public Guid location_id { get; set; }
        public int quantity { get; set; }
    }
}
