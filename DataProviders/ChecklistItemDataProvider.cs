namespace Landlords.DataProviders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Database;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;

    public class ChecklistItemDataProvider : BaseDataProvider, IChecklistItemDataProvider
    {
        public ChecklistItemDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task ToggleChecklistItemCompletedAsync(Guid userId, Guid checklistId, Guid checklistItemId, bool completed)
        {
            var checklist = await Context.ChecklistInstances.Join(Context.ChecklistItemInstances, checklistInstance => checklistInstance.Id, itemInstance => itemInstance.ChecklistInstanceId, (checklistInstance, itemInstance) => new {Checklist = checklistInstance, Item = itemInstance})
                .FirstOrDefaultAsync(c => c.Checklist.UserId == userId && c.Checklist.Id == checklistId && c.Item.Id == checklistItemId);

            if (checklist != null)
            {
                checklist.Item.IsCompleted = completed;
                await Context.SaveChangesAsync();
            }
        }

        public async Task ToggleChecklistItemExpandedAsync(Guid userId, Guid checklistId, Guid checklistItemId, bool expanded)
        {
            var checklistItems = await (from checklist in Context.ChecklistInstances
                join checklistItem in Context.ChecklistItemInstances on checklist.Id equals checklistItem.ChecklistInstanceId into itemJoin
                from item in itemJoin.DefaultIfEmpty()
                where checklist.UserId == userId && checklist.Id == checklistId
                select item).ToListAsync();

            checklistItems.ForEach(item => item.IsExpanded = false);

            var selectedItem = checklistItems.FirstOrDefault(c => c.Id == checklistItemId);
            if (selectedItem != null)
            {
                selectedItem.IsExpanded = expanded;
                await Context.SaveChangesAsync();
            }
        }
    }
}