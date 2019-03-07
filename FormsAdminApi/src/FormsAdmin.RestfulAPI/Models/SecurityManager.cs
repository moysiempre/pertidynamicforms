using FormsAdmin.Core;
using FormsAdmin.Core.DTO;
using FormsAdmin.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace FormsAdmin.RestfulAPI.Models
{
    public class SecurityManager
    {
        private readonly JwtSettings _settings = null;
        private readonly ISecurityManagerService _securityManagerService;
        public SecurityManager(JwtSettings settings, ISecurityManagerService securityManagerService)
        {
            this._securityManagerService = securityManagerService;
            this._settings = settings;
        }

        public AppUserAuth ValidateUser(AppUser user)
        {
            var ret = new AppUserAuth();
            AppUserDto appUser = null;

            appUser = this._securityManagerService.GetUserWithClaims(user).Result;

            if (appUser != null)
            {
                // Build User Security Object
                ret = BuildUserAuthObject(appUser);
            }

            return ret;
        }

        protected AppUserAuth BuildUserAuthObject(AppUserDto authUser)
        {
            AppUserAuth ret = new AppUserAuth();
            List<AppRoleClaimDto> claims = new List<AppRoleClaimDto>();
            var claimsPrincipal = new List<Claim>();
            //var claimsPrincipal = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Role, authUser.Role.Description)
            //};

            ClaimsPrincipal.ClaimsPrincipalSelector = () =>
            {
                return new ClaimsPrincipal(new ClaimsIdentity(new GenericIdentity(authUser.UserName), claimsPrincipal));
            };

            // Set User Properties
            ret.UserId = authUser.Id;
            ret.UserName = authUser.UserName;
            ret.IsAuthenticated = true;
            ret.BearerToken = new Guid().ToString();
            //ret.RoleName = authUser.Role.Description;
            // Get all claims for this user
            //ret.Claims = authUser.Role.Claims.ToList();

            // Set JWT bearer token
            ret.BearerToken = BuildJwtToken(ret);

            return ret;
        }

        protected string BuildJwtToken(AppUserAuth authUser)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(_settings.Key));

            // Create standard JWT claims
            List<Claim> jwtClaims = new List<Claim>();
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, authUser.UserName));
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            //jwtClaims.Add(new Claim("role", authUser.RoleName.ToString()));
            jwtClaims.Add(new Claim("userId", authUser.UserId ?? ""));

            // Add custom claims
            //foreach (var claim in authUser.Claims)
            //{
            //    jwtClaims.Add(new Claim(claim.ClaimType, claim.ClaimValue));
            //}

            // Create the JwtSecurityToken object
            var token = new JwtSecurityToken(
              issuer: _settings.Issuer,
              audience: _settings.Audience,
              claims: jwtClaims,
              notBefore: DateTime.UtcNow,
              expires: DateTime.UtcNow.AddMinutes(_settings.MinutesToExpiration),
              signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            // Create a string representation of the Jwt token
            return new JwtSecurityTokenHandler().WriteToken(token); ;
        }
    }
}
