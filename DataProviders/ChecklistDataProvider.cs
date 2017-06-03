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
    using Core;
    using Model;
    using Model.Database;

    public class ChecklistDataProvider : BaseDataProvider, IChecklistDataProvider
    {
        public ChecklistDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task<ChecklistOverviewViewModel> GetChecklistOverviewAsync(Guid portfolioId, Guid agencyId, bool includeArchived)
        {
            var checklistInstances = await (from checklist in Context.ChecklistInstances.AsNoTracking()
                    join checklistItems in Context.ChecklistItemInstances on checklist.Id equals checklistItems.ChecklistInstanceId into checklistItemsJoin
                    join propertyDetails in Context.PropertyDetails on checklist.PropertyDetailsId.GetValueOrDefault() equals propertyDetails.Id into propertyDetailsJoin
                    where checklist.PortfolioId == portfolioId && !checklist.IsDeleted && (includeArchived ? true : !checklist.IsArchived)
                    select new ChecklistViewModel(checklist, checklistItemsJoin.OrderBy(c => c.Order).ToList(), ChecklistOrigin.User.ToString())
                    {
                        PropertyReference = propertyDetailsJoin.Any() ? propertyDetailsJoin.First().Reference : null,
                        PropertyStreetAddress = propertyDetailsJoin.Any() ? propertyDetailsJoin.First().PropertyStreetAddress : null
                    }
                )
                .ToListAsync();

            var userChecklists = await (from checklist in Context.Checklists.AsNoTracking()
                    join aup in Context.ApplicationUserPortfolios on checklist.UserId equals aup.UserId
                    join checklistItems in Context.ChecklistItems on checklist.Id equals checklistItems.ChecklistId into checklistItemsJoin
                    where aup.PortfolioId == portfolioId && !checklist.IsDeleted
                    select new ChecklistViewModel(checklist, checklistItemsJoin.OrderBy(c => c.Order).ToList(), ChecklistOrigin.User.ToString())
                )
                .ToListAsync();

            var agencyChecklists = new List<ChecklistViewModel>();
            if (agencyId != default(Guid))
            {
                agencyChecklists = await (from checklist in Context.Checklists.AsNoTracking()
                        join aup in Context.ApplicationUserPortfolios on checklist.UserId equals aup.AgencyId
                        join checklistItems in Context.ChecklistItems on checklist.Id equals checklistItems.ChecklistId into checklistItemsJoin
                        where aup.PortfolioId == portfolioId && checklist.IsAvailableDownstream && !checklist.IsDeleted
                        select new ChecklistViewModel(checklist, checklistItemsJoin.OrderBy(c => c.Order).ToList(), ChecklistOrigin.Agency.ToString())
                    )
                    .ToListAsync();
            }

            var adminChecklists = await (from checklist in Context.Checklists.AsNoTracking()
                    join checklistItems in Context.ChecklistItems on checklist.Id equals checklistItems.ChecklistId into checklistItemsJoin
                    join userRoles in Context.UserRoles on checklist.UserId equals userRoles.UserId
                    join role in Context.Roles on userRoles.RoleId equals role.Id
                    where role.Name == ApplicationRoles.SiteAdministrator
                    select new ChecklistViewModel(checklist, checklistItemsJoin.OrderBy(c => c.Order).ToList(), ChecklistOrigin.Admin.ToString())
                )
                .ToListAsync();

            return new ChecklistOverviewViewModel
            {
                AvailableChecklists = userChecklists.Concat(agencyChecklists.Concat(adminChecklists)).ToList(),
                Checklists = checklistInstances
            };
        }
    }
}