using FormsAdmin.Core.Entities;
using FormsAdmin.Core.Responses;
using System.Collections.Generic;

namespace FormsAdmin.Core.DTO
{
    public class AppUserDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }
        public string Id { get; set; }
        public byte RoleId { get; set; }
        public AppRoleDto Role { get; set; }
    }

    public class AppUserAuth
    {
        public AppUserAuth() : base()
        {
            UserName = "Not authorized";
            BearerToken = string.Empty;
        }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string CustomerId { get; set; }
        public string BearerToken { get; set; }
        public bool IsAuthenticated { get; set; }

        public IList<AppRoleClaimDto> Claims { get; set; }
    }

    public class PasswordResponse : BaseResponse
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AppRoleClaimDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public byte RoleId { get; set; }
    }

    public class AppRoleDto : Entity<byte>
    {
        public string Description { get; set; }
        public IList<AppRoleClaimDto> Claims { get; set; }
    }
}
