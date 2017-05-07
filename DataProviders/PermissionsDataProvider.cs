namespace Landlords.DataProviders
{
    using System;
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

        public async Task<UserMenuStructureViewModel> GetMenuStructureAsync(Guid userId)
        {
            // TODO

            var permissions = Context.UserPermissions.Where(c => c.UserId == userId);
            return await permissions.Select(c => new UserMenuStructureViewModel()).FirstAsync();
        }
    }
}