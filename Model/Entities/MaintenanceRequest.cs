namespace Model.Entities
{
    using System;
    using System.Collections.Generic;
    using Database;

    public class MaintenanceRequest : BaseModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Severity { get; set; }

        public bool IsArchived { get; set; }

        public PropertyDetails PropertyDetails { get; set; }

        public Guid PropertyDetailsId { get; set; }

        public ICollection<MaintenanceEntry> Entries { get; set; }
    }
}