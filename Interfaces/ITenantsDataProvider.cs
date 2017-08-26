namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;

    public interface ITenantsDataProvider
    {
        Task<ICollection<TenantViewModel>> GetTenantsByPortfolioIdAsync(Guid portfolioId);
        Task<ICollection<TenantViewModel>> GetTenantsForPropertyAsync(Guid portfolioId, Guid propertyDetailsId);
        Task<ICollection<TenantViewModel>> GetTenantsByAgencyIdAsync(Guid agencyId);
        Task<TenantViewModel> GetTenantByIdAsync(Guid tenantId);

        Task<TenantViewModel> CreateAsync(Guid portfolioId, TenantViewModel tenant);

        Task<ICollection<TenantViewModel>> UpdateAsync(Guid portfolioId, ICollection<TenantViewModel> tenants);
        Task<TenantViewModel> UpdateAsync(Guid portfolioId, TenantViewModel tenant);
    }
}