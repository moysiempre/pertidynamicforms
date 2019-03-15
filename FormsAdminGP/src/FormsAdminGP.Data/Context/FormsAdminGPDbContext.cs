using FormsAdminGP.Domain;
using Microsoft.EntityFrameworkCore;

namespace FormsAdminGP.Data.Context
{
    public class FormsAdminGPDbContext : DbContext
    {
        public FormsAdminGPDbContext(DbContextOptions<FormsAdminGPDbContext> options)
               : base(options)
        {
        }

        public virtual DbSet<LandingPage> LandingPages { get; set; }
        public virtual DbSet<FormHd> FormHds { get; set; }
        public virtual DbSet<FormDetail> FormDetails { get; set; }
        public virtual DbSet<FormHdLandingPage> FormHdLandingPage { get; set; }
        public virtual DbSet<InfoRequest> InfoRequests { get; set; }
        public virtual DbSet<DDLCatalog> DDLCatalogs { get; set; }

        // SECURITY
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }
        public virtual DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);


            // Custom application mappings  
            builder.Entity<LandingPage>(entity => {
                entity.ToTable("LandingPages", "landing");
                entity.Property(e => e.Name).HasMaxLength(225).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(450);
                entity.Property(e => e.FilePath).HasMaxLength(450).IsRequired();
                entity.Property(e => e.TypeId).IsRequired();
            });

            builder.Entity<FormHd>(entity =>
            {
                entity.ToTable("FormHds", "landing");
                entity.Property(e => e.Name).HasMaxLength(225).IsRequired();
                entity.Property(e => e.Title).HasMaxLength(225).IsRequired();
                entity.Property(e => e.FilePath).HasMaxLength(450).IsRequired();

                entity.HasMany(p => p.FormDetails)
                        .WithOne()
                        .HasForeignKey(b => b.FormHdId);
            });
          
            builder.Entity<FormDetail>(entity =>
            {
                entity.ToTable("FormDetails", "landing");
                entity.Property(e => e.FieldLabel).HasMaxLength(225).IsRequired();
                entity.Property(e => e.FieldTypeId).HasMaxLength(50).IsRequired();
            });

            builder.Entity<InfoRequest>(entity =>
            {
                entity.ToTable("InfoRequests", "landing");
                entity.Property(e => e.InfoRequestData).IsRequired();
                entity.Property(e => e.LandingPageId).IsRequired();
            });

            builder.Entity<FormHdLandingPage>(entity =>
            {
                entity.HasKey(pc => new { pc.FormHdId, pc.LandingPageId });
                entity.HasOne(pc => pc.FormHd).WithMany().HasForeignKey(pc => pc.FormHdId);
                entity.HasOne(pc => pc.LandingPage).WithMany().HasForeignKey(pc => pc.LandingPageId);
            });

            builder.Entity<DDLCatalog>(entity =>
            {
                entity.ToTable("DDLCatalogs", "landing");
                entity.Property(e => e.Name).HasMaxLength(225).IsRequired();
            });
 

            // SECURITY Custom application mappings
            builder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "security");
                entity.Property(e => e.Username).HasMaxLength(450).IsRequired();
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.SerialNumber).HasMaxLength(450);
            });

            builder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles", "security");
                entity.Property(e => e.Name).HasMaxLength(450).IsRequired();
                entity.HasIndex(e => e.Name).IsUnique();
            });

            builder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRoles", "security");
                entity.HasKey(e => new { e.UserId, e.RoleId });
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.RoleId);
                entity.Property(e => e.UserId);
                entity.Property(e => e.RoleId);
                entity.HasOne(d => d.Role).WithMany(p => p.UserRoles).HasForeignKey(d => d.RoleId);
                entity.HasOne(d => d.User).WithMany(p => p.UserRoles).HasForeignKey(d => d.UserId);
            });
           
            builder.Entity<UserToken>(entity =>
            {
                entity.ToTable("UserTokens", "security");
                entity.HasOne(ut => ut.User)
                      .WithMany(u => u.UserTokens)
                      .HasForeignKey(ut => ut.UserId);

                entity.Property(ut => ut.RefreshTokenIdHash).HasMaxLength(450).IsRequired();
                entity.Property(ut => ut.RefreshTokenIdHashSource).HasMaxLength(450);
            });


        }
    }
}
