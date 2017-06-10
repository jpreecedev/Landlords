namespace Landlords.DataProviders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Database;
    using Interfaces;
    using Landlords.ViewModels;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;

    public class TransactionsDataProvider : BaseDataProvider, ITransactionsDataProvider
    {
        public TransactionsDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task<ICollection<TransactionViewModel>> GetTransactionsAsync(Guid portfolioId, Guid accountId)
        {
            return await Context.Transactions.AsNoTracking()
                .Join(Context.Accounts, transaction => transaction.AccountId, account => account.Id, (transaction, account) => new {Transaction = transaction, Account = account})
                .Where(c => c.Account.Id == accountId && c.Account.PortfolioId == portfolioId && !c.Account.IsDeleted && !c.Transaction.IsDeleted)
                .Select(c => new TransactionViewModel(c.Transaction))
                .OrderByDescending(c => c.Date)
                .ToListAsync();
        }
    }
}