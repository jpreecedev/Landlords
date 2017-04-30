namespace Landlords.DataProviders
{
    using Model;
    using Landlords.Database;
    using Landlords.ViewModels;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Core;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class PropertyDataProvider : BaseDataProvider<PropertyDetails>, IPropertyDataProvider
    {
        private readonly LLDbContext _dataContext;

        public PropertyDataProvider(LLDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task CreateAsync(ClaimsPrincipal user, PropertyDetailsViewModel viewModel)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            PropertyDetails entity = viewModel.Map();

            var applicationUser = user.GetApplicationUser(_dataContext);
            PopulateNewEntity(applicationUser, entity);

            await _dataContext.PropertyDetails.AddAsync(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<PropertyDetails> GetDetailsAsync(Guid userId, Guid propertyId)
        {
            return await _dataContext.PropertyDetails.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == propertyId);
        }

        public async Task<ICollection<PropertyDetails>> GetListAsync(Guid userId)
        {
            return await _dataContext.PropertyDetails
                .Where(c => c.UserId == userId)
                .Select(c => new PropertyDetails
                {
                    Id = c.Id,
                    Reference = c.Reference,
                    PropertyStreetAddress = c.PropertyStreetAddress
                })
                .ToListAsync();
        }
    }
}
