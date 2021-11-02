using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Craft
{
    public partial class Dispatch
    {
        public int IdDispatch { get; set; }
        public int IdOrder { get; set; }
        public string Tracknumber { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }

        public virtual Orders IdOrderNavigation { get; set; }
    }
}
