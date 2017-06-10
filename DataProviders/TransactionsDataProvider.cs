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
    using Model.Entities;

    public class TransactionsDataProvider : BaseDataProvider, ITransactionsDataProvider
    {
        public TransactionsDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task<ICollection<TransactionViewModel>> GetTransactionsAsync(Guid portfolioId, Guid accountId)
        {
            return await Context.Transactions.AsNoTracking()
                .Join(Context.Accounts, transaction => transaction.AccountId, account => account.Id, (transaction, account) => new { Transaction = transaction, Account = account })
                .Where(c => c.Account.Id == accountId && c.Account.PortfolioId == portfolioId && !c.Account.IsDeleted && !c.Transaction.IsDeleted)
                .Select(c => new TransactionViewModel(c.Transaction))
                .OrderByDescending(c => c.Date)
                .ToListAsync();
        }

        public async Task<ICollection<TransactionViewModel>> ProcessImportedTransactionsAsync(ICollection<TransactionViewModel> transactions)
        {
            var newTransactions = new List<Transaction>();

            foreach (var transactionViewModel in transactions)
            {
                var existingEntity = await Context.Transactions.Where(c => c.AccountId == transactionViewModel.AccountId &&
                        c.Balance == transactionViewModel.Balance &&
                        c.Date == transactionViewModel.Date &&
                        c.DestinationAccountNumber == transactionViewModel.DestinationAccountNumber &&
                        c.DestinationSortCode == transactionViewModel.DestinationSortCode &&
                        c.In == transactionViewModel.In &&
                        c.Out == transactionViewModel.Out &&
                        c.PaymentType == transactionViewModel.PaymentType &&
                        c.Reference == transactionViewModel.Reference)
                    .FirstOrDefaultAsync();

                if (existingEntity == null)
                {
                    newTransactions.Add(new Transaction
                    {
                        AccountId = transactionViewModel.AccountId,
                        Balance = transactionViewModel.Balance,
                        Date = transactionViewModel.Date,
                        DestinationAccountNumber = transactionViewModel.DestinationAccountNumber,
                        DestinationSortCode = transactionViewModel.DestinationSortCode,
                        In = transactionViewModel.In,
                        Out = transactionViewModel.Out,
                        PaymentType = transactionViewModel.PaymentType,
                        Reference = transactionViewModel.Reference
                    });
                }
            }

            await Context.Transactions.AddRangeAsync(newTransactions);
            await Context.SaveChangesAsync();

            return newTransactions.Select(c => new TransactionViewModel(c)).ToList();
        }
    }
}