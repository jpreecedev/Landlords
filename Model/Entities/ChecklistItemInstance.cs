namespace Model.Entities
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
            Order = checklistItem.Order;
            Template = checklistItem.Template;
            Payload = checklistItem.Payload;
        }
        
        public string DisplayText { get; set; }

        public string Key { get; set; }

        public bool IsExpanded { get; set; }

        public string Template { get; set; }

        public bool IsCompleted { get; set; }

        public int Order { get; set; }

        public string Payload { get; set; }

        [RequiredGuid]
        public Guid ChecklistInstanceId { get; set; }
    }
}