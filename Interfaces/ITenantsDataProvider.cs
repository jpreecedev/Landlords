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
        Task<TenantViewModel> NewAsync();
        Task<TenantViewModel> GetTenantByIdAsync(Guid tenantId);
        Task UpdateAsync(TenantViewModel tenant);
    }
}