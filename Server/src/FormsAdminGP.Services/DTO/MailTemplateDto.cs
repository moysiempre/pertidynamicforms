using FormsAdminGP.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FormsAdminGP.Services.DTO
{
    public class MailTemplateDto : Entity<string>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Subject { get; set; }
        
        public string Salut { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
