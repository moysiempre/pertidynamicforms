using FormsAdmin.Core;
using FormsAdmin.Core.DTO;
using FormsAdmin.RestfulAPI.Models;
using FormsAdmin.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormsAdmin.RestfulAPI.Controllers
{
    [Produces("application/json")]
    [Route("landing/api-security")]
    public class SecurityController : Controller
    {
        private readonly JwtSettings _settings = null;
        private readonly ISecurityManagerService _securityManagerService;
        public SecurityController(JwtSettings settings, ISecurityManagerService securityManagerService)
        {
            this._securityManagerService = securityManagerService;
            this._settings = settings;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]AppUser user)
        {
            IActionResult ret = null;
            AppUserAuth auth = new AppUserAuth();
            SecurityManager mgr = new SecurityManager(this._settings, this._securityManagerService);

            auth = mgr.ValidateUser(user);
            if (auth.IsAuthenticated)
            {
                ret = StatusCode(StatusCodes.Status200OK, auth);
            }
            else
            {
                ret = StatusCode(StatusCodes.Status404NotFound,
                                 "Invalid User Name/Password.");
            }
            return ret;
        }


        //[HttpPut("changePassword")]
        //[Authorize]
        //public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordViewModel request)
        //{
        //    var response = await _securityManagerService.ChangePassword(request);
        //    return Ok(response);
        //}

        //[HttpPut("resetPassword")]
        //[Authorize]
        //public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordViewModel request)
        //{
        //    var response = await _securityManagerService.ResetPassword(request);
        //    return Ok(response);
        //}

    }
}