namespace Landlords.DataProviders
{
    using System;
    using System.Security.Claims;
    using ViewModels;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Model;

    public interface IPropertyDataProvider
    {
        Task CreateAsync(ClaimsPrincipal user, PropertyDetailsViewModel viewModel);
        Task<PropertyDetailsViewModel> GetDetailsAsync(Guid userId, Guid propertyId);
        Task<ICollection<PropertyDetailsViewModel>> GetListAsync(Guid guid);
    }
}
