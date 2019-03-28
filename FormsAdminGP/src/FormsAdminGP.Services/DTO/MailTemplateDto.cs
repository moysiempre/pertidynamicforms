using FormsAdminGP.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormsAdminGP.Services.DTO
{
    public class MailTemplateDto : Entity<string>
    {
        public string Subject { get; set; }
        public string Salut { get; set; }
        public string Body { get; set; }
    }
}
