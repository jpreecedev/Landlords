namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;

    public interface IMaintenanceRequestsDataProvider
    {
        Task<MaintenanceRequestViewModel> CreateForPortfolioAsync(Guid userId, Guid portfolioId, MaintenanceRequestViewModel maintenanceRequest);
        Task<MaintenanceRequestViewModel> CreateForTenantAsync(Guid userId, MaintenanceRequestViewModel maintenanceRequest);

        Task<MaintenanceRequestViewModel> UpdateMaintenanceRequestAsync(Guid userId, Guid? portfolioId, MaintenanceRequestViewModel maintenanceRequest);
        Task<MaintenanceEntryViewModel> UpdateMaintenanceEntryAsync(Guid userId, Guid? portfolioId, MaintenanceEntryViewModel viewModel);

        Task<MaintenanceEntryViewModel> AddMaintenanceEntryAsync(Guid userId, Guid? portfolioId, MaintenanceEntryViewModel maintenanceEntry);

        Task<ICollection<MaintenanceRequestViewModel>> GetMaintenanceRequestsForTenant(Guid userId);
        Task<ICollection<MaintenanceRequestViewModel>> GetMaintenanceRequests(Guid portfolioId);
    }
}