namespace Landlords.DataProviders
{
    using Model;
    using System;
    using ViewModels;

    public interface IPropertyDataProvider
    {
        PropertyDetails GetDetails(Guid userId, Guid propertyId);
        void Create(Guid userId, PropertyDetailsViewModel viewModel);
    }
}
