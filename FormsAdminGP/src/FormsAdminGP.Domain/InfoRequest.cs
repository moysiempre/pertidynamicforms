using System.ComponentModel.DataAnnotations;

namespace FormsAdminGP.Domain
{
    public class InfoRequest: Entity<string>
    {
        [Required]        
        public string InfoRequestData { get; set; }        
        public string LandingPageId { get; set; }
        public LandingPage LandingPage { get; set; }
    }
}
