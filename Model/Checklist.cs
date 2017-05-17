namespace Model
{
    using System.Collections.Generic;

    public class Checklist : BaseModel, IChecklist
    {
        public bool IsPropertyMandatory { get; set; }

        public bool IsAvailableDownstream { get; set; }

        public string Image { get; set; }

        public ICollection<IChecklistItem> ChecklistItems { get; set; }
    }
}