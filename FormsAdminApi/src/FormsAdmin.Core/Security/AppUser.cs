using FormsAdmin.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace FormsAdmin.Core
{
    //[Table("User", Schema = "Security")]
    public class AppUser : AuditableEntity<string>
    {

        [Required()]
        [StringLength(255)]
        public string UserName { get; set; }

        [Required()]
        [StringLength(255)]
        public string Password { get; set; }


        public byte RoleId { get; set; }
        public AppRole Role { get; set; }
    
    }
}
