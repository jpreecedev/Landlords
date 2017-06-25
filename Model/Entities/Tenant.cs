namespace Model.Entities
{
    using Model.Validation;
    using System;
    using System.Collections.Generic;

    public class Tenant : BaseModel
    {
        public string Title { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        [LLDate]
        public DateTime DateOfBirth { get; set; }

        public string MainContactNumber { get; set; }

        public string SecondaryContactNumber { get; set; }

        public string EmailAddress { get; set; }

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