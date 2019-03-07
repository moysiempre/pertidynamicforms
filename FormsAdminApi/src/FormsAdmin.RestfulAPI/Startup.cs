using AutoMapper;
using FormsAdmin.Core;
using FormsAdmin.Core.Context;
using FormsAdmin.Core.Entities;
using FormsAdmin.RestfulAPI.Filters;
using FormsAdmin.RestfulAPI.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FormsAdmin.RestfulAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private string[] withOrigins => new[] { "http://localhost:8080" };
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add OAuthConnection
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"], b => b.MigrationsAssembly("FormsAdmin.Core"));
            });
            services.AddScoped<DbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //AddMvc Core
            services.AddMvcCore()
               .AddAuthorization()
               .AddJsonFormatters()
               .AddApiExplorer() ;

            // Add cors
            services.AddCors();

            // AddAutoMapper
            services.AddAutoMapper();

            // Add Register modules.
            RepositoryModule.Register(services);
            ServiceModule.Register(services);
            SecurityModule.Register(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();

            //Configure Cors
            app.UseCors(builder =>
                           builder.WithOrigins(withOrigins)
                                  .AllowAnyHeader()
                                  .AllowAnyMethod()
                                  .AllowCredentials()
                                  );

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
