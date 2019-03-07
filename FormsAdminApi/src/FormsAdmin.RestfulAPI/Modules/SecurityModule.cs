using FormsAdmin.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
namespace FormsAdmin.RestfulAPI.Modules
{
    public class SecurityModule
    {
        private static IConfiguration _configuration;
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            _configuration = configuration;
            // Get JWT Token Settings from JwtSettings.json file
            JwtSettings settings;
            settings = GetJwtSettings();
            // Create singleton of JwtSettings
            services.AddSingleton<JwtSettings>(settings);

            // Register Jwt as the Authentication service
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key)),
                    ValidateIssuer = true,
                    ValidIssuer = settings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = settings.Audience,

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(settings.MinutesToExpiration)
                };
            });
        }

        public static JwtSettings GetJwtSettings()
        {
            JwtSettings settings = new JwtSettings();

            settings.Key = _configuration["JwtSettings:key"];
            settings.Audience = _configuration["JwtSettings:audience"];
            settings.Issuer = _configuration["JwtSettings:issuer"];
            settings.MinutesToExpiration = Convert.ToInt32(_configuration["JwtSettings:minutesToExpiration"]);

            return settings;
        }
    }
}
