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
    using ViewModels;

    public class ChecklistInstanceDataProvider : BaseDataProvider, IChecklistInstanceDataProvider
    {
        public ChecklistInstanceDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
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

                return new ChecklistViewModel(instance, checklistItemInstances, ChecklistOrigin.User.ToString());
            }

            return null;
        }

        public async Task DeleteInstanceAsync(Guid userId, Guid checklistId)
        {
            var checklist = await Context.ChecklistInstances.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == checklistId);
            if (checklist != null)
            {
                checklist.Deleted = DateTime.Now;
                await Context.SaveChangesAsync();
            }
        }

        public async Task ArchiveChecklistInstanceAsync(Guid userId, Guid checklistId)
        {
            var checklist = await Context.ChecklistInstances.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == checklistId);
            if (checklist != null)
            {
                checklist.IsArchived = true;
                await Context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<ChecklistViewModel>> GetArchivedChecklistInstancesAsync(Guid userId)
        {
            return await (from checklist in Context.ChecklistInstances.AsNoTracking()
                    join checklistItems in Context.ChecklistItemInstances on checklist.Id equals checklistItems.ChecklistInstanceId into checklistItemsJoin
                    where checklist.UserId == userId && !checklist.IsDeleted && checklist.IsArchived
                    select new ChecklistViewModel(checklist, checklistItemsJoin.ToList(), ChecklistOrigin.User.ToString())
                )
                .ToListAsync();
        }
    }
}