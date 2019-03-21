using FormsAdminGP.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FormsAdminGP.Services.DTO
{
    public partial class FormDetailDto : Entity<string>
    {
       
        public string FormHdId { get; set; }
        [Required]
        public string FieldTypeId { get; set; }         
        [Required]
        public string FieldLabel { get; set; }
        [Required]
        public byte Order { get; set; } = 1;
        public bool IsRequired { get; set; } = true;
        public string Name => (!string.IsNullOrEmpty(FieldLabel))? FieldLabel.Replace(" ", "").ToLower() : string.Empty;

        public virtual ICollection<DDLCatalogDto> DDLCatalogs { get; set; }       

    }

}