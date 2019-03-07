using FormsAdmin.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace FormsAdmin.Core
{
    //[Table("UserClaim", Schema = "Security")]
    public class AppRoleClaim : AuditableEntity<string>
    {

        [Required()]
        public byte RoleId { get; set; }

        [Required()]
        public string ClaimType { get; set; }

        [Required()]
        public string ClaimValue { get; set; }

        

    }
}
