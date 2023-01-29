using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxer.Model
{
    public class RelocationItemNoId
    {
        public int task_id { get; set; }
        public int? inventory_id { get; set; }
        public int? location_id { get; set; }
        public int? quantity { get; set; }
        public int? current_quantity { get; set; }
    }
}
