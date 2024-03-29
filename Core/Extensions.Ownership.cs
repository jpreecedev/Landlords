﻿namespace Landlords.Core
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Database;
    using Microsoft.EntityFrameworkCore;

    public static class Ownership
    {
        public static async Task<bool> OwnsChecklistAsync(this Guid portfolioId, Guid checklistId, ILLDbContext context)
        {
            if (portfolioId.IsDefault() || checklistId.IsDefault() || context == null)
            {
                return false;
            }

            return await context.ChecklistInstances.Where(c => c.PortfolioId == portfolioId && c.Id == checklistId).AnyAsync();
        }
        public static async Task<bool> OwnsPropertyImageAsync(this Guid userId, Guid propertyImageId, ILLDbContext context)
        {
            if (userId.IsDefault() || propertyImageId.IsDefault() || context == null)
            {
                return false;
            }

            return await context.ApplicationUserPortfolios.Include(x => x.Portfolio.Users)
                .Where(aup => aup.Portfolio.Users.Any(link => link.UserId == userId))
                .Join(context.PropertyDetails, aup => aup.PortfolioId, details => details.PortfolioId, (portfolio, details) => new { Portfolio = portfolio, PropertyDetails = details })
                .Join(context.PropertyImages, arg => arg.PropertyDetails.Id, image => image.PropertyId, (details, image) => new { PropertyDetails = details, PropertyImage = image })
                .AnyAsync(c => c.PropertyImage.Id == propertyImageId);
        }

        public static async Task<bool> OwnsPropertyDetailsAsync(this Guid userId, Guid propertyId, ILLDbContext context)
        {
            if (userId.IsDefault() || propertyId.IsDefault() || context == null)
            {
                return false;
            }

            return await context.PropertyDetails.Include(x => x.Portfolio.Users).AnyAsync(c => c.Portfolio.Users.Any(link => link.UserId == userId) && c.Id == propertyId);
        }

        public static async Task<bool> OwnsPortfolioAsync(this Guid userId, Guid portfolioId, ILLDbContext context)
        {
            if (userId.IsDefault() || portfolioId.IsDefault() || context == null)
            {
                return false;
            }

            return await context.ApplicationUserPortfolios.AnyAsync(c => c.UserId == userId && c.PortfolioId == portfolioId);
        }

        public static async Task<bool> OwnsAccountAsync(this Guid userId, Guid portfolioId, Guid accountId, ILLDbContext context)
        {
            if (userId.IsDefault() || portfolioId.IsDefault() || accountId.IsDefault() || context == null)
            {
                return false;
            }

            return await context.ApplicationUserPortfolios.AsNoTracking()
                .Join(context.Accounts, aup => aup.PortfolioId, account => account.PortfolioId, (portfolio, account) => new { ApplicationUserPortfolio = portfolio, Account = account })
                .Where(c => c.ApplicationUserPortfolio.UserId == userId && c.ApplicationUserPortfolio.PortfolioId == portfolioId && c.Account.Id == accountId)
                .AnyAsync();
        }

        public static async Task<bool> OwnsInvoiceAsync(this Guid userId, Guid portfolioId, Guid invoiceId, ILLDbContext context)
        {
            if (userId.IsDefault() || portfolioId.IsDefault() || invoiceId.IsDefault() || context == null)
            {
                return false;
            }

            return await context.ApplicationUserPortfolios.AsNoTracking()
                .Join(context.Invoices, aup => aup.PortfolioId, invoice => invoice.PortfolioId, (portfolio, invoice) => new { ApplicationUserPortfolio = portfolio, Invoice = invoice })
                .Where(c => c.ApplicationUserPortfolio.UserId == userId && c.ApplicationUserPortfolio.PortfolioId == portfolioId && c.Invoice.Id == invoiceId)
                .AnyAsync();
        }

        public static async Task<bool> OwnsSupplierAsync(this Guid userId, Guid portfolioId, Guid supplierId, ILLDbContext context)
        {
            if (userId.IsDefault() || portfolioId.IsDefault() || supplierId.IsDefault() || context == null)
            {
                return false;
            }

            return await context.ApplicationUserPortfolios.AsNoTracking()
                .Join(context.Suppliers, aup => aup.PortfolioId, supplier => supplier.PortfolioId, (portfolio, supplier) => new { ApplicationUserPortfolio = portfolio, Supplier = supplier })
                .Where(c => c.ApplicationUserPortfolio.UserId == userId && c.ApplicationUserPortfolio.PortfolioId == portfolioId && c.Supplier.Id == supplierId)
                .AnyAsync();
        }
    }
}