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
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Model.Entities;

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private readonly LLDbContext _context;

        public ApplicationUserManager(LLDbContext context, IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserPermission>> GetUserPermissionsAsync(Guid userId)
        {
            return await _context.UserPermissions.Where(up => up.UserId == userId).ToListAsync();
        }

        public async Task SetUserPermissionsAsync(Guid userId, params Guid[] permissions)
        {
            var permissionEntities = _context.Permissions.Where(c => permissions.Any(d => d == c.Id));

            var userPermissions = permissionEntities.Select(c => new UserPermission
            {
                Created = DateTime.Now,
                Permission = c,
                UserId = userId
            });

            await _context.UserPermissions.AddRangeAsync(userPermissions);
            await _context.SaveChangesAsync();
        }
    }
}