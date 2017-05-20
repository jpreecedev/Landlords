namespace Model
{
    using System;
    using Validation;

    public class ChecklistItemInstance : BaseModel
    {
        public ChecklistItemInstance()
        {

        }

        public ChecklistItemInstance(ChecklistItem checklistItem)
        {
            if (checklistItem == null)
            {
                return;
            }
            
            DisplayText = checklistItem.DisplayText;
            Key = checklistItem.Key;
            IsExpanded = checklistItem.IsExpanded;
            Template = checklistItem.Template;
        }
        
        public string DisplayText { get; set; }

        public string Key { get; set; }

        public bool IsExpanded { get; set; }

        public string Template { get; set; }

        public bool IsCompleted { get; set; }

        [ValidateGuid]
        public Guid ChecklistInstanceId { get; set; }
    }
}