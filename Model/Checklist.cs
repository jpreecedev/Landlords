namespace Model
{
    using System;
    using System.Collections.Generic;
    using Database;
    using Validation;

    public class Checklist : BaseModel
    {
        public bool IsPropertyMandatory { get; set; }

        public bool IsAvailableDownstream { get; set; }

        public string Image { get; set; }

        public ICollection<ChecklistItem> ChecklistItems { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ApplicationUser User { get; set; }

        [ValidateGuid]
        public Guid UserId { get; set; }
    }
}