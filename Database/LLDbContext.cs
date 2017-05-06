namespace Landlords.Database
{
    using System;
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

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<PropertyDetails> PropertyDetails { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<ApplicationUserPortfolio> ApplicationUserPortfolios { get; set; }
    }
}