using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxer.Model
{
    public class OrderItemNoId
    {
        public int order_id { get; set; }
        public int? product_id { get; set; }
        public int? quantity { get; set; }
        public int? current_quantity { get; set; }
    }
}
