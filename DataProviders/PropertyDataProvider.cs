namespace Landlords.DataProviders
{
    using Model;
    using Landlords.Database;
    using Landlords.ViewModels;
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;

    public class PropertyDataProvider : BaseDataProvider<PropertyDetails>, IPropertyDataProvider
    {
        public PropertyDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task<PropertyDetailsViewModel> NewAsync(Guid userId)
        {
            PropertyDetails entity = new PropertyDetails();
            PopulateNewEntity(userId, entity);

            await Context.PropertyDetails.AddAsync(entity);
            await Context.SaveChangesAsync();

            return new PropertyDetailsViewModel(entity);
        }

        public async Task UpdateAsync(Guid userId, PropertyDetailsViewModel viewModel)
        {
            var existingEntity = await Context.PropertyDetails.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == viewModel.Id);
            if (existingEntity != null)
            {
                existingEntity.MapFrom(viewModel);
                Context.PropertyDetails.Update(existingEntity);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<PropertyDetailsViewModel> GetDetailsAsync(Guid userId, Guid propertyId)
        {
            return await (from details in Context.PropertyDetails
                          join images in Context.PropertyImages on details.Id equals images.PropertyId into imageJoin
                          from img in imageJoin.DefaultIfEmpty()
                          where details.UserId == userId && details.Id == propertyId && !details.IsDeleted
                          select new PropertyDetailsViewModel(details.Id, userId)
                          {
                              Reference = details.Reference,
                              PropertyStreetAddress = details.PropertyStreetAddress,
                              Furnishing = details.Furnishing,
                              PropertyType = details.PropertyType,
                              ConstructionDate = details.ConstructionDate,
                              TargetRent = details.TargetRent,
                              PaymentTerm = details.PaymentTerm,
                              PurchaseDate = details.PurchaseDate,
                              PurchasePrice = details.PurchasePrice,
                              SellingDate = details.SellingDate,
                              SellingPrice = details.SellingPrice,
                              PropertyTownOrCity = details.PropertyTownOrCity,
                              PropertyCountyOrRegion = details.PropertyCountyOrRegion,
                              PropertyPostcode = details.PropertyPostcode,
                              PropertyCountry = details.PropertyCountry,
                              IsAvailableForLetting = details.IsAvailableForLetting,
                              PropertyImages = imageJoin.Where(c => !c.IsDeleted).Select(c => new PropertyImageViewModel(c)).ToList()
                          })
                         .FirstOrDefaultAsync();
        }

        public async Task<ICollection<PropertyDetailsViewModel>> GetListAsync(Guid userId)
        {
            return await (from details in Context.PropertyDetails
                          join images in Context.PropertyImages on details.Id equals images.PropertyId into imageJoin
                          where details.UserId == userId && !details.IsDeleted
                          select new PropertyDetailsViewModel(details.Id, userId)
                          {
                              Reference = details.Reference,
                              PropertyStreetAddress = details.PropertyStreetAddress,
                              LeadImage = imageJoin.Where(c => !c.IsDeleted).Select(c => new PropertyImageViewModel(c)).FirstOrDefault()
                          })
                          .ToListAsync();
        }
    }
}
