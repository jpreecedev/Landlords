namespace Landlords.ViewModels
{
    using System.Collections.Generic;

    public class ChecklistOverviewViewModel
    {
        public ICollection<ChecklistViewModel> Checklists { get; set; }
                                    
        public ICollection<ChecklistViewModel> AvailableChecklists { get; set; }
    }
}