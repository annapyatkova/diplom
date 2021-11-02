using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Craft
{
    public partial class Paintcolors
    {
        public int IdPainting { get; set; }
        public int IdColor { get; set; }
        public string Comment { get; set; }

        public virtual Colors IdColorNavigation { get; set; }
        public virtual Painting IdPaintingNavigation { get; set; }
    }
}
