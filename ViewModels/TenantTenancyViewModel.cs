namespace Landlords.ViewModels
{
    using System;

    public class TenantTenancyViewModel
    {
        public Guid TenancyId { get; set; }

        public string LeadTenant { get; set; }

        public string TenantPhoneNumber { get; set; }

        public string TenantSecondaryPhoneNumber { get; set; }

        public string TenantEmailAddress { get; set; }

        public string PropertyReference { get; set; }

        public DateTime TenancyEndDate { get; set; }
    }
}