using FormsAdminGP.Common.Enums;
using FormsAdminGP.Domain;
using System.ComponentModel.DataAnnotations;

namespace FormsAdminGP.Services.DTO
{
    public class FormDetailDto : EntityDto<string>
    {
        [Required]
        public string FormHdId { get; set; }
        [Required]
        public string FieldTypeId { get; set; }         
        [Required]
        public string FieldLabel { get; set; }
        [Required]
        public byte Order { get; set; }
        public bool IsRequired { get; set; }        
    }

    public class FieldTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

}