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

        public async Task<PropertyDetailsViewModel> GetDetailsAsync(Guid userId, Guid propertyId)
        {
            return await (from details in _dataContext.PropertyDetails
                          join images in _dataContext.PropertyImages on details.Id equals images.PropertyId into imageJoin
                          from img in imageJoin.DefaultIfEmpty()
                          where details.UserId == userId && details.Id == propertyId
                          select new PropertyDetailsViewModel(details.Id, userId)
                          {
                              Reference = details.Reference,
                              PropertyStreetAddress = details.PropertyStreetAddress,
                              PropertyImages = imageJoin.Select(c => new PropertyImageViewModel(c)).ToList()
                          })
                         .FirstOrDefaultAsync();
        }

        public async Task<ICollection<PropertyDetailsViewModel>> GetListAsync(Guid userId)
        {
            return await (from details in _dataContext.PropertyDetails
                          join images in _dataContext.PropertyImages on details.Id equals images.PropertyId into imageJoin
                          from img in imageJoin.DefaultIfEmpty()
                          where details.UserId == userId
                          select new PropertyDetailsViewModel(details.Id, userId)
                          {
                              Reference = details.Reference,
                              PropertyStreetAddress = details.PropertyStreetAddress,
                              LeadImage = imageJoin.Select(c => new PropertyImageViewModel(c)).FirstOrDefault()
                          })
                          .ToListAsync();
        }
    }
}
