namespace Model.Entities
{
    using System;
    using Validation;

    public class TenantAddress : BaseModel
    {
        public string Street { get; set; }

        public string TownOrCity { get; set; }

        public string CountyOrRegion { get; set; }

        public string Postcode { get; set; }

        public string Country { get; set; }

        public Tenant Tenant { get; set; }

        [ValidateGuid]
        public Guid TenantId { get; set; }

        public int YearsAtAddress { get; set; }

        public int MonthsAtAddress { get; set; }
    }
}