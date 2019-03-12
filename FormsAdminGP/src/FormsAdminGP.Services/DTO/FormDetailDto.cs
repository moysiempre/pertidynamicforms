﻿using FormsAdminGP.Domain;
using System.ComponentModel.DataAnnotations;

namespace FormsAdminGP.Services.DTO
{
    public class FormDetailDto : Entity<string>
    {
        [Required]
        public string FormHdId { get; set; }
        [Required]
        public short FieldTypeId { get; set; }
        [Required]
        public string FieldLabel { get; set; }
        public short Order { get; set; }
        public bool IsRequired { get; set; }
    }
}