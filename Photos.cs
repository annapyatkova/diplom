using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Craft
{
    public partial class Photos
    {
        public Photos()
        {
            Productphotos = new HashSet<Productphotos>();
        }

        public int IdProductphoto { get; set; }
        public string Productphoto { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Productphotos> Productphotos { get; set; }
    }
}
