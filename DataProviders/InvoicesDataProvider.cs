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
    using Model.Entities;
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

        public async Task AddAsync(Guid portfolioId, InvoiceViewModel invoice)
        {
            var entity = new Invoice
            {
                Created = DateTime.Now,
                Date = invoice.Date,
                DueDate = invoice.DueDate,
                Number = invoice.Number,
                PoNumber = invoice.PoNumber,
                PortfolioId = portfolioId,
                SubTotal = invoice.SubTotal,
                Total = invoice.Total,
                VAT = invoice.VAT,
                SupplierId = invoice.Supplier.Id,
            };

            await Context.Invoices.AddAsync(entity);

            var invoiceLines = new List<InvoiceLine>();
            foreach (var invoiceLineViewModel in invoice.Lines)
            {
                invoiceLines.Add(new InvoiceLine
                {
                    Invoice = entity,
                    Item = invoiceLineViewModel.Item,
                    Created = DateTime.Now,
                    Description = invoiceLineViewModel.Description,
                    Quantity = invoiceLineViewModel.Quantity,
                    Total = invoiceLineViewModel.Total,
                    UnitCost = invoiceLineViewModel.UnitCost,
                    VAT = invoiceLineViewModel.VAT
                });
            }
            entity.Lines = invoiceLines;

            var supplier = await GetSupplierAsync(portfolioId, invoice);
            entity.Supplier = supplier;

            if (supplier.IsNewEntity)
            {
                await Context.Suppliers.AddAsync(supplier);
            }

            entity.Supplier = supplier;
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid portfolioId, InvoiceViewModel invoice)
        {
            var existingInvoice = await Context.Invoices.SingleAsync(c => c.PortfolioId == portfolioId && c.Id == invoice.Id);
            existingInvoice.MapFrom(invoice);

            existingInvoice.Supplier.MapFrom(invoice.Supplier);

            foreach (var invoiceLineViewModel in invoice.Lines)
            {
                var existingLine = existingInvoice.Lines.SingleOrDefault(c => c.InvoiceId == existingInvoice.Id && c.Id == invoiceLineViewModel.Id);
                if (existingLine != null)
                {
                    existingLine.MapFrom(invoiceLineViewModel);

                    if (invoiceLineViewModel.IsDeleted)
                    {
                        existingLine.Deleted = DateTime.Now;
                    }
                }
            }
        }

        private async Task<Supplier> GetSupplierAsync(Guid portfolioId, InvoiceViewModel invoice)
        {
            var existingEntity = await Context.Suppliers.SingleOrDefaultAsync(c => c.PortfolioId == portfolioId && c.Id == invoice.Supplier.Id);
            if (existingEntity != null)
            {
                return existingEntity;
            }

            var supplier = invoice.Supplier;

            return new Supplier
            {
                Created = DateTime.Now,
                AddressLine1 = supplier.AddressLine1,
                AddressLine2 = supplier.AddressLine2,
                AddressLine3 = supplier.AddressLine3,
                EmailAddress = supplier.EmailAddress,
                MainContactNumber = supplier.MainContactNumber,
                Name = supplier.Name,
                PortfolioId = portfolioId,
                PostCode = supplier.PostCode,
                SecondaryContactNumber = supplier.SecondaryContactNumber,
                TownOrCity = supplier.TownOrCity
            };
        }
    }
}