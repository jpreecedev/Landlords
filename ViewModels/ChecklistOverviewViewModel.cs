namespace Landlords.ViewModels
{
    using System.Collections.Generic;

    public class ChecklistOverviewViewModel
    {
        public ICollection<ChecklistViewModel> UserChecklists { get; set; }
                                    
        public ICollection<ChecklistViewModel> AgencyChecklists { get; set; }
                                    
        public ICollection<ChecklistViewModel> AdminChecklists { get; set; }
    }
}