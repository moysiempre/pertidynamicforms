using FormsAdmin.Core.Entities;
using FormsAdmin.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;

namespace FormsAdmin.Core.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    

        public virtual DbSet<LandingPage> LandingPages { get; set; }
        public virtual DbSet<FormHd> FormHds { get; set; }
        public virtual DbSet<FormDetail> FormDetails { get; set; }
        public virtual DbSet<FormHdLandingPage> FormHdLandingPage { get; set; }
        public virtual DbSet<InfoRequest> InfoRequests { get; set; }

 

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            // SECURITY SCHEMA
            builder.Entity<ApplicationUser>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");

            builder.Entity<LandingPage>().ToTable("LandingPages", "dbo");
            builder.Entity<FormHd>().ToTable("FormHds", "dbo");
            builder.Entity<FormDetail>().ToTable("FormDetails", "dbo");
            builder.Entity<FormHd>()
                .HasMany(p => p.FormDetails)
                .WithOne()
                .HasForeignKey(b => b.FormHdId);
           
            builder.Entity<InfoRequest>().ToTable("InfoRequests", "dbo");         
            builder.Entity<FormHdLandingPage>().HasKey(pc => new { pc.FormHdId, pc.LandingPageId });

            builder.Entity<FormHdLandingPage>()
                .HasOne(pc => pc.FormHd)
                .WithMany()
                .HasForeignKey(pc => pc.FormHdId);

            builder.Entity<FormHdLandingPage>()
                .HasOne(pc => pc.LandingPage)
                .WithMany()
                .HasForeignKey(pc => pc.LandingPageId);
 
        }
 
    }
}
