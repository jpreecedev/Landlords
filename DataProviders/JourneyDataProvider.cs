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
    using ViewModels;

    public class JourneyDataProvider : BaseDataProvider, IJourneyDataProvider
    {
        private readonly ITenanciesDataProvider _tenanciesDataProvider;
        private readonly ITenantsDataProvider _tenantsDataProvider;

        public JourneyDataProvider(ITenanciesDataProvider tenanciesDataProvider, ITenantsDataProvider tenantsDataProvider, IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
            _tenanciesDataProvider = tenanciesDataProvider;
            _tenantsDataProvider = tenantsDataProvider;
        }

        public async Task CreateTenancyAsync(Guid portfolioId, StartTenancyJourneyViewModel viewModel)
        {
            //Untested

            var tenants = new List<TenantViewModel>();
            foreach (var tenant in viewModel.Tenants)
            {
                tenants.Add(await _tenantsDataProvider.CreateAsync(portfolioId, tenant));
            }

            var tenancy = await _tenanciesDataProvider.CreateAsync(portfolioId, viewModel.Tenancy, tenants);
            await _tenanciesDataProvider.CreateTenantTenancyAsync(tenancy, tenants);
        }

        public async Task UpdateTenancyAsync(Guid portfolioId, StartTenancyJourneyViewModel viewModel)
        {
            // If the tenant has previous been created, and has now been deleted, this will be reflected in IsDeleted on the tenant view model
            // same for tenant address
            // same for tenant contact

            var hasTenantTenancy = await (from tt in Context.TenantTenancies.AsNoTracking()
                    join tenancy in Context.Tenancies on tt.TenancyId equals tenancy.Id
                    join propertyDetails in Context.PropertyDetails on tenancy.PropertyDetailsId equals propertyDetails.Id
                    join portfolio in Context.Portfolios on propertyDetails.PortfolioId equals portfolio.Id
                    where portfolio.Id == portfolioId && tenancy.Id == viewModel.Tenancy.Id
                    select tt)
                .AnyAsync();

            if (!hasTenantTenancy)
            {
                throw new InvalidOperationException("Unable to find tenant tenancy");
            }

            var updatedTenants = await _tenantsDataProvider.UpdateAsync(portfolioId, viewModel.Tenants);
            var updatedTenancy = await _tenanciesDataProvider.UpdateAsync(portfolioId, viewModel.Tenancy, viewModel.Tenants);

            await _tenanciesDataProvider.CreateTenantTenancyAsync(updatedTenancy, updatedTenants);
        }
    }
}