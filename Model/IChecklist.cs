namespace Model
{
    using System.Collections.Generic;

    public interface IChecklist
    {
        bool IsPropertyMandatory { get; set; }

        bool IsAvailableDownstream { get; set; }

        string Image { get; set; }

        ICollection<IChecklistItem> ChecklistItems { get; set; }
    }
}