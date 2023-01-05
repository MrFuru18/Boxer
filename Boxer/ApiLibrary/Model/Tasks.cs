using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    class Tasks
    {
        public Guid employee_id { get; set; }
        public string type { get; set; }
        public Guid order_id { get; set; }
        public Guid supply_id { get; set; }
        public string remarks { get; set; }
    }
}
