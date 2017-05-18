namespace Model
{
    using System;
    using System.Collections.Generic;
    using Validation;

    public class Checklist : BaseModel, IChecklist<IChecklistItem>
    {
        public bool IsPropertyMandatory { get; set; }

        public bool IsAvailableDownstream { get; set; }

        public string Image { get; set; }

        public ICollection<IChecklistItem> ChecklistItems { get; set; }

        public string Name { get; set; }

        [ValidateGuid]
        public Guid UserId { get; set; }
    }
}