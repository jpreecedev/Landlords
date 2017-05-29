namespace Landlords.ViewModels
{
    using System;
    using Model;

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
            Id = checklistItem.Id;
        }

        public string DisplayText { get; set; }
        public string Key { get; set; }
        public bool IsExpanded { get; set; }
        public string Template { get; set; }
        public bool IsCompleted { get; set; }
        public Guid Id { get; set; }
    }
}