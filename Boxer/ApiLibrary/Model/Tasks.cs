using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    public class Tasks
    {
        public int id { get; set; }
        public int employee_id { get; set; }
        public string type { get; set; }
        public int order_id { get; set; }
        public int supply_id { get; set; }
        public string remarks { get; set; }
    }
}
