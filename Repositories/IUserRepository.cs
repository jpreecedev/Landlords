namespace Landlords.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Landlords.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Model.Database;

    public interface IUserRepository
    {
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);
        Task UpdateAsync(Guid userId, ProfileViewModel viewModel);
        Task<ProfileViewModel> GetProfileAsync(Guid userId);
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task<IdentityResult> ConfirmEmailAsync(Guid userId, string code);
    }
}