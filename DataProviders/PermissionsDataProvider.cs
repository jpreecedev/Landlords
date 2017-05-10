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
    using ViewModels;

    public class PermissionsDataProvider : BaseDataProvider, IPermissionsDataProvider
    {
        public PermissionsDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task<ICollection<UserPermissionViewModel>> GetPermissionsAsync(Guid userId)
        {
            return await Context.UserPermissions.Where(c => c.UserId == userId)
                .Select(c => new UserPermissionViewModel(c.Permission.Description, c.Permission.RouteId))
                .ToListAsync();
        }
    }
}