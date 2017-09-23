namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;

    public interface ISuppliersDataProvider
    {
        Task<ICollection<SupplierViewModel>> GetAsync(Guid portfolioId);
        Task<SupplierViewModel> GetByIdAsync(Guid portfolioId, Guid supplierId);
        Task<SupplierViewModel> UpdateAsync(Guid portfolioId, SupplierViewModel supplier);
        Task DeleteAsync(Guid portfolioId, Guid supplierId);
    }
}