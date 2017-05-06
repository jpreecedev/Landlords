namespace Landlords.DataProviders
{
    using System;
    using ViewModels;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IPropertyDataProvider
    {
        Task<PropertyDetailsViewModel> NewAsync(Guid portfolioId);
        Task<ICollection<PropertyDetailsViewModel>> GetListAsync(Guid portfolioId);
        Task<PropertyDetailsViewModel> GetDetailsAsync(Guid propertyId);
        Task UpdateAsync(PropertyDetailsViewModel viewModel);
    }
}
