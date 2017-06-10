namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;

    public interface ITransactionsDataProvider
    {
        Task<ICollection<TransactionViewModel>> GetTransactionsAsync(Guid portfolioId, Guid accountId);
    }
}