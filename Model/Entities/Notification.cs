namespace Model.Entities
{
    using System;

    public class Notification : BaseModel
    {
        public Portfolio Portfolio { get; set; }
        
        public Guid PortfolioId { get; set; }
        
        public PropertyDetails PropertyDetails { get; set; }

        public Guid? PropertyDetailsId { get; set; }

        public Tenancy Tenancy { get; set; }
        
        public Guid? TenancyId { get; set; }

        public Guid? TenantId { get; set; }

        public Tenant Tenant { get; set; }

        public Guid? ShortlistedPropertyId { get; set; }

        public ShortlistedProperty ShortlistedProperty { get; set; }
        
        public string Type { get; set; }

        public string Message { get; set; }
    }
}