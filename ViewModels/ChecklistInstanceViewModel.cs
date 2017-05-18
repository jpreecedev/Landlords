namespace Landlords.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Model;

    public class ChecklistInstanceViewModel : IChecklist<ChecklistItemViewModel>
    {
        public ChecklistInstanceViewModel()
        {
            
        }

        public ChecklistInstanceViewModel(ChecklistInstance instance)
        {
            if (instance == null)
            {
                return;
            }

            IsPropertyMandatory = instance.IsPropertyMandatory;
            IsAvailableDownstream = instance.IsAvailableDownstream;
            Image = instance.Image;
            Name = instance.Name;
            ChecklistItems = instance.ChecklistItems.Select(c => new ChecklistItemViewModel(c)).ToList();
            UserId = instance.UserId;
        }

        public bool IsPropertyMandatory { get; set; }
        public bool IsAvailableDownstream { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public ICollection<ChecklistItemViewModel> ChecklistItems { get; set; }
        public Guid UserId { get; set; }
    }
}