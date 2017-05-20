namespace Model
{
    using System;
    using System.Collections.Generic;
    using Database;
    using Validation;

    public class ChecklistInstance : BaseModel, IChecklist<IChecklistItem>
    {
        public bool IsArchived { get; set; }

        public bool IsAvailableDownstream { get; set; }

        public bool IsPropertyMandatory { get; set; }

        public string Image { get; set; }

        [ValidateGuid]
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }

        [ValidateGuid]
        public Guid ChecklistId { get; set; }
        
        public Checklist Checklist { get; set; }

        public ICollection<IChecklistItem> ChecklistItems { get; set; }

        public string Name { get; set; }
    }
}