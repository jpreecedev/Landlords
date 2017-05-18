namespace Landlords.ViewModels
{
    using System.Collections.Generic;

    public class ChecklistOverviewViewModel
    {
        public ICollection<ChecklistInstanceViewModel> UserChecklists { get; set; }

        public ICollection<ChecklistInstanceViewModel> AgencyChecklists { get; set; }

        public ICollection<ChecklistInstanceViewModel> AdminChecklists { get; set; }
    }
}