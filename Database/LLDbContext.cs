namespace Landlords.Database
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Model.Database;
    using Model.Entities;

    public class LLDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, ILLDbContext
    {
        public LLDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUserPortfolio>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<ApplicationUserPortfolio>()
                .HasKey(aup => new { aup.Id, aup.UserId, aup.PortfolioId });

            modelBuilder.Entity<TenantTenancy>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<TenantTenancy>()
                .HasKey(tt => new { tt.Id, tt.TenancyId, tt.TenantId });

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(p => p.Agency)
                .WithMany(b => b.Users)
                .HasForeignKey(p => p.AgencyId)
                .HasConstraintName("ForeignKey_ApplicationUser_Agency");

            modelBuilder.Entity<Checklist>()
                .HasMany(c => (List<ChecklistItem>)c.ChecklistItems);

            modelBuilder.Entity<ChecklistInstance>()
                .HasMany(c => (List<ChecklistItemInstance>)c.ChecklistItems);

            modelBuilder.Entity<Tenant>()
                .HasMany(c => (List<TenantAddress>)c.Addresses);

            modelBuilder.Entity<Tenant>()
                .HasMany(c => (List<TenantContact>) c.Contacts);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<PropertyDetails> PropertyDetails { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<ApplicationUserPortfolio> ApplicationUserPortfolios { get; set; }

        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<Checklist> Checklists { get; set; }
        public DbSet<ChecklistItem> ChecklistItems { get; set; }
        public DbSet<ChecklistInstance> ChecklistInstances { get; set; }
        public DbSet<ChecklistItemInstance> ChecklistItemInstances { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantAddress> TenantAddresses { get; set; }
        public DbSet<Tenancy> Tenancies { get; set; }
        public DbSet<TenantTenancy> TenantTenancies { get; set; }
        public DbSet<TenantContact> TenantContacts { get; set; }
        public DbSet<ShortlistedProperty> ShortlistedProperties { get; set; }
    }
}