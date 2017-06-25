namespace Model.Entities
{
    using System;
    using System.Collections.Generic;
    using Validation;

    public class ChecklistInstance : BaseModel
    {
        public ChecklistInstance()
        {
            
        }

        public ChecklistInstance(Checklist checklist)
        {
            IsPropertyMandatory = checklist.IsPropertyMandatory;
            Image = checklist.Image;
            ChecklistId = checklist.Id;
            Name = checklist.Name;
        }

        public bool IsArchived { get; set; }

        public bool IsAvailableDownstream { get; set; }

        public bool IsPropertyMandatory { get; set; }

        public Portfolio Portfolio { get; set; }

        [RequiredGuid]
        public Guid PortfolioId { get; set; }

        public PropertyDetails PropertyDetails { get; set; }

        public Guid? PropertyDetailsId { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }
        
        [RequiredGuid]
        public Guid ChecklistId { get; set; }
        
        public Checklist Checklist { get; set; }

        public ICollection<ChecklistItemInstance> ChecklistItems { get; set; }

        public string Name { get; set; }
    }
}