namespace Landlords.ViewModels
{
    using System.Collections.Generic;
    using Model.DataTypes;

    public class MaintenanceRequestOverviewViewModel
    {
        public ICollection<PropertyBasicDetailsViewModel> Properties { get; set; }
        public LLDataType[] DefaultMaintenanceSeverities { get; } = MaintenanceSeverity.GetDefaultMaintenanceSeverities();
        public LLDataType[] DefaultMaintenanceStatuses { get; } = MaintenanceStatus.GetDefaultMaintenanceStatuses();
    }
}