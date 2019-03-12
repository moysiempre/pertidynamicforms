﻿// <auto-generated />
using System;
using FormsAdminGP.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FormsAdminGP.Data.Migrations
{
    [DbContext(typeof(FormsAdminGPDbContext))]
    partial class FormsAdminGPDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FormsAdminGP.Domain.FormDetail", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FieldLabel");

                    b.Property<short>("FieldTypeId");

                    b.Property<string>("FormHdId");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsRequired");

                    b.Property<short>("Order");

                    b.HasKey("Id");

                    b.HasIndex("FormHdId");

                    b.ToTable("FormDetails","landing");
                });

            modelBuilder.Entity("FormsAdminGP.Domain.FormHd", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("FilePath");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<string>("Title");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("FormHds","landing");
                });

            modelBuilder.Entity("FormsAdminGP.Domain.FormHdLandingPage", b =>
                {
                    b.Property<string>("FormHdId");

                    b.Property<string>("LandingPageId");

                    b.Property<bool>("IsActive");

                    b.HasKey("FormHdId", "LandingPageId");

                    b.HasIndex("LandingPageId");

                    b.ToTable("FormHdLandingPage");
                });

            modelBuilder.Entity("FormsAdminGP.Domain.InfoRequest", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("InfoRequestData");

                    b.Property<bool>("IsActive");

                    b.Property<string>("LandingPageId");

                    b.HasKey("Id");

                    b.HasIndex("LandingPageId");

                    b.ToTable("InfoRequests","landing");
                });

            modelBuilder.Entity("FormsAdminGP.Domain.LandingPage", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("FilePath");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<short>("TypeId");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("LandingPages","landing");
                });

            modelBuilder.Entity("FormsAdminGP.Domain.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Roles","security");
                });

            modelBuilder.Entity("FormsAdminGP.Domain.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName");

                    b.Property<bool>("IsActive");

                    b.Property<DateTimeOffset?>("LastLoggedIn");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("SerialNumber")
                        .HasMaxLength(450);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users","security");
                });

            modelBuilder.Entity("FormsAdminGP.Domain.UserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles","security");
                });

            modelBuilder.Entity("FormsAdminGP.Domain.UserToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("AccessTokenExpiresDateTime");

                    b.Property<string>("AccessTokenHash");

                    b.Property<DateTimeOffset>("RefreshTokenExpiresDateTime");

                    b.Property<string>("RefreshTokenIdHash")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.Property<string>("RefreshTokenIdHashSource")
                        .HasMaxLength(450);

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserTokens","security");
                });

            modelBuilder.Entity("FormsAdminGP.Domain.FormDetail", b =>
                {
                    b.HasOne("FormsAdminGP.Domain.FormHd")
                        .WithMany("FormDetails")
                        .HasForeignKey("FormHdId");
                });

            modelBuilder.Entity("FormsAdminGP.Domain.FormHdLandingPage", b =>
                {
                    b.HasOne("FormsAdminGP.Domain.FormHd", "FormHd")
                        .WithMany()
                        .HasForeignKey("FormHdId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FormsAdminGP.Domain.LandingPage", "LandingPage")
                        .WithMany()
                        .HasForeignKey("LandingPageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FormsAdminGP.Domain.InfoRequest", b =>
                {
                    b.HasOne("FormsAdminGP.Domain.LandingPage", "LandingPage")
                        .WithMany()
                        .HasForeignKey("LandingPageId");
                });

            modelBuilder.Entity("FormsAdminGP.Domain.UserRole", b =>
                {
                    b.HasOne("FormsAdminGP.Domain.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FormsAdminGP.Domain.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FormsAdminGP.Domain.UserToken", b =>
                {
                    b.HasOne("FormsAdminGP.Domain.User", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
