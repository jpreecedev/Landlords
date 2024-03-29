namespace Landlords.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Model.Entities;

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
            IsArchived = maintenanceRequest.IsArchived;
            PropertyDetails = new PropertyBasicDetailsViewModel(maintenanceRequest.PropertyDetails);

            if (maintenanceEntries != null)
            {
                Entries = maintenanceEntries.Select(c => new MaintenanceEntryViewModel(c)).ToList();
            }
        }
        
        public Guid Id { get; set; }

        [Display(Name = "Title")]
        [Required, MinLength(2)]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [Required, MinLength(2)]
        public string Description { get; set; }

        [Required]
        public string Severity { get; set; }

        public bool IsArchived { get; set; }

        public string Text
        {
            get
            {
                if (PropertyDetails == null || string.IsNullOrEmpty(PropertyDetails.PropertyStreetAddress))
                {
                    return string.Format("{0} ({1})", Title, Severity);
                }
                return string.Format("{0} ({1}, {2})", Title, Severity, PropertyDetails.PropertyStreetAddress);
            }
        }

        public PropertyBasicDetailsViewModel PropertyDetails { get; set; }

        public ICollection<MaintenanceEntryViewModel> Entries { get; set; }
    }
}