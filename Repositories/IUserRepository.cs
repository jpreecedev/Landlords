namespace Landlords.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Landlords.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Model.Database;

    public interface IUserRepository
    {
        Task<IdentityResult> Create(ApplicationUser user, string password);
        Task<IdentityResult> AddToRole(ApplicationUser user, string role);
        Task UpdateAsync(Guid userId, ProfileViewModel viewModel);
        Task<ProfileViewModel> GetProfileAsync(Guid userId);
    }
}