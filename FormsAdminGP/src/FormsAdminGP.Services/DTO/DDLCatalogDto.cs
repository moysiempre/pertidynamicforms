using FormsAdminGP.Domain;
using System.ComponentModel.DataAnnotations;

namespace FormsAdminGP.Services.DTO
{
    public class DDLCatalogDto : Entity<string>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string FormDetailId { get; set; }
        
    }
}
