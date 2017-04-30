namespace Landlords.DataProviders
{
    using System;
    using ViewModels;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IPropertyDataProvider
    {
        Task<PropertyDetailsViewModel> GetDetailsAsync(Guid userId, Guid propertyId);
        Task<ICollection<PropertyDetailsViewModel>> GetListAsync(Guid guid);
        Task<PropertyDetailsViewModel> NewAsync(Guid userId);
        Task UpdateAsync(Guid userId, PropertyDetailsViewModel viewModel);
    }
}
