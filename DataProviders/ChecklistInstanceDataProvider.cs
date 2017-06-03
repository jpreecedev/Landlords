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

        public async Task<ChecklistViewModel> GetChecklistByIdAsync(Guid portfolioId, Guid checklistId)
        {
            return await (from checklistInstance in Context.ChecklistInstances
                join checklistItems in Context.ChecklistItemInstances on checklistInstance.Id equals checklistItems.ChecklistInstanceId into checklistItemsJoin
                join propertyDetails in Context.PropertyDetails on checklistInstance.PropertyDetailsId.GetValueOrDefault() equals propertyDetails.Id into propertyDetailsJoin
                where checklistInstance.Id == checklistId && checklistInstance.PortfolioId == portfolioId && !checklistInstance.IsDeleted
                select new ChecklistViewModel
                {
                    ChecklistItems = checklistItemsJoin.OrderBy(c => c.Order).Where(c => !c.IsDeleted).Select(c => new ChecklistItemViewModel(c)).ToList(),
                    Id = checklistInstance.Id,
                    PortfolioId = checklistInstance.PortfolioId,
                    Description = checklistInstance.Description,
                    Name = checklistInstance.Name,
                    IsAvailableDownstream = checklistInstance.IsAvailableDownstream,
                    IsArchived = checklistInstance.IsArchived,
                    Image = checklistInstance.Image,
                    IsPropertyMandatory = checklistInstance.IsPropertyMandatory,
                    PropertyDetailsId = checklistInstance.PropertyDetailsId,
                    Origin = ChecklistOrigin.User.ToString(),
                    PropertyReference = propertyDetailsJoin.Any() ? propertyDetailsJoin.First().Reference : null,
                    PropertyStreetAddress = propertyDetailsJoin.Any() ? propertyDetailsJoin.First().PropertyStreetAddress : null
                }).FirstOrDefaultAsync();
        }

        public async Task<ChecklistViewModel> CreateChecklistInstanceAsync(Guid portfolioId, Guid checklistId, Guid? propertyDetailsId)
        {
            var checklist = await Context.Checklists.FirstOrDefaultAsync(c => c.Id == checklistId);
            if (checklist != null)
            {
                var instance = new ChecklistInstance(checklist)
                {
                    Created = DateTime.Now,
                    PortfolioId = portfolioId,
                    PropertyDetailsId = propertyDetailsId
                };

                await Context.ChecklistInstances.AddAsync(instance);
                await Context.SaveChangesAsync();

                var checklistItems = await Context.ChecklistItems.OrderBy(c => c.Order).AsNoTracking().Where(c => c.ChecklistId == checklist.Id).ToListAsync();
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

        public async Task DeleteInstanceAsync(Guid portfolioId, Guid checklistId)
        {
            var checklist = await Context.ChecklistInstances.FirstOrDefaultAsync(c => c.PortfolioId == portfolioId && c.Id == checklistId);
            if (checklist != null)
            {
                checklist.Deleted = DateTime.Now;
                await Context.SaveChangesAsync();
            }
        }

        public async Task ArchiveChecklistInstanceAsync(Guid portfolioId, Guid checklistId)
        {
            var checklist = await Context.ChecklistInstances.FirstOrDefaultAsync(c => c.PortfolioId == portfolioId && c.Id == checklistId);
            if (checklist != null)
            {
                checklist.IsArchived = true;
                await Context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<ChecklistViewModel>> GetArchivedChecklistInstancesAsync(Guid portfolioId)
        {
            return await (from checklist in Context.ChecklistInstances.AsNoTracking()
                          join checklistItems in Context.ChecklistItemInstances on checklist.Id equals checklistItems.ChecklistInstanceId into checklistItemsJoin
                          join propertyDetails in Context.PropertyDetails on checklist.PropertyDetailsId.GetValueOrDefault() equals propertyDetails.Id into propertyDetailsJoin
                          where checklist.PortfolioId == portfolioId && !checklist.IsDeleted && checklist.IsArchived
                          select new ChecklistViewModel(checklist, checklistItemsJoin.OrderBy(c => c.Order).ToList(), ChecklistOrigin.User.ToString())
                          {
                              PropertyReference = propertyDetailsJoin.Any() ? propertyDetailsJoin.First().Reference : null,
                              PropertyStreetAddress = propertyDetailsJoin.Any() ? propertyDetailsJoin.First().PropertyStreetAddress : null
                          }
                )
                .ToListAsync();
        }
    }
}