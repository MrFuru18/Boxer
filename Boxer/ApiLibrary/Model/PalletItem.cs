using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    class PalletItem
    {
        public Guid pallet_id { get; set; }
        public Guid product_id { get; set; }
        public int quantity { get; set; }
    }
}
