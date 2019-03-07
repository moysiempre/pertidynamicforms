

using System.Collections.Generic;

namespace FormsAdmin.Core.Entities
{
    public class LandingPage : AuditableEntity<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public short TypeId { get; set; }

        //public virtual ICollection<FormHd> FormHds { get; set; }
    }
}



