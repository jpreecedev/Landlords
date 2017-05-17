namespace Model
{
    using System;
    using Validation;

    public class ChecklistItemInstance : BaseModel, IChecklistItem
    {
        [ValidateGuid]
        public Guid ChecklistInstanceId { get; set; }
        
        public string DisplayText { get; set; }

        public string Key { get; set; }

        public bool IsExpanded { get; set; }

        public string Template { get; set; }

        public bool IsCompleted { get; set; }
    }
}