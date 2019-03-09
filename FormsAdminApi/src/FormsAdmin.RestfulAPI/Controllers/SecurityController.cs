using FormsAdmin.Core;
using FormsAdmin.Core.DTO;
using FormsAdmin.RestfulAPI.Models;
using FormsAdmin.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FormsAdmin.RestfulAPI.Controllers
{
    [Produces("application/json")]
    [Route("landing/api-security")]
    [Authorize]
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
        [AllowAnonymous]
        public IActionResult Login([FromBody]AppUser user)
        {
            IActionResult ret = null;
            if (ModelState.IsValid)
            {
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
               
            }
            else
            {
                ret = StatusCode(StatusCodes.Status404NotFound,
                                     "Invalid User Name/Password.");
            }

            return ret;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterDto request)
        {
            if (ModelState.IsValid)
            {
                var response = await _securityManagerService.Register(request);
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
              
        }


        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordDto request)
        {
            if (ModelState.IsValid)
            {
                var response = await _securityManagerService.ChangePassword(request);
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
           
        }

        [HttpPut("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordDto request)
        {
            if (ModelState.IsValid)
            {
                var response = await _securityManagerService.ResetPassword(request);
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
           
        }

    }
}