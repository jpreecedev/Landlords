namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;

    public interface ITenantsDataProvider
    {
        Task<ICollection<TenantViewModel>> GetTenantsAsync(Guid portfolioId);
        Task<ICollection<TenantViewModel>> GetTenantsForPropertyAsync(Guid portfolioId, Guid propertyDetailsId);
        Task<TenantViewModel> NewAsync(Guid portfolioId);
        Task<TenantViewModel> GetTenantByIdAsync(Guid portfolioId, Guid tenantId);
        Task UpdateAsync(Guid portfolioId, TenantViewModel tenant);
    }
}