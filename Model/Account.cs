namespace Model
{
    using System;
    using Validation;

    public class Account : BaseModel
    {
        public string Name { get; set; }

        public string Number { get; set; }

        public string SortCode { get; set; }

        public string ProviderName { get; set; }

        public string Type { get; set; }

        public DateTime Opened { get; set; }

        public decimal OpeningBalance { get; set; }

        [ValidateGuid]
        public Guid PortfolioId { get; set; }

        public Portfolio Portfolio { get; set; }
    }
}