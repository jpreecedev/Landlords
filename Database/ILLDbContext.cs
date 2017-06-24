namespace Landlords.Database
{
    using Microsoft.EntityFrameworkCore;
    using Model.Database;
    using Model.Entities;

    public interface ILLDbContext
    {
        DbSet<PropertyDetails> PropertyDetails { get; set; }

        DbSet<PropertyImage> PropertyImages { get; set; }

        DbSet<ApplicationUser> Users { get; set; }

        DbSet<UserPermission> UserPermissions { get; set; }

        DbSet<Agency> Agencies { get; set; }

        DbSet<Portfolio> Portfolios { get; set; }

        DbSet<ApplicationUserPortfolio> ApplicationUserPortfolios { get; set; }

        DbSet<EmailTemplate> EmailTemplates { get; set; }

        DbSet<Checklist> Checklists { get; set; }

        DbSet<ChecklistInstance> ChecklistInstances { get; set; }

        DbSet<Account> Accounts { get; set; }

        DbSet<Transaction> Transactions { get; set; }

        DbSet<Tenant> Tenants { get; set; }

        DbSet<TenantAddress> TenantAddresses { get; set; }

        DbSet<Tenancy> Tenancies { get; set; }

        DbSet<TenantTenancy> TenantTenancies { get; set; }

        DbSet<T> Set<T>() where T : class;
    }
}