namespace Landlords.DataProviders
{
    using Model;
    using Landlords.Database;
    using Landlords.ViewModels;
    using System;

    public class PropertyDataProvider : IPropertyDataProvider
    {
        private readonly LLDbContext _dataContext;

        public PropertyDataProvider(LLDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Create(Guid userId, PropertyDetailsViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public PropertyDetails GetDetails(Guid userId, Guid propertyId)
        {
            throw new NotImplementedException();
        }
    }
}
