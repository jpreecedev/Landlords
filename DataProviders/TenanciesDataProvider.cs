namespace Landlords.DataProviders
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database;
    using Interfaces;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using ViewModels;
    using System.Linq;
    using Model.Entities;

    public class TenanciesDataProvider : BaseDataProvider, ITenanciesDataProvider
    {
        private readonly ITenantsDataProvider _tenantsDataProvider;

        public TenanciesDataProvider(ITenantsDataProvider tenantsDataProvider, IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
            _tenantsDataProvider = tenantsDataProvider;
        }

        public async Task<ICollection<TenantTenancyViewModel>> GetTenanciesByPortfolioIdAsync(Guid portfolioId)
        {
            //untested
            return await (from portfolio in Context.Portfolios.AsNoTracking()
                          join propertyDetails in Context.PropertyDetails on portfolio.Id equals propertyDetails.PortfolioId into propertyDetailsJoin
                          from property in propertyDetailsJoin
                          join tenancy in Context.Tenancies on property.Id equals tenancy.PropertyDetailsId into tenanciesJoin
                          from tenancyItem in tenanciesJoin
                          join tenantTenancy in Context.TenantTenancies on tenancyItem.Id equals tenantTenancy.TenancyId into tenantTenanciesJoin
                          from tenantTenancyItem in tenantTenanciesJoin
                          join tenant in Context.Tenants on tenantTenancyItem.TenantId equals tenant.Id
                          join applicationUser in Context.Users on tenant.ApplicationUserId equals applicationUser.Id
                          where portfolio.Id == portfolioId && !property.IsDeleted && !tenancyItem.IsDeleted && !tenantTenancyItem.IsDeleted && !tenant.IsDeleted && tenant.IsLeadTenant
                    select new TenantTenancyViewModel
                    {
                        TenancyId = tenancyItem.Id,
                        LeadTenant = applicationUser.Name,
                        TenantPhoneNumber = applicationUser.PhoneNumber,
                        TenantSecondaryPhoneNumber = applicationUser.SecondaryPhoneNumber,
                        TenantEmailAddress = applicationUser.Email,
                        PropertyReference = property.Reference,
                        TenancyEndDate = tenancyItem.EndDate
                    })
                .ToListAsync();
        }

        public async Task<ICollection<TenantTenancyViewModel>> GetTenanciesByAgencyIdAsync(Guid agencyId)
        {
            return null;
        }

        public async Task<StartTenancyJourneyViewModel> GetTenancyJourneyByIdAsync(Guid portfolioId, Guid tenancyId)
        {
            var tenantsDataSet = await (from tenant in Context.Tenants
                join applicationUser in Context.Users on tenant.ApplicationUserId equals applicationUser.Id
                join tenantTenancies in Context.TenantTenancies on tenant.Id equals tenantTenancies.TenantId into tenantTenanciesJoin
                join tenantAddresses in Context.TenantAddresses on tenant.Id equals tenantAddresses.TenantId into tenantAddressesJoin
                join tenantContacts in Context.TenantContacts on tenant.Id equals tenantContacts.TenantId into tenantContactsJoin
                from tenantTenanciesItem in tenantTenanciesJoin
                join tenancy in Context.Tenancies on tenantTenanciesItem.TenancyId equals tenancy.Id
                join propertyDetails in Context.PropertyDetails on tenancy.PropertyDetailsId equals propertyDetails.Id
                join portfolio in Context.Portfolios on propertyDetails.PortfolioId equals portfolio.Id
                where tenancy.Id == tenancyId && portfolio.Id == portfolioId && !tenant.IsDeleted && !tenancy.IsDeleted && !propertyDetails.IsDeleted && !portfolio.IsDeleted
                select new
                {
                    ApplicationUser = applicationUser,
                    Tenant = tenant,
                    TenantAddresses = tenantAddressesJoin.Where(c => !c.IsDeleted).ToList(),
                    TenantContacts = tenantContactsJoin.Where(c => !c.IsDeleted).ToList()
                })
                .ToListAsync();

            var tenancyDataSet = await (from tenancy in Context.Tenancies
                    join propertyDetails in Context.PropertyDetails on tenancy.PropertyDetailsId equals propertyDetails.Id
                    join portfolio in Context.Portfolios on propertyDetails.PortfolioId equals portfolio.Id
                    where tenancy.Id == tenancyId && portfolio.Id == portfolioId && !tenancy.IsDeleted && !portfolio.IsDeleted
                    select new
                    {
                        Tenancy = tenancy,
                        PropertyDetailsId = propertyDetails.Id
                    })
                    .SingleAsync();

            return new StartTenancyJourneyViewModel
            {
                Tenants = tenantsDataSet.Select(c =>
                {
                    return new TenantViewModel(c.Tenant, c.ApplicationUser)
                    {
                        Addresses = c.TenantAddresses.Select(d => new TenantAddressViewModel(!c.Tenant.IsAdult, d)).ToList(),
                        Contacts = c.TenantContacts.Select(d => new TenantContactViewModel(d)).ToList(),
                    };
                }).ToList(),
                Tenancy = new TenancyViewModel(tenancyDataSet.Tenancy, tenancyDataSet.PropertyDetailsId)
            };
        }

        public async Task<TenancyViewModel> CreateAsync(Guid portfolioId, TenancyViewModel viewModel, ICollection<TenantViewModel> tenants)
        {
            //Untested
            var tenancy = new Tenancy
            {
                PropertyDetailsId = viewModel.PropertyDetailsId,
                Created = DateTime.Now,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate,
                RentalAmount = viewModel.RentalAmount,
                RentalFrequency = viewModel.RentalFrequency,
                RentalPaymentReference = viewModel.RentalPaymentReference,
                TenancyType = viewModel.TenancyType
            };

            await Context.Tenancies.AddAsync(tenancy);
            await Context.SaveChangesAsync();

            var tenancyViewModel = new TenancyViewModel(tenancy, tenancy.PropertyDetailsId);

            return tenancyViewModel;
        }

        public async Task<TenancyViewModel> UpdateAsync(Guid portfolioId, TenancyViewModel viewModel, ICollection<TenantViewModel> tenants)
        {
            var entity = await (from tenancy in Context.Tenancies
                    join propertyDetails in Context.PropertyDetails on tenancy.PropertyDetailsId equals propertyDetails.Id
                    join portfolio in Context.Portfolios on propertyDetails.PortfolioId equals portfolio.Id
                    where portfolio.Id == portfolioId && tenancy.Id == viewModel.Id && !tenancy.IsDeleted && !portfolio.IsDeleted && !portfolio.IsDeleted
                    select tenancy)
                .SingleAsync();

            var propertyDetailsId = entity.PropertyDetailsId;

            entity.MapFrom(viewModel);

            var tenantTenacies = await (from tenantTenancy in Context.TenantTenancies.Include(x => x.Tenancy)
                    where tenantTenancy.TenancyId == entity.Id && tenantTenancy.Tenancy.PropertyDetailsId == propertyDetailsId && !tenantTenancy.IsDeleted && !tenantTenancy.Tenancy.IsDeleted && !tenantTenancy.Tenant.IsDeleted
                    select tenantTenancy)
                .ToListAsync();

            foreach (var tenantTenancy in tenantTenacies)
            {
                tenantTenancy.Tenancy.PropertyDetailsId = viewModel.PropertyDetailsId;
            }
            
            Context.Tenancies.Update(entity);
            await Context.SaveChangesAsync();

            return new TenancyViewModel(entity, viewModel.PropertyDetailsId);
        }

        public async Task CreateTenantTenancyAsync(TenancyViewModel tenancy, ICollection<TenantViewModel> tenants)
        {
            if (tenants == null || tenants.Count == 0)
            {
                return;
            }
            
            foreach (var tenantViewModel in tenants)
            {
                var entity = await Context.TenantTenancies.FirstOrDefaultAsync(c => c.TenancyId == tenancy.Id && c.TenantId == tenantViewModel.Id && !c.IsDeleted);
                if (entity == null)
                {
                    await Context.TenantTenancies.AddAsync(new TenantTenancy
                    {
                        Created = DateTime.Now,
                        TenancyId = tenancy.Id,
                        TenantId = tenantViewModel.Id
                    });
                }
            }

            await Context.SaveChangesAsync();
        }
    }
}