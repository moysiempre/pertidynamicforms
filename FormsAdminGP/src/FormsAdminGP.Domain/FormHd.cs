using System.Collections.Generic;

namespace FormsAdminGP.Domain
{
    public class FormHd : AuditableEntity<string>
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public virtual ICollection<FormDetail> FormDetails { get; set; }
    }
}
