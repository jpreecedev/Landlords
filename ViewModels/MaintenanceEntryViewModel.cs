namespace Landlords.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Model.Database;
    using Model.DataTypes;
    using Model.Entities;
    using Model.Validation;

    public class MaintenanceEntryViewModel
    {
        public MaintenanceEntryViewModel()
        {
        }

        public MaintenanceEntryViewModel(MaintenanceEntry maintenanceEntry)
        {
            if (maintenanceEntry == null) return;

            Id = maintenanceEntry.Id;
            Description = maintenanceEntry.Description;
            Status = maintenanceEntry.Status;
        }

        [RequiredGuid]
        public Guid Id { get; set; }

        [RequiredGuid]
        public Guid MaintenanceRequestId { get; set; }

        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Display(Name = "Title")]
        [Required, MinLength(2)]
        public string Description { get; set; }

        [Required, MinLength(2)]
        public string Status { get; set; }

        public LLDataType[] DefaultMaintenanceStatuses { get; } = MaintenanceStatus.GetDefaultMaintenanceStatuses();
    }
}