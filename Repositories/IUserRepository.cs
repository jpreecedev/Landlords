namespace Landlords.Repositories
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Model.Database;

    public interface IUserRepository
    {
        Task<IdentityResult> Create(ApplicationUser user, string password);
        Task<IdentityResult> AddToRole(ApplicationUser user, string role);
    }
}