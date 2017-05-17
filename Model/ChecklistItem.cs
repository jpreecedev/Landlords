namespace Model
{
    using System;
    using Validation;

    public class ChecklistItem : BaseModel, IChecklistItem
    {
        [ValidateGuid]
        public Guid ChecklistId { get; set; }

        public Checklist Checklist { get; set; }

        public string DisplayText { get; set; }

        public string Key { get; set; }

        public bool IsExpanded { get; set; }

        public string Template { get; set; }

        public bool IsCompleted { get; set; }
    }
}