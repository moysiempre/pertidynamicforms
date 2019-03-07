using FormsAdmin.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FormsAdmin.Core.DTO
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
