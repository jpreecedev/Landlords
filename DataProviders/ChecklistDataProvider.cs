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

        public async Task<ChecklistOverviewViewModel> GetChecklistOverviewAsync(Guid userId, Guid agencyId)
        {
            var userChecklists = await (from checklist in Context.Checklists.AsNoTracking()
                    join checklistItems in Context.ChecklistItems on checklist.Id equals checklistItems.ChecklistId into checklistItemsJoin
                    from checklistItem in checklistItemsJoin.DefaultIfEmpty()
                    where checklist.UserId == userId && !checklist.IsDeleted
                    select new ChecklistViewModel(checklist, checklistItemsJoin.ToList())
                )
                .Distinct()
                .ToListAsync();

            var agencyChecklists = new List<ChecklistViewModel>();
            if (agencyId != default(Guid))
            {
                agencyChecklists = await (from checklist in Context.Checklists.AsNoTracking()
                        join checklistItems in Context.ChecklistItems on checklist.Id equals checklistItems.ChecklistId into checklistItemsJoin
                        from checklistItem in checklistItemsJoin.DefaultIfEmpty()
                        where checklist.UserId == agencyId && checklist.IsAvailableDownstream && !checklist.IsDeleted
                        select new ChecklistViewModel(checklist, checklistItemsJoin.ToList())
                    )
                    .Distinct()
                    .ToListAsync();
            }

            var adminChecklists = await (from checklist in Context.Checklists.AsNoTracking()
                    join checklistItems in Context.ChecklistItems on checklist.Id equals checklistItems.ChecklistId into checklistItemsJoin
                    join userRoles in Context.UserRoles on checklist.UserId equals userRoles.UserId
                    join role in Context.Roles on userRoles.RoleId equals role.Id
                    where role.Name == ApplicationRoles.SiteAdministrator
                    select new ChecklistViewModel(checklist, checklistItemsJoin.ToList())
                )
                .ToListAsync();

            return new ChecklistOverviewViewModel
            {
                AdminChecklists = adminChecklists,
                AgencyChecklists = agencyChecklists,
                UserChecklists = userChecklists
            };
        }
    }
}