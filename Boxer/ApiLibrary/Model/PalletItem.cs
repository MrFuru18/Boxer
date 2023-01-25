using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    public class PalletItem
    {
        public int pallet_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
    }
}
