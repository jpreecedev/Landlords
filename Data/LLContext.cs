using Microsoft.EntityFrameworkCore;
using Model;

namespace Landlords.Data
{
    public class LLContext : DbContext, IDataContext
    {
        public LLContext(DbContextOptions options) :base(options)
        {
        }

        public DbSet<PropertyOverview> PropertyOverview { get; set; }
    }
}
