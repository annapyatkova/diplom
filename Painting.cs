using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Craft
{
    public partial class Painting
    {
        public Painting()
        {
            Orders = new HashSet<Orders>();
            Paintcolors = new HashSet<Paintcolors>();
            Paintingproduct = new HashSet<Paintingproduct>();
        }

        public int IdPainting { get; set; }
        public string Paintingname { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Paintcolors> Paintcolors { get; set; }
        public virtual ICollection<Paintingproduct> Paintingproduct { get; set; }
    }
}
