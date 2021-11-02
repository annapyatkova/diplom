using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Craft
{
    public partial class Productphotos
    {
        public int IdProductphoto { get; set; }
        public int IdProduct { get; set; }

        public virtual Products IdProductNavigation { get; set; }
        public virtual Photos IdProductphotoNavigation { get; set; }
    }
}
