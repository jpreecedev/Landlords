namespace Landlords.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Model.Entities;
    using Model.Validation;

    public class ShortlistedPropertyViewModel
    {
        public ShortlistedPropertyViewModel()
        {
        }

        public ShortlistedPropertyViewModel(ShortlistedProperty shortlistedProperty)
        {
            if (shortlistedProperty == null)
            {
                return;
            }

            ShortlistedPropertyId = shortlistedProperty.Id;
            Reference = shortlistedProperty.Reference;
            Address = shortlistedProperty.Address;
            PricePaid = shortlistedProperty.PricePaid;
            Deposit = shortlistedProperty.Deposit;
            Fees = shortlistedProperty.Fees;
            LettableUnits = shortlistedProperty.LettableUnits;
            ExpectedRentalIncome = shortlistedProperty.ExpectedRentalIncome;
            MortgageInterestRate = shortlistedProperty.MortgageInterestRate;
            ManagementCost = shortlistedProperty.ManagementCost;
            RepairsContingency = shortlistedProperty.RepairsContingency;
            ServiceCharge = shortlistedProperty.ServiceCharge;
            Insurance = shortlistedProperty.Insurance;
        }
        
        public Guid? ShortlistedPropertyId { get; set; }

        [Required, MaxLength(255)]
        public string Reference { get; set; }

        [Required, MaxLength(255)]
        public string Address { get; set; }

        [Required, Range(10000, 10000000)]
        public decimal PricePaid { get; set; }

        [Required, Range(1000, 10000000)]
        public decimal Deposit { get; set; }

        [Required, Range(0, 10000000)]
        public decimal Fees { get; set; }

        [Required, Range(0, 1000)]
        public int LettableUnits { get; set; }

        [Required, Range(100, 10000000)]
        public decimal ExpectedRentalIncome { get; set; }

        [Required, Range(0.1, 20)]
        public decimal MortgageInterestRate { get; set; }

        [Required, Range(0, 100)]
        public decimal ManagementCost { get; set; }

        [Required, Range(0, 100)]
        public decimal RepairsContingency { get; set; }

        [Required, Range(0, 100)]
        public decimal ServiceCharge { get; set; }

        [Required, Range(0, 10000000)]
        public decimal Insurance { get; set; }
    }
}