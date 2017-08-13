namespace Landlords.DataProviders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Database;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Model.Entities;
    using ViewModels;
    using Microsoft.AspNetCore.Hosting;

    public class ShortlistedPropertiesDataProvider : BaseDataProvider, IShortlistedPropertiesDataProvider
    {
        public ShortlistedPropertiesDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task DeleteAsync(Guid portfolioId, Guid shortlistedPropertyId)
        {
            var shortlistedProperty = await Context.ShortlistedProperties.FirstOrDefaultAsync(c => c.PortfolioId == portfolioId && c.Id == shortlistedPropertyId);
            if (shortlistedProperty != null)
            {
                shortlistedProperty.Deleted = DateTime.Now;
                await Context.SaveChangesAsync();
            }
        }

        public async Task<ShortlistedPropertyViewModel> GetAsync(Guid portfolioId, Guid shortlistedPropertyId)
        {
            return new ShortlistedPropertyViewModel(await Context.ShortlistedProperties.FirstOrDefaultAsync(c => c.PortfolioId == portfolioId && c.Id == shortlistedPropertyId));
        }

        public async Task<List<ShortlistedPropertyViewModel>> GetListAsync(Guid portfolioId)
        {
            return await (from shortlistedProperty in Context.ShortlistedProperties
                    where shortlistedProperty.PortfolioId == portfolioId && !shortlistedProperty.IsDeleted
                    select new ShortlistedPropertyViewModel
                    {
                        Address = shortlistedProperty.Address,
                        Reference = shortlistedProperty.Reference,
                        ManagementCost = shortlistedProperty.ManagementCost,
                        Deposit = shortlistedProperty.Deposit,
                        ExpectedRentalIncome = shortlistedProperty.ExpectedRentalIncome,
                        Fees = shortlistedProperty.Fees,
                        Insurance = shortlistedProperty.Insurance,
                        LettableUnits = shortlistedProperty.LettableUnits,
                        MortgageInterestRate = shortlistedProperty.MortgageInterestRate,
                        PricePaid = shortlistedProperty.PricePaid,
                        ServiceCharge = shortlistedProperty.ServiceCharge,
                        RepairsContingency = shortlistedProperty.RepairsContingency,
                        ShortlistedPropertyId = shortlistedProperty.Id
                    })
                .ToListAsync();
        }

        public async Task UpdateAsync(Guid portfolioId, ShortlistedPropertyViewModel viewModel)
        {
            var existingEntity = await Context.ShortlistedProperties.FirstOrDefaultAsync(c => c.PortfolioId == portfolioId && !c.IsDeleted && c.Id == viewModel.ShortlistedPropertyId);
            if (existingEntity != null)
            {
                existingEntity.MapFrom(viewModel);
                Context.ShortlistedProperties.Update(existingEntity);
                await Context.SaveChangesAsync();
            }
            else
            {
                var newEntity = new ShortlistedProperty
                {
                    Address = viewModel.Address,
                    Created = DateTime.Now,
                    Deposit = viewModel.Deposit,
                    ExpectedRentalIncome = viewModel.ExpectedRentalIncome,
                    Fees = viewModel.Fees,
                    Insurance = viewModel.Insurance,
                    LettableUnits = viewModel.LettableUnits,
                    ManagementCost = viewModel.ManagementCost,
                    MortgageInterestRate = viewModel.MortgageInterestRate,
                    PortfolioId = portfolioId,
                    PricePaid = viewModel.PricePaid,
                    Reference = viewModel.Reference,
                    RepairsContingency = viewModel.RepairsContingency,
                    ServiceCharge = viewModel.ServiceCharge
                };

                await Context.ShortlistedProperties.AddAsync(newEntity);
                await Context.SaveChangesAsync();
            }
        }
    }
}