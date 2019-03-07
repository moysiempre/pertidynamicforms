using FormsAdmin.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace FormsAdmin.Core.DTO
{
    public class InfoRequestDto : Entity<string>
    {
        [Required]
        public string InfoRequestData { get; set; }
        [Required]
        public string LandingPageId { get; set; }
    }
}
