namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;

    public interface IChecklistInstanceDataProvider
    {
        Task<ChecklistViewModel> CreateAsync(Guid portfolioId, Guid checklistId, Guid? propertyDetailsId);
        Task DeleteAsync(Guid portfolioId, Guid checklistId);
        Task ArchiveAsync(Guid portfolioId, Guid checklistId);
        Task<ICollection<ChecklistViewModel>> GetArchivedAsync(Guid portfolioId);
        Task<ChecklistViewModel> GetChecklistByIdAsync(Guid portfolioId, Guid checklistId);
        Task UpdateAsync(Guid portfolioId, Guid checklistId, ChecklistInstanceViewModel viewModel);
    }
}