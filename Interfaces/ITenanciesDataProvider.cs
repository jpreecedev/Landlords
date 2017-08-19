namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;

    public interface ITenanciesDataProvider
    {
        Task<ICollection<TenantTenancyViewModel>> GetTenanciesByPortfolioIdAsync(Guid portfolioId);
        Task<ICollection<TenantTenancyViewModel>> GetTenanciesByAgencyIdAsync(Guid agencyId);
        Task<StartTenancyJourneyViewModel> GetTenancyJourneyByIdAsync(Guid portfolioId, Guid tenancyId);
        Task<TenancyViewModel> CreateAsync(Guid portfolioId, TenancyViewModel viewModel, ICollection<TenantViewModel> tenants);
    }
}