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

        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }

        public Guid PortfolioId { get; set; }

        public Portfolio Portfolio { get; set; }

        public ICollection<MaintenanceEntry> Entries { get; set; }
    }
}