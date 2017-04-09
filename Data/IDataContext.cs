namespace Landlords.Data
{
    using Microsoft.EntityFrameworkCore;
    using Model;

    public interface IDataContext
    {
        DbSet<PropertyOverview> PropertyOverview { get; set; }

        int SaveChanges();
    }
}
