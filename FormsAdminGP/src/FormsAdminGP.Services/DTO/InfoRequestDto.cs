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
        public string Name { get; set; }
        [Required]
        public string LandingPageId { get; set; }
        public string FileName { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestDateStr
        {
            get
            {
                return (RequestDate.Year > 1)? RequestDate.ToUniversalTime().ToString("dd/MM/yyyy HH:mm:ss"): string.Empty;
            }
        }
        public LandingPageDto LandingPage { get; set; }
        public string LandingPageName { get { return  (LandingPage != null) ? LandingPage.Name : string.Empty; } }
    }
}
