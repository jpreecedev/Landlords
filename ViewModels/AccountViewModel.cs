namespace Landlords.ViewModels
{
    using System;
    using Model.Validation;
    using Model.Entities;
    using System.ComponentModel.DataAnnotations;

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

        [RequiredGuid]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string SortCode { get; set; }

        [Required]
        public string ProviderName { get; set; }

        [Required]
        public string Type { get; set; }

        [RequiredGuid]
        public Guid PortfolioId { get; set; }

        [LLDate]
        public DateTime Opened { get; set; }

        public decimal OpeningBalance { get; set; }
        
        public string[] DefaultAccountTypes { get; } = Model.DataTypes.AccountTypes.GetDefaultAccountTypes();

        public string[] DefaultAccountProviders { get; } = Model.DataTypes.AccountProviders.GetDefaultAccountProviders();
    }
}