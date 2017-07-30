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
        Task<TenantTenancyViewModel> GetTenancyByIdAsync(Guid portfolioId, Guid tenancyId);
    }
}