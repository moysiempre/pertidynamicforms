using FormsAdminGP.Services.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FormsAdminGP.RestfulAPI.Controllers
{
    public abstract class BaseController : Controller
    {
        protected UserClaimDto CurrentUser
        {
            get
            {
                var user = User;
                return new UserClaimDto
                {
                    Id = user.FindFirst(x => x.Type == "jti")?.Value ?? "",
                    UserName = user.FindFirst(x => x.Type == ClaimTypes.Name)?.Value ?? "",
                    RoleName = user.FindFirst(x => x.Type == ClaimTypes.Role)?.Value ?? "",
                    UserId = user.FindFirst(x => x.Type == "userId")?.Value ?? "",
                    //RoleId = user.FindFirst(x => x.Type == "roleId")?.Value ?? "",                    
                    //PropertyId = user.FindFirst(x => x.Type == "propertyId")?.Value ?? "",
                    //UnitId = user.FindFirst(x => x.Type == "unitId")?.Value ?? ""
                };
            }
        }
    }
}
