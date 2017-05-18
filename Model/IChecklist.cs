namespace Model
{
    using System;
    using System.Collections.Generic;

    public interface IChecklist<T> where T : IChecklistItem
    {
        bool IsPropertyMandatory { get; set; }

        bool IsAvailableDownstream { get; set; }

        string Image { get; set; }

        string Name { get; set; }

        ICollection<T> ChecklistItems { get; set; }
        
        Guid UserId { get; set; }
    }
}