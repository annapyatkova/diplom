using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Craft
{
    public partial class Products
    {
        public Products()
        {
            Orders = new HashSet<Orders>();
            Paintingproduct = new HashSet<Paintingproduct>();
            Productphotos = new HashSet<Productphotos>();
        }

        public int IdProduct { get; set; }
        public int IdCategory { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }

        public virtual Categories IdCategoryNavigation { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Paintingproduct> Paintingproduct { get; set; }
        public virtual ICollection<Productphotos> Productphotos { get; set; }
    }
}
