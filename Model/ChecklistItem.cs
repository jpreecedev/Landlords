namespace Model
{
    using System;
    using Validation;

    public class ChecklistItem : BaseModel
    {
        [ValidateGuid]
        public Guid ChecklistId { get; set; }

        public string DisplayText { get; set; }

        public string Key { get; set; }

        public bool IsExpanded { get; set; }

        public string Template { get; set; }

        public int Order { get; set; }
    }
}