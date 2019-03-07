using FormsAdmin.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace FormsAdmin.Core.DTO
{
    public class LandingPageDto: Entity<string>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string FilePath { get; set; }
        [Required]
        public short TypeId { get; set; }
    }
}
