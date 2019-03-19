using System;
using System.ComponentModel.DataAnnotations;

namespace FormsAdminGP.Domain
{
    public class InfoRequest: Entity<string>
    {       
        public string InfoRequestData { get; set; }
        public DateTime RequestDate { get; set; }
        public string LandingPageId { get; set; }
        public LandingPage LandingPage { get; set; }
    }
}
