namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;

    public interface IChecklistInstanceDataProvider
    {
        Task<ChecklistViewModel> CreateChecklistInstanceAsync(Guid userId, Guid checklistId, Guid? portfolioId, Guid? propertyDetailsId);
        Task DeleteInstanceAsync(Guid userId, Guid checklistId);
        Task ArchiveChecklistInstanceAsync(Guid userId, Guid checklistId);
        Task<ICollection<ChecklistViewModel>> GetArchivedChecklistInstancesAsync(Guid userId);
        Task<ChecklistViewModel> GetChecklistByIdAsync(Guid userId, Guid checklistId);
    }
}