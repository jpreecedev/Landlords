namespace Landlords.DataProviders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Database;
    using Interfaces;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using ViewModels;
    using Model;
    using Model.DataTypes;
    using Model.Entities;

    public class AccountsDataProvider : BaseDataProvider, IAccountsDataProvider
    {
        public AccountsDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task<AccountsOverviewViewModel> GetAccountsOverviewAsync(Guid portfolioId)
        {
            var accounts = await Context.Accounts.Where(c => c.PortfolioId == portfolioId && !c.IsDeleted)
                .Select(c => new AccountViewModel
                {
                    Id = c.Id,
                    PortfolioId = c.PortfolioId,
                    Name = c.Name,
                    Type = c.Type,
                    ProviderName = c.ProviderName,
                    Number = c.Number,
                    SortCode = c.SortCode,
                    Opened = c.Opened,
                    OpeningBalance = c.OpeningBalance
                })
                .ToListAsync();

            return new AccountsOverviewViewModel(accounts);
        }

        public async Task<AccountViewModel> GetAccountByIdAsync(Guid portfolioId, Guid accountId)
        {
            return await Context.Accounts.Where(c => !c.IsDeleted && c.PortfolioId == portfolioId && c.Id == accountId)
                .Select(c => new AccountViewModel
                {
                    Id = c.Id,
                    PortfolioId = c.PortfolioId,
                    Name = c.Name,
                    Type = c.Type,
                    ProviderName = c.ProviderName,
                    Number = c.Number,
                    SortCode = c.SortCode,
                    Opened = c.Opened,
                    OpeningBalance = c.OpeningBalance
                })
                .SingleOrDefaultAsync();
        }

        public async Task<AccountViewModel> NewAsync(Guid portfolioId)
        {
            var entity = new Account
            {
                Created = DateTime.Now,
                PortfolioId = portfolioId,
                Name = "New bank account",
                Type = AccountTypes.Bank,
                Opened = DateTime.Now
            };

            await Context.Accounts.AddAsync(entity);
            await Context.SaveChangesAsync();

            return new AccountViewModel(entity);
        }

        public async Task UpdateAsync(Guid portfolioId, AccountViewModel account)
        {
            var existingEntity = await Context.Accounts.Where(c => c.PortfolioId == portfolioId && c.Id == account.Id && !c.IsDeleted).FirstOrDefaultAsync();
            if (existingEntity != null)
            {
                existingEntity.MapFrom(account);
                Context.Accounts.Update(existingEntity);
                await Context.SaveChangesAsync();
            }
        }
    }
}