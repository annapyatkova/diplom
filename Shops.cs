using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Craft
{
    public partial class Shops
    {
        public Shops()
        {
            Orders = new HashSet<Orders>();
        }

        public int IdShop { get; set; }
        public string Nameofshop { get; set; }
        public string Shopaddress { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
