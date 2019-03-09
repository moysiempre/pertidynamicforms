using FormsAdminGP.Core.Interfaces;
using FormsAdminGP.Core.Repositories;
using FormsAdminGP.Data.Repositories;
using FormsAdminGP.Data.Repositories.Security;
using FormsAdminGP.Services;
using FormsAdminGP.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FormsAdminGP.RestfulAPI
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddRepositoriesConfig(this IServiceCollection services)
        {
            services.AddScoped<ILandingPageRepository, LandingPageRepository>()
                .AddScoped<IFormHdRepository, FormHdRepository>()
                .AddScoped<IFormDetailRepository, FormDetailRepository>()
                .AddScoped<IInfoRequestRepository, InfoRequestRepository>();

            //SECURITY
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();

            return services;
        }

        public static IServiceCollection AddServicesConfig(this IServiceCollection services)
        {
            services.AddScoped<ILandindPageService, LandindPageService>()
                .AddScoped<IFormHdService, FormHdService>()
                .AddScoped<IInfoRequestService, InfoRequestService>();

            //SECURITY
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAntiForgeryCookieService, AntiForgeryCookieService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITokenStoreService, TokenStoreService>();
            services.AddScoped<ITokenValidatorService, TokenValidatorService>();
            services.AddScoped<ITokenFactoryService, TokenFactoryService>();            

            return services;
        }


    }
}
