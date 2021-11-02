using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Craft
{
    public partial class Sizes
    {
        public Sizes()
        {
            Orders = new HashSet<Orders>();
        }

        public int IdSize { get; set; }
        public string Size { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
