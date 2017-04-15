namespace Landlords.Repositories
{
    using System.Linq;
    using Database;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;

    public interface IUserRepository
    {
        Task<IdentityResult> Create(ApplicationUser user, string password);
        Task<IdentityResult> AddToRole(ApplicationUser user, string role);
    }

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

        public async Task<IdentityResult> Create(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
    }
}