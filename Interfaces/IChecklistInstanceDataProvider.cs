namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;

    public interface IChecklistInstanceDataProvider
    {
        Task<ChecklistViewModel> CreateChecklistInstanceAsync(Guid portfolioId, Guid checklistId, Guid? propertyDetailsId);
        Task DeleteInstanceAsync(Guid portfolioId, Guid checklistId);
        Task ArchiveChecklistInstanceAsync(Guid portfolioId, Guid checklistId);
        Task<ICollection<ChecklistViewModel>> GetArchivedChecklistInstancesAsync(Guid portfolioId);
        Task<ChecklistViewModel> GetChecklistByIdAsync(Guid portfolioId, Guid checklistId);
    }
}