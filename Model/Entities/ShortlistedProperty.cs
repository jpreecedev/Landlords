namespace Model.Entities
{
    using System;
    using Validation;

    public class ShortlistedProperty : BaseModel
    {
        public string Reference { get; set; }

        public string Address { get; set; }

        public decimal PricePaid { get; set; }

        public decimal Deposit { get; set; }

        public decimal Fees { get; set; }

        public int LettableUnits { get; set; }

        public decimal ExpectedRentalIncome { get; set; }

        public decimal MortgageInterestRate { get; set; }

        public decimal ManagementCost { get; set; }

        public decimal RepairsContingency { get; set; }

        public decimal ServiceCharge { get; set; }

        public decimal Insurance { get; set; }

        [RequiredGuid]
        public Guid PortfolioId { get; set; }

        public Portfolio Portfolio { get; set; }
    }
}