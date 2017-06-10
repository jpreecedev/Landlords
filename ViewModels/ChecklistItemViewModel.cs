namespace Landlords.ViewModels
{
    using System;
    using Model.Entities;

    public class ChecklistItemViewModel
    {
        public ChecklistItemViewModel()
        {
        }

        public ChecklistItemViewModel(ChecklistItem checklistItem)
        {
            if (checklistItem == null)
            {
                return;
            }

            DisplayText = checklistItem.DisplayText;
            Key = checklistItem.Key;
            IsExpanded = checklistItem.IsExpanded;
            Template = checklistItem.Template;
            Order = checklistItem.Order;
            Payload = checklistItem.Payload;
            Id = checklistItem.Id;
        }

        public ChecklistItemViewModel(ChecklistItemInstance checklistItem)
        {
            if (checklistItem == null)
            {
                return;
            }

            DisplayText = checklistItem.DisplayText;
            Key = checklistItem.Key;
            IsExpanded = checklistItem.IsExpanded;
            Template = checklistItem.Template;
            IsCompleted = checklistItem.IsCompleted;
            Order = checklistItem.Order;
            Payload = checklistItem.Payload;
            Id = checklistItem.Id;
        }

        public string DisplayText { get; set; }
        public string Key { get; set; }
        public bool IsExpanded { get; set; }
        public string Template { get; set; }
        public bool IsCompleted { get; set; }
        public int Order { get; set; }
        public string Payload { get; set; }
        public Guid Id { get; set; }
    }
}