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
    using System.Collections.Generic;
    using Model.Database;

    public class ChecklistDataProvider : BaseDataProvider, IChecklistDataProvider
    {
        public ChecklistDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task<ChecklistOverviewViewModel> GetChecklistOverview(Guid userId, Guid agencyId)
        {
            var userChecklists = await GetChecklistsByUser(userId);
            var agencyChecklists = new List<ChecklistInstanceViewModel>();

            if (agencyId != default(Guid))
            {
                agencyChecklists = await GetChecklistsByUser(agencyId);
                agencyChecklists = agencyChecklists.Where(c => c.IsAvailableDownstream).ToList();
            }

            var adminChecklists = await Context.ChecklistInstances.Select(c => new {Instance = c})
                .Join(Context.UserRoles, arg => arg.Instance.UserId, role => role.UserId, (arg, role) => new {Instance = arg.Instance, Role = role})
                .Join(Context.Roles, arg => arg.Role.RoleId, role => role.Id, (arg, role) => new {Instance = arg.Instance, Role = role})
                .Where(c => c.Role.Name == ApplicationRoles.SiteAdministrator && c.Instance.IsAvailableDownstream && !c.Instance.IsDeleted)
                .Select(c => new ChecklistInstanceViewModel(c.Instance))
                .ToListAsync();

            return new ChecklistOverviewViewModel
            {
                AdminChecklists = adminChecklists,
                AgencyChecklists = agencyChecklists,
                UserChecklists = userChecklists
            };
        }

        private async Task<List<ChecklistInstanceViewModel>> GetChecklistsByUser(Guid userId)
        {
            return await Context.ChecklistInstances.Include(x => x.ChecklistItems)
                .Join(Context.Users, instance => instance.UserId, user => user.Id, (instance, user) => new {Instance = instance, User = user})
                .Where(c => c.User.Id == userId && !c.Instance.IsArchived && !c.Instance.IsDeleted)
                .Select(c => new ChecklistInstanceViewModel(c.Instance))
                .ToListAsync();
        }
    }
}