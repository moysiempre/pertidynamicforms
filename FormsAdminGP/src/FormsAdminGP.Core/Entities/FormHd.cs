using System.Collections.Generic;

namespace FormsAdminGP.Core.Entities
{
    public class FormHd : AuditableEntity<string>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<FormDetail> FormDetails { get; set; }
    }
}
