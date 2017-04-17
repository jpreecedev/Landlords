namespace Landlords.Database
{
    using Microsoft.EntityFrameworkCore;
    using Model;
    using Model.Database;

    public interface ILLDbContext
    {
        DbSet<PropertyDetails> PropertyDetails { get; set; }

        DbSet<ApplicationUser> Users { get; set; }
    }
}