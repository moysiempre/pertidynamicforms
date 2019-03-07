using FormsAdmin.Services;
using FormsAdmin.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FormsAdmin.RestfulAPI.Modules
{
    public class ServiceModule
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<ILandindPageService, LandindPageService>();
            services.AddTransient<IFormHdService, FormHdService>();
            services.AddTransient<IInfoRequestService, InfoRequestService>();
            //security
            services.AddTransient<ISecurityManagerService, SecurityManagerService>();
        }
    }
}
