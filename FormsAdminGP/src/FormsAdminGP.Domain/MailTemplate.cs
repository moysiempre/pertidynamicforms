using System;
using System.Collections.Generic;
using System.Text;

namespace FormsAdminGP.Domain
{
    public class MailTemplate: AuditableEntity<string>
    {
        public string Subject { get; set; }
        public string Salut { get; set; }
        public string Body { get; set; }
        
    }
}
