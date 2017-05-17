﻿namespace Landlords.Database
{
    using Microsoft.EntityFrameworkCore;
    using Model;
    using Model.Database;

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

        DbSet<ChecklistItem> ChecklistItems { get; set; }
        
        DbSet<ChecklistInstance> ChecklistInstances { get; set; }

        DbSet<ChecklistItemInstance> ChecklistItemInstances { get; set; }
        
        DbSet<T> Set<T>() where T : class;
    }
}