namespace Model
{
    using System;
    using Validation;
    
    public class PropertyDetails : BaseModel, IPropertyDetails
    {
        public string Reference { get; set; }

        public int Bedrooms { get; set; }

        public string Furnishing { get; set; }

        public string PropertyType { get; set; }

        public DateTime? ConstructionDate { get; set; }

        public decimal? TargetRent { get; set; }

        public string PaymentTerm { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public decimal? PurchasePrice { get; set; }

        public DateTime? SellingDate { get; set; }

        public decimal? SellingPrice { get; set; }

        public string PropertyStreetAddress { get; set; }

        public string PropertyTownOrCity { get; set; }

        public string PropertyCountyOrRegion { get; set; }

        public string PropertyPostcode { get; set; }

        public string PropertyCountry { get; set; }

        public bool IsAvailableForLetting { get; set; }

        public decimal? MortgageAmount { get; set; }

        public double? InterestRate { get; set; }

        public string MortgageProvider { get; set; }

        public DateTime? CurrentDealExpirationDate { get; set; }

        public decimal? MonthlyPayment { get; set; }

        [ValidateGuid]
        public Guid PortfolioId { get; set; }

        public Portfolio Portfolio { get; set; }
    }
}