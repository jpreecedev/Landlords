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
    using Landlords.Interfaces;

    public class PropertyDataProvider : BaseDataProvider, IPropertyDataProvider
    {
        public PropertyDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task<PropertyDetailsViewModel> NewAsync(Guid portfolioId)
        {
            var entity = new PropertyDetails
            {
                Created = DateTime.Now,
                PortfolioId = portfolioId,
                Reference = "New property"
            };

            await Context.PropertyDetails.AddAsync(entity);
            await Context.SaveChangesAsync();

            return new PropertyDetailsViewModel(entity);
        }

        public async Task UpdateAsync(PropertyDetailsViewModel viewModel)
        {
            var existingEntity = await Context.PropertyDetails.Where(details => !details.IsDeleted)
                .FirstOrDefaultAsync(details => details.Id == viewModel.Id);

            if (existingEntity != null)
            {
                existingEntity.MapFrom(viewModel);
                Context.PropertyDetails.Update(existingEntity);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<PropertyDetailsViewModel> GetDetailsAsync(Guid propertyId)
        {
            return await (from details in Context.PropertyDetails
                          join images in Context.PropertyImages on details.Id equals images.PropertyId into imageJoin
                          from img in imageJoin.DefaultIfEmpty()
                          where details.Id == propertyId && !details.IsDeleted
                          select new PropertyDetailsViewModel(details.Id, details.PortfolioId)
                          {
                              Reference = details.Reference,
                              PropertyStreetAddress = details.PropertyStreetAddress,
                              Furnishing = details.Furnishing,
                              Bedrooms = details.Bedrooms,
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
                              MortgageAmount = details.MortgageAmount,
                              MortgageProvider = details.MortgageProvider,
                              InterestRate = details.InterestRate,
                              CurrentDealExpirationDate = details.CurrentDealExpirationDate,
                              MonthlyPayment = details.MonthlyPayment,
                              PropertyImages = imageJoin.Where(c => !c.IsDeleted).Select(c => new PropertyImageViewModel(c)).ToList()
                          })
                         .Distinct()
                         .FirstOrDefaultAsync();
        }

        public async Task<ICollection<PropertyDetailsViewModel>> GetListAsync(Guid portfolioId)
        {
            return await (from details in Context.PropertyDetails.Include(x => x.Portfolio)
                          where details.PortfolioId == portfolioId
                          join images in Context.PropertyImages on details.Id equals images.Property.Id into imageJoin
                          where !details.IsDeleted && !details.Portfolio.IsDeleted
                          select new PropertyDetailsViewModel(details.Id, details.PortfolioId)
                          {
                              Reference = details.Reference,
                              PropertyStreetAddress = details.PropertyStreetAddress,
                              LeadImage = imageJoin.Where(c => !c.IsDeleted).Select(c => new PropertyImageViewModel(c)).FirstOrDefault()
                          })
                          .ToListAsync();
        }
    }
}
