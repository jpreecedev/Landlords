namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;

    public interface IChecklistDataProvider
    {
        Task<ChecklistOverviewViewModel> GetChecklistOverviewAsync(Guid userId, Guid agencyId, bool includeArchived);
        Task<ChecklistViewModel> CreateChecklistInstanceAsync(Guid userId, Guid checklistId);
        Task<ChecklistViewModel> GetChecklistByIdAsync(Guid userId, Guid checklistId);
        Task DeleteInstanceAsync(Guid userId, Guid checklistId);
        Task ArchiveChecklistInstanceAsync(Guid userId, Guid checklistId);
        Task<ICollection<ChecklistViewModel>> GetArchivedChecklistInstancesAsync(Guid userId);
    }
}