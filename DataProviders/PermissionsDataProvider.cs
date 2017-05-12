namespace Landlords.DataProviders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Database;
    using Interfaces;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Model;
    using Model.Database;
    using ViewModels;

    public class PermissionsDataProvider : BaseDataProvider, IPermissionsDataProvider
    {
        public PermissionsDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task<ICollection<PermissionViewModel>> GetPermissionsAsync()
        {
            return await Context.Permissions.Select(c => new PermissionViewModel
                {
                    PermissionId = c.Id,
                    Description = c.Description,
                    RouteId = c.RouteId
                })
                .ToListAsync();
        }

        public async Task<ICollection<UserPermissionViewModel>> GetUserPermissionsAsync(Guid userId)
        {
            return await Context.UserPermissions.Where(c => c.UserId == userId)
                .Select(c => new UserPermissionViewModel(c.Permission.Description, c.Permission.RouteId) { Id = c.PermissionId })
                .ToListAsync();
        }

        public async Task<ICollection<UserViewModel>> GetUsersAsync()
        {
            return await Context.Users.Include(x => x.Roles)
                .Join(Context.UserRoles, user => user.Id, role => role.UserId, (user, role) => new { User = user, Role = role })
                .Join(Context.Roles, arg => arg.Role.RoleId, role => role.Id, (arg, role) => new { User = arg.User, Role = role })
                .Where(c => c.Role.Name != ApplicationRoles.SiteAdministrator)
                .Select(c => new UserViewModel
                {
                    Id = c.User.Id,
                    Name = c.User.FirstName + " " + c.User.LastName,
                    EmailAddress = c.User.Email
                })
                .ToListAsync();
        }

        public async Task RemovePermissionAsync(Guid userId, Guid permissionId)
        {
            var userPermission = await Context.UserPermissions.SingleOrDefaultAsync(c => c.UserId == userId && c.PermissionId == permissionId);
            if (userPermission != null)
            {
                Context.UserPermissions.Remove(userPermission);
                await Context.SaveChangesAsync();
            }
        }

        public async Task AddPermissionAsync(Guid userId, Guid permissionId)
        {
            var existingPermission = await Context.UserPermissions.SingleOrDefaultAsync(c => c.UserId == userId && c.PermissionId == permissionId);
            if (existingPermission == null)
            {
                await Context.UserPermissions.AddAsync(new UserPermission
                {
                    Created = DateTime.Now,
                    UserId = userId,
                    PermissionId = permissionId
                });
                await Context.SaveChangesAsync();
            }
        }
    }
}