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
        public string Data { get; set; }
        public string FieldType
        {
            get
            {
                var fieldType = string.Empty;
                if (!string.IsNullOrEmpty(FieldTypeId))
                {
                    switch (FieldTypeId)
                    {
                        case "name":
                            fieldType = "Texto";
                            break;
                        case "email":
                            fieldType = "Email";
                            break;
                        case "phone":
                            fieldType = "Teléfono";
                            break;
                        case "text":
                            fieldType = "Texto";
                            break;
                        case "select":
                            fieldType = "Selección";
                            break;
                        case "textarea":
                            fieldType = "Textarea";
                            break;
                        case "submit":
                            fieldType = "Botón";
                            break;
                    }
                }
               
                return fieldType;
            }
        }
        public virtual ICollection<DDLCatalogDto> DDLCatalogs { get; set; }       

    }

}