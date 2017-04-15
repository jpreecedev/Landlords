namespace Landlords.Database
{
    using Microsoft.EntityFrameworkCore;
    using Model;

    public interface ILLDbContext
    {
        DbSet<PropertyOverview> PropertyOverview { get; set; }

        DbSet<ApplicationUser> Users { get; set; }
    }
}