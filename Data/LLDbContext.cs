namespace Landlords.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Model;

    public class LLDbContext : IdentityDbContext, IDataContext
    {
        public LLDbContext(DbContextOptions<LLDbContext> options) : base(options)
        {
        }

        public DbSet<PropertyOverview> PropertyOverview { get; set; }
    }
}