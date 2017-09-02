namespace Landlords.ViewModels
{
    using Model.DataTypes;

    public class MaintenanceRequestOverviewViewModel
    {
        public LLDataType[] DefaultMaintenanceSeverities { get; } = MaintenanceSeverity.GetDefaultMaintenanceSeverities();
    }
}