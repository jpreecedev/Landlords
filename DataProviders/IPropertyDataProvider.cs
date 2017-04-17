namespace Landlords.DataProviders
{
    using Model;
    using System;
    using System.Security.Claims;
    using ViewModels;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IPropertyDataProvider
    {
        Task CreateAsync(ClaimsPrincipal user, PropertyDetailsViewModel viewModel);
        Task<PropertyDetails> GetDetailsAsync(Guid userId, Guid propertyId);
        Task<ICollection<PropertyDetails>> GetListAsync(Guid guid);
    }
}
