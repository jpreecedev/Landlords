namespace Landlords.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Model;

    public class ChecklistViewModel : IChecklist<ChecklistItemViewModel>
    {
        public ChecklistViewModel()
        {
            
        }

        public ChecklistViewModel(Checklist checklist, ICollection<ChecklistItem> checklistItems)
        {
            if (checklist == null)
            {
                return;
            }

            Id = checklist.Id;
            IsPropertyMandatory = checklist.IsPropertyMandatory;
            IsAvailableDownstream = checklist.IsAvailableDownstream;
            Image = checklist.Image;
            Name = checklist.Name;
            UserId = checklist.UserId;
            ChecklistItems = checklistItems.Select(c => new ChecklistItemViewModel(c)).ToList();
        }

        public bool IsPropertyMandatory { get; set; }
        public bool IsAvailableDownstream { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public ICollection<ChecklistItemViewModel> ChecklistItems { get; set; }
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}