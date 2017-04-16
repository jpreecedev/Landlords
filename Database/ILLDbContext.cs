namespace Landlords.Database
{
    using Microsoft.EntityFrameworkCore;
    using Model;

    public interface ILLDbContext
    {
        DbSet<PropertyDetails> PropertyDetails { get; set; }

        DbSet<ApplicationUser> Users { get; set; }
    }
}