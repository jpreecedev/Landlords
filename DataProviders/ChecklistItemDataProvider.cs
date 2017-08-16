namespace Landlords.DataProviders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Database;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using ViewModels;

    public class ChecklistItemDataProvider : BaseDataProvider, IChecklistItemDataProvider
    {
        public ChecklistItemDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task ToggleChecklistItemCompletedAsync(Guid portfolioId, Guid checklistId, Guid checklistItemId, bool completed)
        {
            var checklist = await Context.ChecklistInstances.Join(Context.ChecklistItemInstances, checklistInstance => checklistInstance.Id, itemInstance => itemInstance.ChecklistInstanceId, (checklistInstance, itemInstance) => new {Checklist = checklistInstance, Item = itemInstance})
                .FirstOrDefaultAsync(c => c.Checklist.PortfolioId == portfolioId && c.Checklist.Id == checklistId && c.Item.Id == checklistItemId);

            if (checklist != null && checklist.Item != null)
            {
                checklist.Item.IsCompleted = completed;
                await Context.SaveChangesAsync();
            }
        }

        public async Task MoveAsync(Guid portfolioId, Guid checklistId, Guid checklistItemId, string direction)
        {
            var checklistItems = await(from checklist in Context.ChecklistInstances
                join checklistItem in Context.ChecklistItemInstances on checklist.Id equals checklistItem.ChecklistInstanceId into itemJoin
                from item in itemJoin.DefaultIfEmpty().OrderBy(c => c.Order)
                where checklist.PortfolioId == portfolioId && checklist.Id == checklistId
                select item).ToListAsync();

            var index = checklistItems.FindIndex(c => c.Id == checklistItemId);
            var poppedItem = checklistItems[index];
            checklistItems.RemoveAt(index);

            switch (direction)
            {
                case "up":
                    checklistItems.Insert(index - 1, poppedItem);
                    break;

                case "down":
                    checklistItems.Insert(index + 1, poppedItem);
                    break;
            }

            for (int i = 0; i < checklistItems.Count; i++)
            {
                checklistItems[i].Order = i + 1;
            }

            await Context.SaveChangesAsync();
        }

        public async Task ApplyTemplatePayloadAsync(Guid portfolioId, Guid checklistId, Guid checklistItemId, string payload)
        {
            var checklist = await Context.ChecklistInstances.Join(Context.ChecklistItemInstances, checklistInstance => checklistInstance.Id, itemInstance => itemInstance.ChecklistInstanceId, (checklistInstance, itemInstance) => new { Checklist = checklistInstance, Item = itemInstance })
                .FirstOrDefaultAsync(c => c.Checklist.PortfolioId == portfolioId && c.Checklist.Id == checklistId && c.Item.Id == checklistItemId);

            if (checklist != null && checklist.Item != null)
            {
                checklist.Item.Payload = payload;
                await Context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<ResourceViewModel>> UploadAsync(ICollection<IFormFile> files, Guid checklistId, Guid checklistItemId)
        {
            return await files.ProcessResourcesAsync(HostingEnvironment.WebRootPath, checklistId.ToString() + checklistItemId.ToString());
        }

        public async Task DeleteAsync(Guid portfolioId, Guid checklistId, Guid checklistItemId)
        {
            var entity = await (from checklistItem in Context.ChecklistItemInstances
                                join checklist in Context.ChecklistInstances on checklistItem.ChecklistInstanceId equals checklist.Id
                                where checklist.PortfolioId == portfolioId && checklist.Id == checklistId && checklistItem.Id == checklistItemId
                                select checklistItem)
                                .SingleAsync();

            entity.Deleted = DateTime.Now;

            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid portfolioId, Guid checklistId, Guid checklistItemId, ChecklistItemViewModel viewModel)
        {
            var entity = await (from checklistItem in Context.ChecklistItemInstances
                    join checklist in Context.ChecklistInstances on checklistItem.ChecklistInstanceId equals checklist.Id
                    where checklist.PortfolioId == portfolioId && checklist.Id == checklistId && checklistItem.Id == checklistItemId
                    select checklistItem)
                .SingleAsync();

            entity.DisplayText = viewModel.DisplayText;
            entity.Template = viewModel.Template;

            await Context.SaveChangesAsync();
        }
    }
}