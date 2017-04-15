namespace Landlords.Database
{
    using System;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Model;

    public class LLDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, ILLDbContext
    {
        public LLDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<PropertyOverview> PropertyOverview { get; set; }
    }
}