namespace Landlords.Database
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Model.Database;
    using System.Threading.Tasks;
    using Model;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private readonly LLDbContext _context;

        public ApplicationUserManager(LLDbContext context, IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> GetUserPermissionsAsync(Guid userId)
        {
            return await _context.UserPermissions.Where(up => up.UserId == userId).Select(up => up.DisplayText).ToListAsync();
        }

        public async Task SetUserPermissionsAsync(Guid userId, params string[] permission)
        {
            var permissions = permission.Select(c => new UserPermission
            {
                Created = DateTime.Now,
                Description = c,
                UserId = userId
            });

            await _context.UserPermissions.AddRangeAsync(permissions);
            await _context.SaveChangesAsync();
        }
    }
}