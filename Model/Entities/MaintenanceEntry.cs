namespace Model.Entities
{
    using System;
    using Database;
    using Validation;

    public class MaintenanceEntry : BaseModel
    {
        public string Description { get; set; }

        public string Status { get; set; }

        [RequiredGuid]
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }

        public MaintenanceRequest MaintenanceRequest { get; set; }

        public Guid MaintenanceRequestId { get; set; }
    }
}