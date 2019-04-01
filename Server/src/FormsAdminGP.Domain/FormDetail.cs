using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormsAdminGP.Domain
{
    public class FormDetail : Entity<string>
    {
        public string FormHdId { get; set; }
        public string FieldTypeId { get; set; }
        public string FieldLabel { get; set; }
        public byte Order { get; set; }
        public bool IsRequired { get; set; }
        public bool IsMandatory { get; set; }
        [NotMapped]
        public virtual ICollection<DDLCatalog> DDLCatalogs { get; set; }
    }
}
