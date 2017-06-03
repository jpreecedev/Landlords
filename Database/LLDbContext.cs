namespace Landlords.Database
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Model;
    using Model.Database;

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

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(p => p.Agency)
                .WithMany(b => b.Users)
                .HasForeignKey(p => p.AgencyId)
                .HasConstraintName("ForeignKey_ApplicationUser_Agency");

            modelBuilder.Entity<Checklist>()
                .HasMany(c => (List<ChecklistItem>) c.ChecklistItems);

            modelBuilder.Entity<ChecklistInstance>()
                .HasMany(c => (List<ChecklistItemInstance>) c.ChecklistItems);
            
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
    }
}