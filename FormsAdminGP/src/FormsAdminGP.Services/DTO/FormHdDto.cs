using FormsAdminGP.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FormsAdminGP.Services.DTO
{
    public class FormHdDto : Entity<string>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual IEnumerable<FormDetail> FormDetails { get; set; }
    }
}
