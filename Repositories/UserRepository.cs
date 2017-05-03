namespace Landlords.Repositories
{
    using System;
    using System.Linq;
    using Database;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Model.Database;
    using ViewModels;

    public class UserRepository : IUserRepository
    {
        private readonly ILLDbContext _context;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;

        public UserRepository(ILLDbContext context, ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task UpdateAsync(Guid userId, ProfileViewModel viewModel)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(c => c.Id == userId);
            if (user != null)
            {
                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;
                user.AvailableFrom = viewModel.AvailableFrom;
                user.AvailableTo = viewModel.AvailableTo;
                user.PhoneNumber = viewModel.PhoneNumber;
                user.SecondaryPhoneNumber = viewModel.SecondaryPhoneNumber;

                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<ProfileViewModel> GetProfileAsync(Guid userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(c => c.Id == userId);
            return new ProfileViewModel(user);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(Guid userId, string code)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(c => c.Id == userId && c.EmailConfirmed == false);
            if (user != null)
            {
                return await _userManager.ConfirmEmailAsync(user, code);
            }
            return IdentityResult.Failed(new IdentityError()
            {
                Code = "User not found", 
                Description = "User not found"
            });
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
    }
}