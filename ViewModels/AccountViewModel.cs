namespace Landlords.ViewModels
{
    using System;
    using Model;
    using Model.Validation;

    public class AccountViewModel
    {
        public AccountViewModel()
        {
        }

        public AccountViewModel(Account account)
        {
            if (account == null) return;

            Id = account.Id;
            Name = account.Name;
            Number = account.Number;
            SortCode = account.SortCode;
            ProviderName = account.ProviderName;
            Type = account.Type;
            PortfolioId = account.PortfolioId;
            Opened = account.Opened;
            OpeningBalance = account.OpeningBalance;
        }

        [ValidateGuid]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public string SortCode { get; set; }

        public string ProviderName { get; set; }

        public string Type { get; set; }

        [ValidateGuid]
        public Guid PortfolioId { get; set; }

        public DateTime Opened { get; set; }

        public decimal OpeningBalance { get; set; }
        
        public string[] DefaultAccountTypes { get; } = Model.DataTypes.AccountTypes.GetDefaultAccountTypes();

        public string[] DefaultAccountProviders { get; } = Model.DataTypes.AccountProviders.GetDefaultAccountProviders();
    }
}