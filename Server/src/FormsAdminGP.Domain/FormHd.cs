using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormsAdminGP.Domain
{
    public class FormHd : AuditableEntity<string>
    {
         
        public string Name { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public virtual ICollection<FormDetail> FormDetails { get; set; }

        public string MailTemplateId { get; set; }
        public virtual MailTemplate MailTemplate { get; set; }

        [NotMapped]
        public virtual ICollection<LandingPage> LandingPages { get; set; }

    }
}
