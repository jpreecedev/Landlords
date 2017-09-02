namespace Landlords.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Model.DataTypes;
    using Model.Entities;
    using Model.Validation;

    public class MaintenanceRequestViewModel
    {
        public MaintenanceRequestViewModel()
        {
        }

        public MaintenanceRequestViewModel(MaintenanceRequest maintenanceRequest, ICollection<MaintenanceEntry> maintenanceEntries)
        {
            if (maintenanceRequest == null) return;

            Id = maintenanceRequest.Id;
            Title = maintenanceRequest.Title;
            Description = maintenanceRequest.Description;
            Severity = maintenanceRequest.Severity;

            if (maintenanceEntries != null)
            {
                Entries = maintenanceEntries.Select(c => new MaintenanceEntryViewModel(c)).ToList();
            }
        }

        [RequiredGuid]
        public Guid Id { get; set; }

        [Display(Name = "Title")]
        [Required, MinLength(2)]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [Required, MinLength(2)]
        public string Description { get; set; }

        [Required]
        public string Severity { get; set; }

        public ICollection<MaintenanceEntryViewModel> Entries { get; set; }

        public LLDataType[] DefaultMaintenanceSeverities { get; } = MaintenanceSeverity.GetDefaultMaintenanceSeverities();
    }
}