using FormsAdminGP.Common.Events;
using FormsAdminGP.Common.Utilities;
using FormsAdminGP.Domain;
using FormsAdminGP.RestfulAPI.Models;
using FormsAdminGP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FormsAdminGP.RestfulAPI.Controllers
{
    [Produces("application/json")]
    [Route("landing/api-security")]
    [Authorize]
    public class SecurityController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenStoreService _tokenStoreService;
        private readonly IAntiForgeryCookieService _antiforgery;
        private readonly ITokenFactoryService _tokenFactoryService;

        public SecurityController(
            IUserService userService,
            ITokenStoreService tokenStoreService,
            ITokenFactoryService tokenFactoryService,
            IAntiForgeryCookieService antiforgery)
        {
            _userService = userService;
            _userService.CheckArgumentIsNull(nameof(userService));

            _tokenStoreService = tokenStoreService;
            _tokenStoreService.CheckArgumentIsNull(nameof(tokenStoreService));

            _antiforgery = antiforgery;
            _antiforgery.CheckArgumentIsNull(nameof(antiforgery));

            _tokenFactoryService = tokenFactoryService;
            _tokenFactoryService.CheckArgumentIsNull(nameof(tokenFactoryService));
        }

        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]User loginUser)
        {
            if (loginUser == null)
            {
                return Ok(new { success = false, message = LoggingEvents.LOGIN_FAILED_MESSAGE });
            }

            var user = await _userService.FindUserAsync(loginUser.Username, loginUser.Password);
            if (user == null || !user.IsActive)
            {
                return Ok(new { success = false,  message = LoggingEvents.LOGIN_FAILED_MESSAGE });
            }

            var result = await _tokenFactoryService.CreateJwtTokensAsync(user);
            await _tokenStoreService.AddUserTokenAsync(user, result.RefreshTokenSerial, result.AccessToken, null);

            _antiforgery.RegenerateAntiForgeryCookies(result.Claims);

            return Ok(new { access_token = result.AccessToken, refresh_token = result.RefreshToken, success = true });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]User loginUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("user is not set.");
            }

            var response = await _userService.CreateAsync(loginUser, "");         
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("refreshToken")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> RefreshToken([FromBody]RefreshTokenViewModel request)
        {
           
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
            {
                return BadRequest("refreshToken is not set.");
            }

            var token = await _tokenStoreService.FindTokenAsync(request.RefreshToken);
            if (token == null)
            {
                return Unauthorized();
            }

            var result = await _tokenFactoryService.CreateJwtTokensAsync(token.User);
            await _tokenStoreService.AddUserTokenAsync(token.User, result.RefreshTokenSerial, result.AccessToken, _tokenFactoryService.GetRefreshTokenSerial(request.RefreshToken));

            _antiforgery.RegenerateAntiForgeryCookies(result.Claims);

            return Ok(new { access_token = result.AccessToken, refresh_token = result.RefreshToken });
        }

        [AllowAnonymous]
        [HttpGet("logout")]
        public async Task<bool> Logout(string refreshToken)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userIdValue = claimsIdentity.FindFirst(ClaimTypes.UserData)?.Value;

            // The Jwt implementation does not support "revoke OAuth token" (logout) by design.
            // Delete the user's tokens from the database (revoke its bearer token)
            await _tokenStoreService.RevokeUserBearerTokensAsync(userIdValue, refreshToken);

            _antiforgery.DeleteAntiForgeryCookies();

            return true;
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        public bool IsAuthenticated()
        {
            return User.Identity.IsAuthenticated;
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        public IActionResult GetUserInfo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            return Json(new { Username = claimsIdentity.Name });
        }

    }
}