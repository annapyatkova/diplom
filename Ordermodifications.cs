using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Craft
{
    public partial class Ordermodifications
    {
        public int IdModification { get; set; }
        public int IdOrder { get; set; }

        public virtual Modifications IdModificationNavigation { get; set; }
        public virtual Orders IdOrderNavigation { get; set; }
    }
}
