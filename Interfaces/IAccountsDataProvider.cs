namespace Landlords.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using ViewModels;

    public interface IAccountsDataProvider
    {
        Task<AccountsOverviewViewModel> GetAccountsOverviewAsync(Guid portfolioId);
        Task<AccountViewModel> GetAccountByIdAsync(Guid portfolioId, Guid accountId);
        Task<AccountViewModel> NewAsync(Guid portfolioId);
        Task UpdateAsync(Guid portfolioId, AccountViewModel account);
    }
}