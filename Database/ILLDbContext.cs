namespace Landlords.Database
{
    using Microsoft.EntityFrameworkCore;
    using Model;
    using Model.Database;

    public interface ILLDbContext
    {
        DbSet<PropertyDetails> PropertyDetails { get; set; }

        DbSet<PropertyImage> PropertyImages { get; set; }
        
        DbSet<ApplicationUser> Users { get; set; }

        DbSet<UserPermission> UserPermissions { get; set; }

        DbSet<T> Set<T>() where T : class;
    }
}