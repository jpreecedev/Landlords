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

    public class InvoicesDataProvider : BaseDataProvider, IInvoicesDataProvider
    {
        public InvoicesDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task DeleteAsync(Guid portfolioId, Guid invoiceId)
        {
            var invoice = await Context.Invoices.SingleOrDefaultAsync(c => c.PortfolioId == portfolioId && c.Id == invoiceId);
            if (invoice != null)
            {
                invoice.Deleted = DateTime.Now;
                await Context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<InvoiceViewModel>> GetAsync(Guid portfolioId)
        {
            //untested

            return await (from invoice in Context.Invoices.AsNoTracking()
                    join portfolio in Context.Portfolios on invoice.PortfolioId equals portfolio.Id
                    join supplier in Context.Suppliers on invoice.SupplierId equals supplier.Id
                    join invoiceLine in Context.InvoiceLines on invoice.Id equals invoiceLine.InvoiceId into invoiceLinesJoin
                    where !invoice.IsDeleted && portfolio.Id == portfolioId
                    select new InvoiceViewModel
                    {
                        Id = invoice.Id,
                        Date = invoice.Date,
                        Total = invoice.Total,
                        VAT = invoice.VAT,
                        DueDate = invoice.DueDate,
                        Number = invoice.Number,
                        PoNumber = invoice.PoNumber,
                        SubTotal = invoice.SubTotal,
                        Supplier = new SupplierViewModel
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
                        },
                        Lines = invoiceLinesJoin.Select(c => new InvoiceLineViewModel
                        {
                            Id = c.Id,
                            Description = c.Description,
                            Total = c.Total,
                            VAT = c.VAT,
                            UnitCost = c.UnitCost,
                            Quantity = c.Quantity,
                            Item = c.Item
                        }).ToList()
                    })
                .ToListAsync();
        }

        public async Task<InvoiceViewModel> GetByIdAsync(Guid portfolioId, Guid invoiceId)
        {
            return await(from invoice in Context.Invoices.AsNoTracking()
                    join portfolio in Context.Portfolios on invoice.PortfolioId equals portfolio.Id
                    join supplier in Context.Suppliers on invoice.SupplierId equals supplier.Id
                    join invoiceLine in Context.InvoiceLines on invoice.Id equals invoiceLine.InvoiceId into invoiceLinesJoin
                    where !invoice.IsDeleted && portfolio.Id == portfolioId && invoice.Id == invoiceId
                    select new InvoiceViewModel
                    {
                        Id = invoice.Id,
                        Date = invoice.Date,
                        Total = invoice.Total,
                        VAT = invoice.VAT,
                        DueDate = invoice.DueDate,
                        Number = invoice.Number,
                        PoNumber = invoice.PoNumber,
                        SubTotal = invoice.SubTotal,
                        Supplier = new SupplierViewModel
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
                        },
                        Lines = invoiceLinesJoin.Select(c => new InvoiceLineViewModel
                        {
                            Id = c.Id,
                            Description = c.Description,
                            Total = c.Total,
                            VAT = c.VAT,
                            UnitCost = c.UnitCost,
                            Quantity = c.Quantity,
                            Item = c.Item
                        }).ToList()
                    })
                .SingleAsync();
        }

        public async Task UpdateAsync(Guid portfolioId, InvoiceViewModel invoice)
        {
            var existingEntity = await Context.Invoices.SingleOrDefaultAsync(c => c.PortfolioId == portfolioId && c.Id == invoice.Id && !c.IsDeleted);
            if (existingEntity != null)
            {
                existingEntity.MapFrom(invoice);
                Context.Invoices.Update(existingEntity);
                await Context.SaveChangesAsync();
            }
        }
    }
}