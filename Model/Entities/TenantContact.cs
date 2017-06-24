namespace Model.Entities
{
    using System;
    using Validation;

    public class TenantContact : BaseModel
    {
        public string Name { get; set; }

        public string MainContactNumber { get; set; }

        public string SecondaryContactNumber { get; set; }

        public string Relationship { get; set; }

        public string Comments { get; set; }

        [ValidateGuid]
        public Guid TenantId { get; set; }

        public Tenant Tenant { get; set; }
    }
}