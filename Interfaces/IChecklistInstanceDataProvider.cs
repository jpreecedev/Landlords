namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;

    public interface IChecklistInstanceDataProvider
    {
        Task<ChecklistViewModel> CreateChecklistInstanceAsync(Guid userId, Guid checklistId);
        Task DeleteInstanceAsync(Guid userId, Guid checklistId);
        Task ArchiveChecklistInstanceAsync(Guid userId, Guid checklistId);
        Task<ICollection<ChecklistViewModel>> GetArchivedChecklistInstancesAsync(Guid userId);
    }
}