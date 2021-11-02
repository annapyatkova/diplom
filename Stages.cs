using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Craft
{
    public partial class Stages
    {
        public Stages()
        {
            Orders = new HashSet<Orders>();
        }

        public int IdStage { get; set; }
        public string Stagename { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
