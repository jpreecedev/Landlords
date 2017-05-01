namespace Landlords.Repositories
{
    using System;
    using System.Linq;
    using System.Security.Claims;
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
        
        public async Task<IdentityResult> AddToRole(ApplicationUser user, string role)
        {
            if (!_roleManager.Roles.Any())
            {
                //TODO: Remove
                await _roleManager.CreateAsync(new ApplicationRole(ApplicationRoles.Administrator));
                await _roleManager.CreateAsync(new ApplicationRole(ApplicationRoles.Agency));
                await _roleManager.CreateAsync(new ApplicationRole(ApplicationRoles.Landlord));
            }

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
                user.MainPhoneNumber = viewModel.MainPhoneNumber;
                user.SecondaryPhoneNumber = viewModel.SecondaryPhoneNumber;

                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<ProfileViewModel> GetProfileAsync(Guid userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(c => c.Id == userId);
            return new ProfileViewModel(user);
        }

        public async Task<IdentityResult> Create(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
    }
}