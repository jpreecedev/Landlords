namespace Landlords.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using ViewModels;

    public interface IJourneyDataProvider
    {
        Task CreateTenancyAsync(Guid portfolioId, StartTenancyJourneyViewModel viewModel);
        Task UpdateTenancyAsync(Guid portfolioId, StartTenancyJourneyViewModel viewModel);
    }
}