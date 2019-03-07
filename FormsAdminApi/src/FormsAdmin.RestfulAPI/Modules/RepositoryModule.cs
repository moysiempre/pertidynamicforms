using FormsAdmin.Core.Interfaces;
using FormsAdmin.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FormsAdmin.RestfulAPI.Modules
{
    public class RepositoryModule
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<ILandingPageRepository, LandingPageRepository>();
            services.AddTransient<IFormHdRepository, FormHdRepository>();
            services.AddTransient<IFormDetailRepository, FormDetailRepository>();
            services.AddTransient<IInfoRequestRepository, InfoRequestRepository>();  
            //security
            services.AddTransient<ISecurityManagerRepository, SecurityManagerRepository>();
        }
    }
}
