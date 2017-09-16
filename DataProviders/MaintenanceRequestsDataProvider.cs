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
    using Model.Database;
    using Model.DataTypes;
    using Model.Entities;
    using ViewModels;

    public class MaintenanceRequestsDataProvider : BaseDataProvider, IMaintenanceRequestsDataProvider
    {
        public MaintenanceRequestsDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task<MaintenanceRequestViewModel> CreateForPortfolioAsync(Guid userId, Guid portfolioId, MaintenanceRequestViewModel maintenanceRequest)
        {
            var entity = new MaintenanceRequest
            {
                Created = DateTime.Now,
                Description = maintenanceRequest.Description,
                Severity = maintenanceRequest.Severity,
                Title = maintenanceRequest.Title,
                UserId = userId,
                PortfolioId = portfolioId
            };

            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();

            return new MaintenanceRequestViewModel(entity, null);
        }

        public async Task<MaintenanceRequestViewModel> CreateForTenantAsync(ApplicationUser user, MaintenanceRequestViewModel maintenanceRequest)
        {
            var entity = new MaintenanceRequest
            {
                Created = DateTime.Now,
                Description = maintenanceRequest.Description,
                Severity = maintenanceRequest.Severity,
                Title = maintenanceRequest.Title,
                UserId = user.Id
            };

            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();

            var maintenanceEntryEntity = new MaintenanceEntry
            {
                Created = DateTime.Now,
                MaintenanceRequestId = entity.Id,
                Status = MaintenanceStatus.Open,
                UserId = user.Id
            };

            await Context.AddAsync(maintenanceEntryEntity);
            await Context.SaveChangesAsync();

            return new MaintenanceRequestViewModel(entity, null)
            {
                Entries = new List<MaintenanceEntryViewModel>
                {
                    new MaintenanceEntryViewModel(maintenanceEntryEntity)
                    {
                        User = user
                    }
                }
            };
        }

        public async Task<MaintenanceEntryViewModel> UpdateMaintenanceEntryAsync(Guid userId, Guid? portfolioId, MaintenanceEntryViewModel viewModel)
        {
            var entity = await (from maintenanceEntry in Context.MaintenanceEntries
                    join maintenanceRequest in Context.MaintenanceRequests on maintenanceEntry.MaintenanceRequestId equals maintenanceRequest.Id
                    where (maintenanceRequest.UserId == userId || (portfolioId != null ? maintenanceRequest.PortfolioId == portfolioId.GetValueOrDefault() : false)) && maintenanceRequest.Id == viewModel.MaintenanceRequestId
                    select maintenanceEntry)
                .SingleAsync();

            entity.MapFrom(viewModel);
            await Context.SaveChangesAsync();

            return new MaintenanceEntryViewModel(entity);
        }

        public async Task<MaintenanceRequestViewModel> UpdateMaintenanceRequestAsync(Guid userId, Guid? portfolioId, MaintenanceRequestViewModel maintenanceRequest)
        {
            var entity = await Context.MaintenanceRequests.Include(x => x.Entries)
                .SingleOrDefaultAsync(c => (c.UserId == userId || (portfolioId != null ? c.PortfolioId == portfolioId.GetValueOrDefault() : false)) && c.Id == maintenanceRequest.Id);

            if (entity != null)
            {
                entity.MapFrom(maintenanceRequest);
                await Context.SaveChangesAsync();

                return new MaintenanceRequestViewModel(entity, entity.Entries);
            }

            return maintenanceRequest;
        }

        public async Task<MaintenanceEntryViewModel> AddMaintenanceEntryAsync(ApplicationUser user, Guid? portfolioId, MaintenanceEntryViewModel maintenanceEntry)
        {
            var maintenanceRequest = await Context.MaintenanceRequests.SingleOrDefaultAsync(c => (c.UserId == user.Id || (portfolioId != null ? c.PortfolioId == portfolioId.GetValueOrDefault() : false)) && c.Id == maintenanceEntry.MaintenanceRequestId);
            if (maintenanceRequest != null)
            {
                var entity = new MaintenanceEntry
                {
                    Created = DateTime.Now,
                    Description = maintenanceEntry.Description,
                    MaintenanceRequestId = maintenanceRequest.Id,
                    Status = maintenanceEntry.Status,
                    UserId = user.Id
                };

                await Context.AddAsync(entity);
                await Context.SaveChangesAsync();

                return new MaintenanceEntryViewModel(entity)
                {
                    User = user
                };
            }

            return null;
        }

        public async Task<ICollection<MaintenanceRequestViewModel>> GetMaintenanceRequestsForTenant(Guid userId)
        {
            return await (from maintenanceRequest in Context.MaintenanceRequests.AsNoTracking()
                    join maintenanceEntry in Context.MaintenanceEntries on maintenanceRequest.Id equals maintenanceEntry.MaintenanceRequestId into maintenanceEntriesJoin
                    where maintenanceRequest.UserId == userId && !maintenanceRequest.IsDeleted && !maintenanceRequest.IsArchived
                    select new MaintenanceRequestViewModel
                    {
                        Id = maintenanceRequest.Id,
                        Title = maintenanceRequest.Title,
                        Description = maintenanceRequest.Description,
                        Severity = maintenanceRequest.Severity,
                        IsArchived = maintenanceRequest.IsArchived,
                        Entries = maintenanceEntriesJoin.Where(c => !c.IsDeleted)
                            .Select(c => new MaintenanceEntryViewModel
                            {
                                Id = c.Id,
                                MaintenanceRequestId = c.MaintenanceRequestId,
                                User = c.User,
                                UserId = c.UserId,
                                Description = c.Description,
                                Created = c.Created,
                                Status = c.Status
                            })
                            .OrderByDescending(c => c.Created)
                            .ToList()
                    })
                .ToListAsync();
        }

        public async Task<ICollection<MaintenanceRequestViewModel>> GetMaintenanceRequests(Guid portfolioId)
        {
            return await (from maintenanceRequest in Context.MaintenanceRequests.AsNoTracking()
                    join maintenanceEntry in Context.MaintenanceEntries on maintenanceRequest.Id equals maintenanceEntry.MaintenanceRequestId into maintenanceEntriesJoin
                    where maintenanceRequest.PortfolioId == portfolioId && !maintenanceRequest.IsDeleted && !maintenanceRequest.IsArchived
                    select new MaintenanceRequestViewModel
                    {
                        Id = maintenanceRequest.Id,
                        Title = maintenanceRequest.Title,
                        Description = maintenanceRequest.Description,
                        Severity = maintenanceRequest.Severity,
                        IsArchived = maintenanceRequest.IsArchived,
                        Entries = maintenanceEntriesJoin.Where(c => !c.IsDeleted)
                            .Select(c => new MaintenanceEntryViewModel
                            {
                                Id = c.Id,
                                Description = c.Description,
                                Status = c.Status,
                                UserId = c.UserId,
                                User = c.User
                            })
                            .OrderByDescending(c => c.Created)
                            .ToList()
                    })
                .ToListAsync();
        }

        public async Task ArchiveMaintenanceRequest(Guid userId, Guid? portfolioId, Guid maintenanceRequestId)
        {
            var maintenanceRequest = await Context.MaintenanceRequests.SingleOrDefaultAsync(c => (c.UserId == userId || (portfolioId != null ? c.PortfolioId == portfolioId.GetValueOrDefault() : false)) && c.Id == maintenanceRequestId);
            if (maintenanceRequest != null)
            {
                maintenanceRequest.IsArchived = true;
                await Context.SaveChangesAsync();
            }
        }
    }
}