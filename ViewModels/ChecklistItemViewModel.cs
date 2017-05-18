namespace Landlords.ViewModels
{
    using Model;

    public class ChecklistItemViewModel : IChecklistItem
    {
        public ChecklistItemViewModel()
        {
        }

        public ChecklistItemViewModel(IChecklistItem checklistItem)
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
        }

        public string DisplayText { get; set; }
        public string Key { get; set; }
        public bool IsExpanded { get; set; }
        public string Template { get; set; }
        public bool IsCompleted { get; set; }
    }
}