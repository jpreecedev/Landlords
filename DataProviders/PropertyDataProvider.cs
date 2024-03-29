﻿namespace Landlords.DataProviders
{
    using Landlords.Database;
    using Landlords.ViewModels;
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Landlords.Interfaces;
    using Model.Entities;

    public class PropertyDataProvider : BaseDataProvider, IPropertyDataProvider
    {
        private readonly INotificationsDataProvider _notificationsDataProvider;

        public PropertyDataProvider(INotificationsDataProvider notificationsDataProvider, IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
            _notificationsDataProvider = notificationsDataProvider;
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

        public async Task UpdateAsync(Guid portfolioId, PropertyDetailsViewModel viewModel)
        {
            var existingEntity = await Context.PropertyDetails.Where(details => details.PortfolioId == portfolioId && !details.IsDeleted)
                .FirstOrDefaultAsync(details => details.Id == viewModel.Id);

            if (existingEntity != null)
            {
                existingEntity.MapFrom(viewModel);
                Context.PropertyDetails.Update(existingEntity);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<PropertyBasicDetailsViewModel>> GetBasicDetailsAsync(Guid userId)
        {
            return await Context.ApplicationUserPortfolios.Where(aup => aup.UserId == userId)
                .Join(Context.PropertyDetails, aup => aup.PortfolioId, details => details.PortfolioId, (portfolio, details) => new {Portfolio = portfolio.Portfolio, PropertyDetails = details})
                .Where(c => !c.PropertyDetails.IsDeleted)
                .Select(c => new PropertyBasicDetailsViewModel
                {
                    PortfolioId = c.Portfolio.Id,
                    PortfolioName = c.Portfolio.DisplayName,
                    Id = c.PropertyDetails.Id,
                    PropertyReference = c.PropertyDetails.Reference,
                    PropertyStreetAddress = c.PropertyDetails.PropertyStreetAddress,
                })
                .ToListAsync();
        }

        public async Task<Guid> PromoteAsync(Guid portfolioId, ShortlistedPropertyViewModel viewModel)
        {
            var entity = new PropertyDetails
            {
                Reference = viewModel.Reference,
                PropertyStreetAddress = viewModel.Address,
                PurchasePrice = viewModel.PricePaid,
                TargetRent = viewModel.ExpectedRentalIncome,
                InterestRate = (double?) viewModel.MortgageInterestRate,
                Created = DateTime.Now,
                PortfolioId = portfolioId,
                MortgageAmount = viewModel.PricePaid - viewModel.Deposit
            };

            await Context.PropertyDetails.AddAsync(entity);
            await Context.SaveChangesAsync();

            var shortlistedPropertyEntity = await Context.ShortlistedProperties.FirstOrDefaultAsync(c => c.Id == viewModel.ShortlistedPropertyId && c.PortfolioId == portfolioId);
            if (shortlistedPropertyEntity != null)
            {
                shortlistedPropertyEntity.Deleted = DateTime.Now;
                await Context.SaveChangesAsync();
            }

            return entity.Id;
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
