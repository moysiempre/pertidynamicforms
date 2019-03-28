using FormsAdminGP.Domain;
using System.ComponentModel.DataAnnotations;

namespace FormsAdminGP.Services.DTO
{
    public class LandingPageDto: Entity<string>
    {
        [Required]
        public string Name { get; set; }       
        public string Description { get; set; }
        public string FormHdId { get; set; }
        public FormHdDto FormHd { get; set; }
    }
}
