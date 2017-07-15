namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;

    public interface IShortlistedPropertiesDataProvider
    {
        Task<ShortlistedPropertyViewModel> GetAsync(Guid portfolioId, Guid shortlistedPropertyId);
        Task<List<ShortlistedPropertyViewModel>> GetListAsync(Guid portfolioId);
        Task DeleteAsync(Guid portfolioId, Guid shortlistedPropertyId);
        Task UpdateAsync(Guid portfolioId, ShortlistedPropertyViewModel viewModel);
    }
}