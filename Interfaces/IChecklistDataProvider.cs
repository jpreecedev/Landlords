namespace Landlords.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using ViewModels;

    public interface IChecklistDataProvider
    {
        Task<ChecklistOverviewViewModel> GetChecklistOverviewAsync(Guid portfolioId, Guid agencyId, bool includeArchived);
    }
}