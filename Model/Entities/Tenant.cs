namespace Model.Entities
{
    using Model.Validation;
    using System;
    using System.Collections.Generic;
    using Database;

    public class Tenant : BaseModel
    {
        [RequiredGuid]
        public Guid ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        
        [LLDate]
        public DateTime DateOfBirth { get; set; }

        public string Occupation { get; set; }

        public string EmploymentType { get; set; }

        public string CompanyName { get; set; }

        public string WorkContactNumber { get; set; }

        public string WorkAddress { get; set; }

        public string DrivingLicenseReference { get; set; }

        public string PassportReference { get; set; }

        public bool IsSmoker { get; set; }

        public bool HasPets { get; set; }

        public string AdditionalInformation { get; set; }

        public bool IsLeadTenant { get; set; }

        public bool IsAdult { get; set; }

        public ICollection<TenantAddress> Addresses { get; set; }

        public ICollection<TenantContact> Contacts { get; set; }
    }
}