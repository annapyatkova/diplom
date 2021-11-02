using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Craft
{
    public partial class Modifications
    {
        public Modifications()
        {
            Ordermodifications = new HashSet<Ordermodifications>();
        }

        public int IdModification { get; set; }
        public string Modificationname { get; set; }

        public virtual ICollection<Ordermodifications> Ordermodifications { get; set; }
    }
}
