namespace Model.Entities
{
    using System;

    public class TenantTenancy : BaseModel
    {
        public Guid TenantId { get; set; }

        public Tenant Tenant { get; set; }

        public Guid TenancyId { get; set; }

        public Tenancy Tenancy { get; set; }
    }
}