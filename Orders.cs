using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Craft
{
    public partial class Orders
    {
        public Orders()
        {
            Dispatch = new HashSet<Dispatch>();
            Ordermodifications = new HashSet<Ordermodifications>();
            Orderphotos = new HashSet<Orderphotos>();
        }

        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
        public int? IdPainting { get; set; }
        public int IdStage { get; set; }
        public int IdShop { get; set; }
        public int? IdSize { get; set; }
        public DateTime Orderdate { get; set; }
        public DateTime? Deadline { get; set; }
        public decimal? Cost { get; set; }
        public string Specification { get; set; }

        public virtual Painting IdPaintingNavigation { get; set; }
        public virtual Products IdProductNavigation { get; set; }
        public virtual Shops IdShopNavigation { get; set; }
        public virtual Sizes IdSizeNavigation { get; set; }
        public virtual Stages IdStageNavigation { get; set; }
        public virtual ICollection<Dispatch> Dispatch { get; set; }
        public virtual ICollection<Ordermodifications> Ordermodifications { get; set; }
        public virtual ICollection<Orderphotos> Orderphotos { get; set; }
    }
}
