using FormsAdmin.Core.Entities;
using System.Collections.Generic;

namespace FormsAdmin.Core
{
    public class AppRole : AuditableEntity<byte>
    {
        public string Description { get; set; }
        public IList<AppRoleClaim> Claims { get; set; }
    }
}
