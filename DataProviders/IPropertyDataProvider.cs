namespace Landlords.DataProviders
{
    using Model;
    using System;
    using System.Security.Claims;
    using ViewModels;
    using System.Threading.Tasks;

    public interface IPropertyDataProvider
    {
        Task CreateAsync(ClaimsPrincipal user, PropertyDetailsViewModel viewModel);
        Task<PropertyDetails> GetDetailsAsync(Guid userId, Guid propertyId);
    }
}
