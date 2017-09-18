namespace Landlords.DataProviders
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database;
    using Interfaces;
    using Microsoft.AspNetCore.Hosting;
    using ViewModels;
    using System.Security.Claims;
    using Core;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using Model.Entities;
    using Model.DataTypes;

    public class MaintenanceRequestsDataProvider : BaseDataProvider, IMaintenanceRequestsDataProvider
    {
        public MaintenanceRequestsDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task<MaintenanceRequestViewModel> CreateAsync(ClaimsPrincipal user, MaintenanceRequestViewModel maintenanceRequest)
        {
            var portfolioId = await GetPortfolioIdForUserAsync(user);
            var propertyDetailsId = await GetPropertyDetailsIdAsync(user, maintenanceRequest);
            var applicationUser = await user.GetApplicationUserAsync(Context);

            var entity = new MaintenanceRequest
            {
                Created = DateTime.Now,
                Description = maintenanceRequest.Description,
                Severity = maintenanceRequest.Severity,
                Title = maintenanceRequest.Title,
                PropertyDetailsId = propertyDetailsId
            };

            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();

            var maintenanceEntryEntity = new MaintenanceEntry
            {
                Created = DateTime.Now,
                MaintenanceRequestId = entity.Id,
                Status = MaintenanceStatus.Open,
                UserId = applicationUser.Id
            };

            await Context.AddAsync(maintenanceEntryEntity);
            await Context.SaveChangesAsync();

            return new MaintenanceRequestViewModel(entity, null)
            {
                Entries = new List<MaintenanceEntryViewModel>
                {
                    new MaintenanceEntryViewModel(maintenanceEntryEntity)
                    {
                        User = new ApplicationUserViewModel(applicationUser)
                    }
                }
            };
        }

        public async Task<MaintenanceEntryViewModel> UpdateMaintenanceEntryAsync(ClaimsPrincipal user, MaintenanceEntryViewModel viewModel)
        {
            var portfolioId = await GetPortfolioIdForUserAsync(user);

            var entity = await (from maintenanceEntry in Context.MaintenanceEntries
                                join maintenanceRequest in Context.MaintenanceRequests.Include(x => x.PropertyDetails) on maintenanceEntry.MaintenanceRequestId equals maintenanceRequest.Id
                                where maintenanceRequest.PropertyDetails.PortfolioId == portfolioId && maintenanceRequest.Id == viewModel.MaintenanceRequestId
                                select maintenanceEntry)
                .SingleAsync();

            entity.MapFrom(viewModel);
            await Context.SaveChangesAsync();

            return new MaintenanceEntryViewModel(entity);
        }

        public async Task<MaintenanceRequestViewModel> UpdateMaintenanceRequestAsync(ClaimsPrincipal user, MaintenanceRequestViewModel maintenanceRequest)
        {
            var portfolioId = await GetPortfolioIdForUserAsync(user);

            var entity = await Context.MaintenanceRequests.Include(x => x.Entries).Include(x => x.PropertyDetails)
                .SingleOrDefaultAsync(c => c.PropertyDetails.PortfolioId == portfolioId && c.Id == maintenanceRequest.Id);

            if (entity != null)
            {
                entity.MapFrom(maintenanceRequest);
                await Context.SaveChangesAsync();

                return new MaintenanceRequestViewModel(entity, entity.Entries);
            }

            return maintenanceRequest;
        }

        public async Task<MaintenanceEntryViewModel> AddMaintenanceEntryAsync(ClaimsPrincipal user, MaintenanceEntryViewModel maintenanceEntry)
        {
            var portfolioId = await GetPortfolioIdForUserAsync(user);
            var applicationUser = await user.GetApplicationUserAsync(Context);

            var maintenanceRequest = await Context.MaintenanceRequests.Include(x => x.PropertyDetails)
                .SingleOrDefaultAsync(c => c.PropertyDetails.PortfolioId == portfolioId && c.Id == maintenanceEntry.MaintenanceRequestId);

            if (maintenanceRequest != null)
            {
                var entity = new MaintenanceEntry
                {
                    Created = DateTime.Now,
                    Description = maintenanceEntry.Description,
                    MaintenanceRequestId = maintenanceRequest.Id,
                    Status = maintenanceEntry.Status,
                    UserId = applicationUser.Id
                };

                await Context.AddAsync(entity);
                await Context.SaveChangesAsync();

                return new MaintenanceEntryViewModel(entity)
                {
                    User = new ApplicationUserViewModel(applicationUser)
                };
            }

            return null;
        }

        public async Task<ICollection<MaintenanceRequestViewModel>> GetMaintenanceRequests(ClaimsPrincipal user)
        {
            var portfolioId = await GetPortfolioIdForUserAsync(user);

            var result = await (from maintenanceRequest in Context.MaintenanceRequests
                    join maintenanceEntry in Context.MaintenanceEntries on maintenanceRequest.Id equals maintenanceEntry.MaintenanceRequestId into maintenanceEntriesJoin
                    join propertyDetails in Context.PropertyDetails on maintenanceRequest.PropertyDetailsId equals propertyDetails.Id
                    join portfolio in Context.Portfolios on propertyDetails.PortfolioId equals portfolio.Id
                    where propertyDetails.PortfolioId == portfolioId && !maintenanceRequest.IsDeleted && !maintenanceRequest.IsArchived
                    select new MaintenanceRequestViewModel
                    {
                        Id = maintenanceRequest.Id,
                        Title = maintenanceRequest.Title,
                        Description = maintenanceRequest.Description,
                        Severity = maintenanceRequest.Severity,
                        IsArchived = maintenanceRequest.IsArchived,
                        PropertyDetails = new PropertyBasicDetailsViewModel
                        {
                            Id = propertyDetails.Id,
                            PortfolioId = propertyDetails.PortfolioId,
                            PortfolioName = portfolio.DisplayName,
                            PropertyStreetAddress = propertyDetails.PropertyStreetAddress,
                            PropertyReference = propertyDetails.Reference
                        },
                        Entries = maintenanceEntriesJoin
                            .Where(c => !c.IsDeleted)
                            .Select(c => new MaintenanceEntryViewModel
                            {
                                Id = c.Id,
                                MaintenanceRequestId = c.MaintenanceRequestId,
                                User = new ApplicationUserViewModel(c.User),
                                UserId = c.UserId,
                                Description = c.Description,
                                Created = c.Created,
                                Status = c.Status
                            })
                            .OrderByDescending(c => c.Created)
                            .ToList()
                    })
                .ToListAsync();

            return result;
        }

        public async Task ArchiveMaintenanceRequest(ClaimsPrincipal user, Guid maintenanceRequestId)
        {
            var portfolioId = await GetPortfolioIdForUserAsync(user);
            var maintenanceRequest = await Context.MaintenanceRequests.Include(x => x.PropertyDetails)
                .SingleOrDefaultAsync(c => c.PropertyDetails.PortfolioId == portfolioId && c.Id == maintenanceRequestId);

            if (maintenanceRequest != null)
            {
                maintenanceRequest.IsArchived = true;
                await Context.SaveChangesAsync();
            }
        }

        private async Task<Guid> GetPortfolioIdForUserAsync(ClaimsPrincipal user)
        {
            var userId = user.GetUserId();
            if (user.IsTenant())
            {
                return await (from portfolio in Context.Portfolios.AsNoTracking()
                              join propertyDetails in Context.PropertyDetails on portfolio.Id equals propertyDetails.PortfolioId into propertyDetailsJoin
                              from property in propertyDetailsJoin
                              join tenancy in Context.Tenancies on property.Id equals tenancy.PropertyDetailsId into tenanciesJoin
                              from tenancyItem in tenanciesJoin
                              join tenantTenancy in Context.TenantTenancies on tenancyItem.Id equals tenantTenancy.TenancyId into tenantTenanciesJoin
                              from tenantTenancyItem in tenantTenanciesJoin
                              join tenant in Context.Tenants on tenantTenancyItem.TenantId equals tenant.Id
                              join applicationUser in Context.Users on tenant.ApplicationUserId equals applicationUser.Id
                              where applicationUser.Id == userId && !property.IsDeleted && !tenancyItem.IsDeleted && !tenantTenancyItem.IsDeleted && !tenant.IsDeleted
                              orderby tenancyItem.StartDate descending
                              select portfolio.Id)
                    .FirstAsync();
            }

            return user.GetPortfolioId();
        }

        private async Task<Guid> GetPropertyDetailsIdAsync(ClaimsPrincipal user, MaintenanceRequestViewModel maintenanceRequest)
        {
            var userId = user.GetUserId();
            if (user.IsTenant())
            {
                return await (from portfolio in Context.Portfolios.AsNoTracking()
                              join propertyDetails in Context.PropertyDetails on portfolio.Id equals propertyDetails.PortfolioId into propertyDetailsJoin
                              from property in propertyDetailsJoin
                              join tenancy in Context.Tenancies on property.Id equals tenancy.PropertyDetailsId into tenanciesJoin
                              from tenancyItem in tenanciesJoin
                              join tenantTenancy in Context.TenantTenancies on tenancyItem.Id equals tenantTenancy.TenancyId into tenantTenanciesJoin
                              from tenantTenancyItem in tenantTenanciesJoin
                              join tenant in Context.Tenants on tenantTenancyItem.TenantId equals tenant.Id
                              join applicationUser in Context.Users on tenant.ApplicationUserId equals applicationUser.Id
                              where applicationUser.Id == userId && !property.IsDeleted && !tenancyItem.IsDeleted && !tenantTenancyItem.IsDeleted && !tenant.IsDeleted
                              orderby tenancyItem.StartDate descending
                              select property.Id)
                    .FirstAsync();
            }
            return maintenanceRequest.PropertyDetails.Id;
        }
    }
}