using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Landlords.Database;

namespace Landlords.Migrations
{
    [DbContext(typeof(LLDbContext))]
    [Migration("20170923062853_FixInvoiceDataTypes")]
    partial class FixInvoiceDataTypes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
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

                    b.Property<bool>("IsPermitted");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MiddleName");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecondaryPhoneNumber");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Title");

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

            modelBuilder.Entity("Model.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("Name");

                    b.Property<string>("Number");

                    b.Property<DateTime>("Opened");

                    b.Property<decimal>("OpeningBalance");

                    b.Property<Guid>("PortfolioId");

                    b.Property<string>("ProviderName");

                    b.Property<string>("SortCode");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Model.Entities.Agency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Agencies");
                });

            modelBuilder.Entity("Model.Entities.ApplicationUserPortfolio", b =>
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

            modelBuilder.Entity("Model.Entities.Checklist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<bool>("IsAvailableDownstream");

                    b.Property<bool>("IsPropertyMandatory");

                    b.Property<string>("Name");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Checklists");
                });

            modelBuilder.Entity("Model.Entities.ChecklistInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ChecklistId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<bool>("IsArchived");

                    b.Property<bool>("IsAvailableDownstream");

                    b.Property<bool>("IsPropertyMandatory");

                    b.Property<string>("Name");

                    b.Property<Guid>("PortfolioId");

                    b.Property<Guid?>("PropertyDetailsId");

                    b.HasKey("Id");

                    b.HasIndex("ChecklistId");

                    b.HasIndex("PortfolioId");

                    b.HasIndex("PropertyDetailsId");

                    b.ToTable("ChecklistInstances");
                });

            modelBuilder.Entity("Model.Entities.ChecklistItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ChecklistId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("DisplayText");

                    b.Property<bool>("IsExpanded");

                    b.Property<string>("Key");

                    b.Property<int>("Order");

                    b.Property<string>("Payload");

                    b.Property<string>("Template");

                    b.HasKey("Id");

                    b.HasIndex("ChecklistId");

                    b.ToTable("ChecklistItems");
                });

            modelBuilder.Entity("Model.Entities.ChecklistItemInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ChecklistInstanceId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("DisplayText");

                    b.Property<bool>("IsCompleted");

                    b.Property<bool>("IsExpanded");

                    b.Property<string>("Key");

                    b.Property<int>("Order");

                    b.Property<string>("Payload");

                    b.Property<string>("Template");

                    b.HasKey("Id");

                    b.HasIndex("ChecklistInstanceId");

                    b.ToTable("ChecklistItemInstances");
                });

            modelBuilder.Entity("Model.Entities.Conversation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<Guid>("ReceiverId");

                    b.Property<Guid>("SenderId");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("Model.Entities.ConversationMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ConversationId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("Message");

                    b.Property<Guid>("ReceiverId");

                    b.Property<DateTime?>("Seen");

                    b.Property<Guid>("SenderId");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("ConversationMessages");
                });

            modelBuilder.Entity("Model.Entities.EmailTemplate", b =>
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

            modelBuilder.Entity("Model.Entities.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Date");

                    b.Property<DateTime?>("Deleted");

                    b.Property<DateTime?>("DueDate");

                    b.Property<string>("Number");

                    b.Property<string>("PoNumber");

                    b.Property<Guid>("PortfolioId");

                    b.Property<decimal>("SubTotal");

                    b.Property<Guid>("SupplierId");

                    b.Property<decimal>("Total");

                    b.Property<decimal>("VAT");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Model.Entities.InvoiceLine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("Description");

                    b.Property<Guid>("InvoiceId");

                    b.Property<string>("Item");

                    b.Property<decimal>("Quantity");

                    b.Property<decimal>("Total");

                    b.Property<decimal>("UnitCost");

                    b.Property<decimal>("VAT");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoiceLines");
                });

            modelBuilder.Entity("Model.Entities.MaintenanceEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("Description");

                    b.Property<Guid>("MaintenanceRequestId");

                    b.Property<string>("Status");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("MaintenanceRequestId");

                    b.HasIndex("UserId");

                    b.ToTable("MaintenanceEntries");
                });

            modelBuilder.Entity("Model.Entities.MaintenanceRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("Description");

                    b.Property<bool>("IsArchived");

                    b.Property<Guid>("PropertyDetailsId");

                    b.Property<string>("Severity");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("PropertyDetailsId");

                    b.ToTable("MaintenanceRequests");
                });

            modelBuilder.Entity("Model.Entities.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("Message");

                    b.Property<Guid>("PortfolioId");

                    b.Property<Guid?>("PropertyDetailsId");

                    b.Property<Guid?>("ShortlistedPropertyId");

                    b.Property<Guid?>("TenancyId");

                    b.Property<Guid?>("TenantId");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioId");

                    b.HasIndex("PropertyDetailsId");

                    b.HasIndex("ShortlistedPropertyId");

                    b.HasIndex("TenancyId");

                    b.HasIndex("TenantId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Model.Entities.Permission", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("Description");

                    b.Property<string>("RouteId");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Model.Entities.Portfolio", b =>
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

            modelBuilder.Entity("Model.Entities.PropertyDetails", b =>
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

            modelBuilder.Entity("Model.Entities.PropertyImage", b =>
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

            modelBuilder.Entity("Model.Entities.ShortlistedProperty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<decimal>("Deposit");

                    b.Property<decimal>("ExpectedRentalIncome");

                    b.Property<decimal>("Fees");

                    b.Property<decimal>("Insurance");

                    b.Property<int>("LettableUnits");

                    b.Property<decimal>("ManagementCost");

                    b.Property<decimal>("MortgageInterestRate");

                    b.Property<Guid>("PortfolioId");

                    b.Property<decimal>("PricePaid");

                    b.Property<string>("Reference");

                    b.Property<decimal>("RepairsContingency");

                    b.Property<decimal>("ServiceCharge");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioId");

                    b.ToTable("ShortlistedProperties");
                });

            modelBuilder.Entity("Model.Entities.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<string>("AddressLine3");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("MainContactNumber");

                    b.Property<string>("Name");

                    b.Property<Guid>("PortfolioId");

                    b.Property<string>("PostCode");

                    b.Property<string>("SecondaryContactNumber");

                    b.Property<string>("TownOrCity");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Model.Entities.Tenancy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<DateTime>("EndDate");

                    b.Property<Guid>("PropertyDetailsId");

                    b.Property<decimal>("RentalAmount");

                    b.Property<string>("RentalFrequency")
                        .IsRequired();

                    b.Property<string>("RentalPaymentReference")
                        .IsRequired();

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("TenancyType")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("PropertyDetailsId");

                    b.ToTable("Tenancies");
                });

            modelBuilder.Entity("Model.Entities.Tenant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdditionalInformation");

                    b.Property<Guid>("ApplicationUserId");

                    b.Property<string>("CompanyName");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("DrivingLicenseReference");

                    b.Property<string>("EmploymentType");

                    b.Property<bool>("HasPets");

                    b.Property<bool>("IsAdult");

                    b.Property<bool>("IsLeadTenant");

                    b.Property<bool>("IsSmoker");

                    b.Property<string>("Occupation");

                    b.Property<string>("PassportReference");

                    b.Property<string>("WorkAddress");

                    b.Property<string>("WorkContactNumber");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("Model.Entities.TenantAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<string>("CountyOrRegion");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<int>("MonthsAtAddress");

                    b.Property<string>("Postcode")
                        .IsRequired();

                    b.Property<string>("Street")
                        .IsRequired();

                    b.Property<Guid>("TenantId");

                    b.Property<string>("TownOrCity")
                        .IsRequired();

                    b.Property<int>("YearsAtAddress");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("TenantAddresses");
                });

            modelBuilder.Entity("Model.Entities.TenantContact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comments");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("MainContactNumber")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Relationship")
                        .IsRequired();

                    b.Property<string>("SecondaryContactNumber");

                    b.Property<Guid>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("TenantContacts");
                });

            modelBuilder.Entity("Model.Entities.TenantTenancy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("TenancyId");

                    b.Property<Guid>("TenantId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.HasKey("Id", "TenancyId", "TenantId");

                    b.HasIndex("TenancyId");

                    b.HasIndex("TenantId");

                    b.ToTable("TenantTenancies");
                });

            modelBuilder.Entity("Model.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AccountId");

                    b.Property<decimal>("Balance");

                    b.Property<string>("Category");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("DestinationAccountNumber");

                    b.Property<string>("DestinationSortCode");

                    b.Property<decimal>("In");

                    b.Property<decimal>("Out");

                    b.Property<string>("PaymentType");

                    b.Property<string>("Reference");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Model.Entities.UserPermission", b =>
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

            modelBuilder.Entity("Model.Database.ApplicationUser", b =>
                {
                    b.HasOne("Model.Entities.Agency", "Agency")
                        .WithMany("Users")
                        .HasForeignKey("AgencyId")
                        .HasConstraintName("ForeignKey_ApplicationUser_Agency");
                });

            modelBuilder.Entity("Model.Entities.Account", b =>
                {
                    b.HasOne("Model.Entities.Portfolio", "Portfolio")
                        .WithMany()
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.ApplicationUserPortfolio", b =>
                {
                    b.HasOne("Model.Entities.Agency", "Agency")
                        .WithMany()
                        .HasForeignKey("AgencyId");

                    b.HasOne("Model.Entities.Portfolio", "Portfolio")
                        .WithMany("Users")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Database.ApplicationUser", "User")
                        .WithMany("Portfolios")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.Checklist", b =>
                {
                    b.HasOne("Model.Database.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.ChecklistInstance", b =>
                {
                    b.HasOne("Model.Entities.Checklist", "Checklist")
                        .WithMany()
                        .HasForeignKey("ChecklistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Entities.Portfolio", "Portfolio")
                        .WithMany()
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Entities.PropertyDetails", "PropertyDetails")
                        .WithMany()
                        .HasForeignKey("PropertyDetailsId");
                });

            modelBuilder.Entity("Model.Entities.ChecklistItem", b =>
                {
                    b.HasOne("Model.Entities.Checklist")
                        .WithMany("ChecklistItems")
                        .HasForeignKey("ChecklistId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.ChecklistItemInstance", b =>
                {
                    b.HasOne("Model.Entities.ChecklistInstance")
                        .WithMany("ChecklistItems")
                        .HasForeignKey("ChecklistInstanceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.Conversation", b =>
                {
                    b.HasOne("Model.Database.ApplicationUser", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Database.ApplicationUser", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.ConversationMessage", b =>
                {
                    b.HasOne("Model.Entities.Conversation", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Database.ApplicationUser", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Database.ApplicationUser", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.Invoice", b =>
                {
                    b.HasOne("Model.Entities.Portfolio", "Portfolio")
                        .WithMany()
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Entities.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.InvoiceLine", b =>
                {
                    b.HasOne("Model.Entities.Invoice", "Invoice")
                        .WithMany("Lines")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.MaintenanceEntry", b =>
                {
                    b.HasOne("Model.Entities.MaintenanceRequest", "MaintenanceRequest")
                        .WithMany("Entries")
                        .HasForeignKey("MaintenanceRequestId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Database.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.MaintenanceRequest", b =>
                {
                    b.HasOne("Model.Entities.PropertyDetails", "PropertyDetails")
                        .WithMany()
                        .HasForeignKey("PropertyDetailsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.Notification", b =>
                {
                    b.HasOne("Model.Entities.Portfolio", "Portfolio")
                        .WithMany()
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Entities.PropertyDetails", "PropertyDetails")
                        .WithMany()
                        .HasForeignKey("PropertyDetailsId");

                    b.HasOne("Model.Entities.ShortlistedProperty", "ShortlistedProperty")
                        .WithMany()
                        .HasForeignKey("ShortlistedPropertyId");

                    b.HasOne("Model.Entities.Tenancy", "Tenancy")
                        .WithMany()
                        .HasForeignKey("TenancyId");

                    b.HasOne("Model.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("Model.Entities.PropertyDetails", b =>
                {
                    b.HasOne("Model.Entities.Portfolio", "Portfolio")
                        .WithMany()
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.PropertyImage", b =>
                {
                    b.HasOne("Model.Entities.PropertyDetails", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.ShortlistedProperty", b =>
                {
                    b.HasOne("Model.Entities.Portfolio", "Portfolio")
                        .WithMany()
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.Supplier", b =>
                {
                    b.HasOne("Model.Entities.Portfolio", "Portfolio")
                        .WithMany()
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.Tenancy", b =>
                {
                    b.HasOne("Model.Entities.PropertyDetails", "PropertyDetails")
                        .WithMany()
                        .HasForeignKey("PropertyDetailsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.Tenant", b =>
                {
                    b.HasOne("Model.Database.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.TenantAddress", b =>
                {
                    b.HasOne("Model.Entities.Tenant", "Tenant")
                        .WithMany("Addresses")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.TenantContact", b =>
                {
                    b.HasOne("Model.Entities.Tenant", "Tenant")
                        .WithMany("Contacts")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.TenantTenancy", b =>
                {
                    b.HasOne("Model.Entities.Tenancy", "Tenancy")
                        .WithMany()
                        .HasForeignKey("TenancyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.Transaction", b =>
                {
                    b.HasOne("Model.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.UserPermission", b =>
                {
                    b.HasOne("Model.Entities.Permission", "Permission")
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
