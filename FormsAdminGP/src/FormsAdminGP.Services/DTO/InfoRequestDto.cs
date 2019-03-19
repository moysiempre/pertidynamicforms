using FormsAdminGP.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace FormsAdminGP.Services.DTO
{
    public class InfoRequestDto : Entity<string>
    {
        [Required]
        public string InfoRequestData { get; set; }
        public string Email { get; set; }
        [Required]
        public string LandingPageId { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
