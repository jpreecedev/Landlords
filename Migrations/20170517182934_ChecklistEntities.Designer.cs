using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Landlords.Database;

namespace Landlords.Migrations
{
    [DbContext(typeof(LLDbContext))]
    [Migration("20170517182934_ChecklistEntities")]
    partial class ChecklistEntities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Model.Agency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Agencies");
                });

            modelBuilder.Entity("Model.ApplicationUserPortfolio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("UserId");

                    b.Property<Guid>("PortfolioId");

                    b.Property<Guid?>("AgencyId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.HasKey("Id", "UserId", "PortfolioId");

                    b.HasIndex("AgencyId");

                    b.HasIndex("PortfolioId");

                    b.HasIndex("UserId");

                    b.ToTable("ApplicationUserPortfolios");
                });

            modelBuilder.Entity("Model.Checklist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("Image");

                    b.Property<bool>("IsAvailableDownstream");

                    b.Property<bool>("IsPropertyMandatory");

                    b.HasKey("Id");

                    b.ToTable("Checklists");
                });

            modelBuilder.Entity("Model.ChecklistInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ChecklistId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("Image");

                    b.Property<bool>("IsArchived");

                    b.Property<bool>("IsAvailableDownstream");

                    b.Property<bool>("IsPropertyMandatory");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ChecklistId");

                    b.HasIndex("UserId");

                    b.ToTable("ChecklistInstances");
                });

            modelBuilder.Entity("Model.ChecklistItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ChecklistId");

                    b.Property<Guid?>("ChecklistId1");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("DisplayText");

                    b.Property<bool>("IsCompleted");

                    b.Property<bool>("IsExpanded");

                    b.Property<string>("Key");

                    b.Property<string>("Template");

                    b.HasKey("Id");

                    b.HasIndex("ChecklistId");

                    b.HasIndex("ChecklistId1");

                    b.ToTable("ChecklistItems");
                });

            modelBuilder.Entity("Model.ChecklistItemInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ChecklistId");

                    b.Property<Guid?>("ChecklistInstanceId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("DisplayText");

                    b.Property<bool>("IsCompleted");

                    b.Property<bool>("IsExpanded");

                    b.Property<string>("Key");

                    b.Property<string>("Template");

                    b.HasKey("Id");

                    b.HasIndex("ChecklistId");

                    b.HasIndex("ChecklistInstanceId");

                    b.ToTable("ChecklistItemInstances");
                });

            modelBuilder.Entity("Model.Database.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Model.Database.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<Guid?>("AgencyId");

                    b.Property<DateTime?>("AvailableFrom");

                    b.Property<DateTime?>("AvailableTo");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecondaryPhoneNumber");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("AgencyId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Model.EmailTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("Description");

                    b.Property<string>("Key");

                    b.Property<string>("Subject");

                    b.HasKey("Id");

                    b.ToTable("EmailTemplates");
                });

            modelBuilder.Entity("Model.Permission", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("Description");

                    b.Property<string>("RouteId");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Model.Portfolio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Portfolios");
                });

            modelBuilder.Entity("Model.PropertyDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Bedrooms");

                    b.Property<DateTime?>("ConstructionDate");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("CurrentDealExpirationDate");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("Furnishing");

                    b.Property<double?>("InterestRate");

                    b.Property<bool>("IsAvailableForLetting");

                    b.Property<decimal?>("MonthlyPayment");

                    b.Property<decimal?>("MortgageAmount");

                    b.Property<string>("MortgageProvider");

                    b.Property<string>("PaymentTerm");

                    b.Property<Guid>("PortfolioId");

                    b.Property<string>("PropertyCountry");

                    b.Property<string>("PropertyCountyOrRegion");

                    b.Property<string>("PropertyPostcode");

                    b.Property<string>("PropertyStreetAddress");

                    b.Property<string>("PropertyTownOrCity");

                    b.Property<string>("PropertyType");

                    b.Property<DateTime?>("PurchaseDate");

                    b.Property<decimal?>("PurchasePrice");

                    b.Property<string>("Reference");

                    b.Property<DateTime?>("SellingDate");

                    b.Property<decimal?>("SellingPrice");

                    b.Property<decimal?>("TargetRent");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioId");

                    b.ToTable("PropertyDetails");
                });

            modelBuilder.Entity("Model.PropertyImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("FileName")
                        .HasMaxLength(255);

                    b.Property<Guid>("PropertyId");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.ToTable("PropertyImages");
                });

            modelBuilder.Entity("Model.UserPermission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<Guid>("PermissionId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPermissions");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Model.Database.ApplicationRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Model.Database.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Model.Database.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Model.Database.ApplicationRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Database.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.ApplicationUserPortfolio", b =>
                {
                    b.HasOne("Model.Agency", "Agency")
                        .WithMany()
                        .HasForeignKey("AgencyId");

                    b.HasOne("Model.Portfolio", "Portfolio")
                        .WithMany("Users")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Database.ApplicationUser", "User")
                        .WithMany("Portfolios")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.ChecklistInstance", b =>
                {
                    b.HasOne("Model.Checklist", "Checklist")
                        .WithMany()
                        .HasForeignKey("ChecklistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Database.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.ChecklistItem", b =>
                {
                    b.HasOne("Model.Checklist", "Checklist")
                        .WithMany()
                        .HasForeignKey("ChecklistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Checklist")
                        .WithMany("ChecklistItems")
                        .HasForeignKey("ChecklistId1");
                });

            modelBuilder.Entity("Model.ChecklistItemInstance", b =>
                {
                    b.HasOne("Model.Checklist", "Checklist")
                        .WithMany()
                        .HasForeignKey("ChecklistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.ChecklistInstance")
                        .WithMany("ChecklistItems")
                        .HasForeignKey("ChecklistInstanceId");
                });

            modelBuilder.Entity("Model.Database.ApplicationUser", b =>
                {
                    b.HasOne("Model.Agency", "Agency")
                        .WithMany("Users")
                        .HasForeignKey("AgencyId")
                        .HasConstraintName("ForeignKey_ApplicationUser_Agency");
                });

            modelBuilder.Entity("Model.PropertyDetails", b =>
                {
                    b.HasOne("Model.Portfolio", "Portfolio")
                        .WithMany()
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.PropertyImage", b =>
                {
                    b.HasOne("Model.PropertyDetails", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.UserPermission", b =>
                {
                    b.HasOne("Model.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Database.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
