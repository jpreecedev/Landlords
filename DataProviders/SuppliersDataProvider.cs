namespace Landlords.DataProviders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Database;
    using Interfaces;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using ViewModels;
    using Model.Entities;

    public class SuppliersDataProvider : BaseDataProvider, ISuppliersDataProvider
    {
        public SuppliersDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task DeleteAsync(Guid portfolioId, Guid supplierId)
        {
            var supplier = await Context.Suppliers.SingleOrDefaultAsync(c => c.PortfolioId == portfolioId && c.Id == supplierId);
            if (supplier != null)
            {
                supplier.Deleted = DateTime.Now;
                await Context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<SupplierViewModel>> GetAsync(Guid portfolioId)
        {
            //untested

            return await (from supplier in Context.Suppliers.AsNoTracking()
                    join portfolio in Context.Portfolios on supplier.PortfolioId equals portfolio.Id
                    where !supplier.IsDeleted && portfolio.Id == portfolioId
                    select new SupplierViewModel
                    {
                        Id = supplier.Id,
                        Name = supplier.Name,
                        EmailAddress = supplier.EmailAddress,
                        PortfolioId = supplier.PortfolioId,
                        SecondaryContactNumber = supplier.SecondaryContactNumber,
                        AddressLine1 = supplier.AddressLine1,
                        AddressLine3 = supplier.AddressLine3,
                        AddressLine2 = supplier.AddressLine2,
                        TownOrCity = supplier.TownOrCity,
                        PostCode = supplier.PostCode,
                        MainContactNumber = supplier.MainContactNumber
                    })
                .ToListAsync();
        }

        public async Task<SupplierViewModel> GetByIdAsync(Guid portfolioId, Guid supplierId)
        {
            return await (from supplier in Context.Suppliers.AsNoTracking()
                    join portfolio in Context.Portfolios on supplier.PortfolioId equals portfolio.Id
                    where !supplier.IsDeleted && portfolio.Id == portfolioId && supplier.Id == supplierId
                    select new SupplierViewModel
                    {
                        Id = supplier.Id,
                        Name = supplier.Name,
                        EmailAddress = supplier.EmailAddress,
                        PortfolioId = supplier.PortfolioId,
                        SecondaryContactNumber = supplier.SecondaryContactNumber,
                        AddressLine1 = supplier.AddressLine1,
                        AddressLine3 = supplier.AddressLine3,
                        AddressLine2 = supplier.AddressLine2,
                        TownOrCity = supplier.TownOrCity,
                        PostCode = supplier.PostCode,
                        MainContactNumber = supplier.MainContactNumber
                    })
                .SingleAsync();
        }

        public async Task<SupplierViewModel> UpdateAsync(Guid portfolioId, SupplierViewModel supplier)
        {
            var existingEntity = await Context.Suppliers.SingleOrDefaultAsync(c => c.PortfolioId == portfolioId && c.Id == supplier.Id && !c.IsDeleted);
            if (existingEntity == null)
            {
                var entity = new Supplier
                {
                    Created = DateTime.Now,
                    MainContactNumber = supplier.MainContactNumber,
                    PostCode = supplier.PostCode,
                    AddressLine1 = supplier.AddressLine1,
                    AddressLine2 = supplier.AddressLine2,
                    AddressLine3 = supplier.AddressLine3,
                    EmailAddress = supplier.EmailAddress,
                    Name = supplier.Name,
                    PortfolioId = portfolioId,
                    SecondaryContactNumber = supplier.SecondaryContactNumber,
                    TownOrCity = supplier.TownOrCity
                };

                await Context.Suppliers.AddAsync(entity);
                await Context.SaveChangesAsync();

                return new SupplierViewModel(entity);
            }
            else
            {
                existingEntity.MapFrom(supplier);
                Context.Suppliers.Update(existingEntity);
                await Context.SaveChangesAsync();

                return new SupplierViewModel(existingEntity);
            }
        }
    }
}