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
    using Model;
    using Model.Database;

    public class ChecklistDataProvider : BaseDataProvider, IChecklistDataProvider
    {
        public ChecklistDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task<ChecklistOverviewViewModel> GetChecklistOverviewAsync(Guid userId, Guid agencyId)
        {
            var checklistInstances = await (from checklist in Context.ChecklistInstances.AsNoTracking()
                    join checklistItems in Context.ChecklistItemInstances on checklist.Id equals checklistItems.ChecklistInstanceId into checklistItemsJoin
                    where checklist.UserId == userId && !checklist.IsDeleted
                    select new ChecklistViewModel(checklist, checklistItemsJoin.ToList(), "User")
                )
                .ToListAsync();

            var userChecklists = new List<ChecklistViewModel>();
            if (userId != default(Guid))
            {
                userChecklists = await (from checklist in Context.Checklists.AsNoTracking()
                        join checklistItems in Context.ChecklistItems on checklist.Id equals checklistItems.ChecklistId into checklistItemsJoin
                        where checklist.UserId == userId && !checklist.IsDeleted
                        select new ChecklistViewModel(checklist, checklistItemsJoin.ToList(), "User")
                    )
                    .ToListAsync();
            }

            var agencyChecklists = new List<ChecklistViewModel>();
            if (agencyId != default(Guid))
            {
                agencyChecklists = await (from checklist in Context.Checklists.AsNoTracking()
                        join checklistItems in Context.ChecklistItems on checklist.Id equals checklistItems.ChecklistId into checklistItemsJoin
                        where checklist.UserId == agencyId && checklist.IsAvailableDownstream && !checklist.IsDeleted
                        select new ChecklistViewModel(checklist, checklistItemsJoin.ToList(), "Agency")
                    )
                    .ToListAsync();
            }

            var adminChecklists = await (from checklist in Context.Checklists.AsNoTracking()
                    join checklistItems in Context.ChecklistItems on checklist.Id equals checklistItems.ChecklistId into checklistItemsJoin
                    join userRoles in Context.UserRoles on checklist.UserId equals userRoles.UserId
                    join role in Context.Roles on userRoles.RoleId equals role.Id
                    where role.Name == ApplicationRoles.SiteAdministrator
                    select new ChecklistViewModel(checklist, checklistItemsJoin.ToList(), "Admin")
                )
                .ToListAsync();

            return new ChecklistOverviewViewModel
            {
                AvailableChecklists = userChecklists.Concat(agencyChecklists.Concat(adminChecklists)).ToList(),
                Checklists = checklistInstances
            };
        }

        public async Task<ChecklistViewModel> CreateChecklistInstanceAsync(Guid userId, Guid checklistId)
        {
            var checklist = await Context.Checklists.FirstOrDefaultAsync(c => c.Id == checklistId);
            if (checklist != null)
            {
                var instance = new ChecklistInstance(checklist)
                {
                    UserId = userId,
                    Created = DateTime.Now
                };

                await Context.ChecklistInstances.AddAsync(instance);
                await Context.SaveChangesAsync();
                
                var checklistItems = await Context.ChecklistItems.AsNoTracking().Where(c => c.ChecklistId == checklist.Id).ToListAsync();
                var checklistItemInstances = new List<ChecklistItemInstance>();
                foreach (var checklistItem in checklistItems)
                {
                    var newItem = new ChecklistItemInstance(checklistItem)
                    {
                        Created = DateTime.Now,
                        Id = default(Guid),
                        ChecklistInstanceId = instance.Id
                    };
                    checklistItemInstances.Add(newItem);
                }

                await Context.ChecklistItemInstances.AddRangeAsync(checklistItemInstances);
                await Context.SaveChangesAsync();

                return new ChecklistViewModel(instance, checklistItemInstances, "User");
            }

            return null;
        }

        public async Task<ChecklistViewModel> GetChecklistByIdAsync(Guid userId, Guid checklistId)
        {
            var checklist = await Context.ChecklistInstances.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == checklistId);
            var checklistItems = await Context.ChecklistItemInstances.Where(c => c.ChecklistInstanceId == checklistId).ToListAsync();

            return new ChecklistViewModel(checklist, checklistItems, "User");
        }
    }
}