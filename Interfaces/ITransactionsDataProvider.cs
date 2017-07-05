namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;

    public interface ITransactionsDataProvider
    {
        Task<AccountTransactionsViewModel> GetTransactionsAsync(Guid portfolioId, Guid accountId);
        Task<ICollection<TransactionViewModel>> ProcessImportedTransactionsAsync(ICollection<TransactionViewModel> transactions);
        Task UpdateTransactionCategoryAsync(Guid portfolioId, Guid accountId, Guid transactionId, string category);
    }
}