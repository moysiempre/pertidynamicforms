using FormsAdminGP.Core.Interfaces;
using FormsAdminGP.Core.Repositories;
using FormsAdminGP.Data.Repositories;
using FormsAdminGP.Data.Repositories.Interfaces;
using FormsAdminGP.Data.Repositories.Security;
using FormsAdminGP.Services;
using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.EmailSender;
using FormsAdminGP.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace FormsAdminGP.RestfulAPI
{
    public static class ServicesConfiguration
    {
        private static IConfiguration _configuration;
        public static IServiceCollection AddRepositoriesConfig(this IServiceCollection services)
        {
           

            services.AddScoped<ILandingPageRepository, LandingPageRepository>()
                .AddScoped<IFormHdRepository, FormHdRepository>()
                .AddScoped<IFormDetailRepository, FormDetailRepository>()
                .AddScoped<IInfoRequestRepository, InfoRequestRepository>()
                .AddScoped<IDDLCatalogRepository, DDLCatalogRepository>()
                .AddScoped<IMailTemplateRepository, MailTemplateRepository>();
            

            //SECURITY
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IDDLCatalogService, DDLCatalogService>();            
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();

            return services;
        }

        public static IServiceCollection AddServicesConfig(this IServiceCollection services, IConfiguration configuration)
        {
            _configuration = configuration;

            services.AddScoped<ILandindPageService, LandindPageService>()
                .AddScoped<IFormHdService, FormHdService>()
                .AddScoped<IInfoRequestService, InfoRequestService>()
                .AddScoped<IMailTemplateService, MailTemplateService>();

            //SECURITY
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAntiForgeryCookieService, AntiForgeryCookieService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITokenStoreService, TokenStoreService>();
            services.AddScoped<ITokenValidatorService, TokenValidatorService>();
            services.AddScoped<ITokenFactoryService, TokenFactoryService>();

            //EMAIL
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.Configure<EmailSettings>(_configuration.GetSection("EmailSettings"));
            services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));
            services.Configure<List<BaseDetailSettings>>(_configuration.GetSection("BaseDetailSettings"));
            return services;
        }


    }
}
