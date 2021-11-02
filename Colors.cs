using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Craft
{
    public partial class Colors
    {
        public Colors()
        {
            Paintcolors = new HashSet<Paintcolors>();
        }

        public int IdColor { get; set; }
        public string Colorname { get; set; }
        public string Colornumber { get; set; }

        public virtual ICollection<Paintcolors> Paintcolors { get; set; }
    }
}
