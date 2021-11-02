using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Craft
{
    public partial class Orderphotos
    {
        public int IdOrderphoto { get; set; }
        public int IdOrder { get; set; }
        public string Orderphoto { get; set; }
        public string Title { get; set; }

        public virtual Orders IdOrderNavigation { get; set; }
    }
}
