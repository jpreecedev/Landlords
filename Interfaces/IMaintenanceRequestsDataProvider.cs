namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;
    using System.Security.Claims;

    public interface IMaintenanceRequestsDataProvider
    {
        Task<MaintenanceRequestViewModel> CreateAsync(ClaimsPrincipal user, MaintenanceRequestViewModel maintenanceRequest);

        Task<MaintenanceRequestViewModel> UpdateMaintenanceRequestAsync(ClaimsPrincipal user, MaintenanceRequestViewModel maintenanceRequest);
        Task<MaintenanceEntryViewModel> UpdateMaintenanceEntryAsync(ClaimsPrincipal user, MaintenanceEntryViewModel viewModel);

        Task<MaintenanceEntryViewModel> AddMaintenanceEntryAsync(ClaimsPrincipal user, MaintenanceEntryViewModel maintenanceEntry);

        Task<ICollection<MaintenanceRequestViewModel>> GetMaintenanceRequests(ClaimsPrincipal user);

        Task ArchiveMaintenanceRequest(ClaimsPrincipal user, Guid maintenanceRequestId);
    }
}