using FormsAdminGP.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace FormsAdminGP.Services.DTO
{
    public class InfoRequestDto : Entity<string>
    {
        [Required]
        public string InfoRequestData { get; set; }
        [Required]
        public string LandingPageId { get; set; }
    }
}
