namespace Landlords.DataProviders
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database;
    using Interfaces;
    using Microsoft.AspNetCore.Hosting;
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

            await _tenanciesDataProvider.CreateAsync(portfolioId, viewModel.Tenancy, tenants);
        }

        public async Task UpdateTenancyAsync(Guid portfolioId, StartTenancyJourneyViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}