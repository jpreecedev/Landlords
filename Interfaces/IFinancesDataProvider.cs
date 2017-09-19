namespace Landlords.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using ViewModels;

    public interface IFinancesDataProvider
    {
        Task<AccountsOverviewViewModel> GetAccountsOverviewAsync(Guid portfolioId);
        Task<AccountViewModel> GetAccountByIdAsync(Guid portfolioId, Guid accountId);
        Task<AccountViewModel> NewAsync(Guid portfolioId);
        Task UpdateAsync(Guid portfolioId, AccountViewModel account);
        Task DeleteAccountAsync(Guid portfolioId, Guid accountId);
    }
}