using FormsAdminGP.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FormsAdminGP.Services.DTO
{
    public class FormHdDto : Entity<string>
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }
        public string FilePath { get; set; }
        public virtual ICollection<FormDetail> FormDetails { get; set; }
    }
}
