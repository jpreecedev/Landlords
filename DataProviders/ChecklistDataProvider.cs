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

        public async Task<ChecklistOverviewViewModel> GetChecklistOverviewAsync(Guid userId, Guid agencyId, bool includeArchived)
        {
            var checklistInstances = await (from checklist in Context.ChecklistInstances.AsNoTracking()
                    join checklistItems in Context.ChecklistItemInstances on checklist.Id equals checklistItems.ChecklistInstanceId into checklistItemsJoin
                    join propertyDetails in Context.PropertyDetails on checklist.PropertyDetailsId.GetValueOrDefault() equals propertyDetails.Id into propertyDetailsJoin
                    where checklist.UserId == userId && !checklist.IsDeleted && (includeArchived ? true : !checklist.IsArchived)
                    select new ChecklistViewModel(checklist, checklistItemsJoin.OrderBy(c => c.Order).ToList(), ChecklistOrigin.User.ToString())
                    {
                        PropertyReference = propertyDetailsJoin.Any() ? propertyDetailsJoin.First().Reference : null,
                        PropertyStreetAddress = propertyDetailsJoin.Any() ? propertyDetailsJoin.First().PropertyStreetAddress : null
                    }
                )
                .ToListAsync();

            var userChecklists = new List<ChecklistViewModel>();
            if (userId != default(Guid))
            {
                userChecklists = await (from checklist in Context.Checklists.AsNoTracking()
                        join checklistItems in Context.ChecklistItems on checklist.Id equals checklistItems.ChecklistId into checklistItemsJoin
                        where checklist.UserId == userId && !checklist.IsDeleted
                        select new ChecklistViewModel(checklist, checklistItemsJoin.OrderBy(c => c.Order).ToList(), ChecklistOrigin.User.ToString())
                    )
                    .ToListAsync();
            }

            var agencyChecklists = new List<ChecklistViewModel>();
            if (agencyId != default(Guid))
            {
                agencyChecklists = await (from checklist in Context.Checklists.AsNoTracking()
                        join checklistItems in Context.ChecklistItems on checklist.Id equals checklistItems.ChecklistId into checklistItemsJoin
                        where checklist.UserId == agencyId && checklist.IsAvailableDownstream && !checklist.IsDeleted
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

        public async Task<ChecklistViewModel> CreateChecklistTemplateAsync(Guid userId, ChecklistViewModel checklist, string userOrigin)
        {
            // TODO, review all logic here

            var entity = await Context.Checklists.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == checklist.Id);
            if (entity == null)
            {
                return await CreateNewChecklistTemplateAsync(userId, checklist, userOrigin);
            }
            return await UpdateChecklistTemplateAsync(checklist, entity, userOrigin);
        }

        private async Task<ChecklistViewModel> CreateNewChecklistTemplateAsync(Guid userId, ChecklistViewModel checklist, string userOrigin)
        {
            var checklistEntity = new Checklist
            {
                Created = DateTime.Now,
                Name = checklist.Name,
                Description = checklist.Description,
                IsPropertyMandatory = checklist.IsPropertyMandatory,
                IsAvailableDownstream = checklist.IsAvailableDownstream,
                Image = checklist.Image,
                UserId = userId
            };

            await Context.Checklists.AddAsync(checklistEntity);
            await Context.SaveChangesAsync();

            var checklistItems = new List<ChecklistItem>();

            if (checklist.ChecklistItems != null && checklist.ChecklistItems.Count > 0)
            {
                foreach (var checklistItem in checklist.ChecklistItems)
                {
                    var checklistItemEntity = new ChecklistItem
                    {
                        ChecklistId = checklistEntity.Id,
                        DisplayText = checklistItem.DisplayText,
                        Key = checklistItem.Key,
                        IsExpanded = checklistItem.IsExpanded,
                        Template = checklistItem.Template
                    };

                    checklistItems.Add(checklistItemEntity);
                }

                await Context.ChecklistItems.AddRangeAsync(checklistItems);
                await Context.SaveChangesAsync();
            }

            return new ChecklistViewModel(checklistEntity, checklistItems, userOrigin);
        }
        
        private async Task<ChecklistViewModel> UpdateChecklistTemplateAsync(ChecklistViewModel checklist, Checklist entity, string userOrigin)
        {
            entity.IsPropertyMandatory = checklist.IsPropertyMandatory;
            entity.IsAvailableDownstream = checklist.IsAvailableDownstream;
            entity.Image = checklist.Image;
            entity.Name = checklist.Name;
            entity.Description = checklist.Description;
            
            var existingChecklistItems = await Context.ChecklistItems.Where(c => c.ChecklistId == entity.Id).ToListAsync();
            var checklistItemEntities = new List<ChecklistItem>();

            foreach (ChecklistItemViewModel checklistItemViewModel in checklist.ChecklistItems)
            {
                if (checklistItemViewModel.Id.IsDefault())
                {
                    var checklistItem = new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = checklist.Id,
                        DisplayText = checklistItemViewModel.DisplayText,
                        Key = checklistItemViewModel.Key,
                        Template = checklistItemViewModel.Template
                    };

                    checklistItemEntities.Add(checklistItem);
                }
                else
                {
                    var match = existingChecklistItems.FirstOrDefault(c => c.Id == checklistItemViewModel.Id);
                    if (match != null)
                    {
                        match.DisplayText = checklistItemViewModel.DisplayText;
                        match.IsExpanded = checklistItemViewModel.IsExpanded;
                        match.Key = checklistItemViewModel.Key;
                        match.Template = checklistItemViewModel.Template;
                    }
                }
            }

            await Context.ChecklistItems.AddRangeAsync(checklistItemEntities);
            await Context.SaveChangesAsync();

            var checklistItems = await Context.ChecklistItems.OrderBy(c => c.Order).Where(c => c.ChecklistId == entity.Id).ToListAsync();
            return new ChecklistViewModel(entity, checklistItems, userOrigin);
        }

    }
}