namespace Model.Entities
{
    using System;
    using System.Collections.Generic;
    using Validation;

    public class Tenant : BaseModel
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ICollection<TenantAddress> Addresses { get; set; }

        public string MainContactNumber { get; set; }

        public string SecondaryContactNumber { get; set; }

        public string EmailAddress { get; set; }

        [ValidateGuid]
        public Guid PortfolioId { get; set; }

        public Portfolio Portfolio { get; set; }
    }
}