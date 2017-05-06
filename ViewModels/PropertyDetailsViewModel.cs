namespace Landlords.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Model;
    using Model.DataTypes;
    using Model.Validation;
    using System.Collections.Generic;

    //TODO: Use automapper
    public class PropertyDetailsViewModel : IPropertyDetails
    {
        public PropertyDetailsViewModel()
        {
        }
        
        public PropertyDetailsViewModel(PropertyDetails propertyDetails)
        {
            if (propertyDetails == null) throw new ArgumentNullException(nameof(propertyDetails));

            Id = propertyDetails.Id;
            PortfolioId = propertyDetails.PortfolioId;
            ConstructionDate = propertyDetails.ConstructionDate;
            Furnishing = propertyDetails.Furnishing;
            Bedrooms = propertyDetails.Bedrooms;
            IsAvailableForLetting = propertyDetails.IsAvailableForLetting;
            PaymentTerm = propertyDetails.PaymentTerm;
            PropertyCountry = propertyDetails.PropertyCountry;
            PropertyCountyOrRegion = propertyDetails.PropertyCountyOrRegion;
            PropertyPostcode = propertyDetails.PropertyPostcode;
            PropertyStreetAddress = propertyDetails.PropertyStreetAddress;
            PropertyTownOrCity = propertyDetails.PropertyTownOrCity;
            PropertyType = propertyDetails.PropertyType;
            PurchaseDate = propertyDetails.PurchaseDate;
            PurchasePrice = propertyDetails.PurchasePrice;
            Reference = propertyDetails.Reference;
            SellingDate = propertyDetails.SellingDate;
            TargetRent = propertyDetails.TargetRent;
            SellingPrice = propertyDetails.SellingPrice;
            MortgageAmount = propertyDetails.MortgageAmount;
            InterestRate = propertyDetails.InterestRate;
            CurrentDealExpirationDate = propertyDetails.CurrentDealExpirationDate;
            MortgageProvider = propertyDetails.MortgageProvider;
        }

        public PropertyDetailsViewModel(Guid id, Guid userId)
        {
            Id = id;
            PortfolioId = PortfolioId;
        }

        public DateTime? ConstructionDate { get; set; }

        [Required]
        [MaxLength(255)]
        public string Furnishing { get; set; }

        [Required, Range(0, 20)]
        public int Bedrooms { get; set; }

        [Required]
        public bool IsAvailableForLetting { get; set; }

        [MaxLength(255)]
        public string PaymentTerm { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [MaxLength(255)]
        public string PropertyCountry { get; set; }

        [MaxLength(255)]
        public string PropertyCountyOrRegion { get; set; }

        [MaxLength(255)]
        public string PropertyPostcode { get; set; }

        [Required(ErrorMessage = "Street address is required")]
        [MaxLength(255)]
        public string PropertyStreetAddress { get; set; }

        [MaxLength(255)]
        public string PropertyTownOrCity { get; set; }

        [Required(ErrorMessage = "Property type is required")]
        [MaxLength(255)]
        public string PropertyType { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public decimal? PurchasePrice { get; set; }

        public decimal? MortgageAmount { get; set; }

        public double? InterestRate { get; set; }

        public string MortgageProvider { get; set; }

        public DateTime? CurrentDealExpirationDate { get; set; }

        [MaxLength(255)]
        public string Reference { get; set; }

        public DateTime? SellingDate { get; set; }

        public decimal? SellingPrice { get; set; }

        public decimal? TargetRent { get; set; }

        public string[] DefaultPropertyTypes { get; } = Model.DataTypes.PropertyType.GetDefaultPropertyTypes();

        public string[] DefaultPaymentTerms { get; } = PaymentTerms.GetDefaultPaymentTerms();

        public string[] DefaultFurnishings { get; } = PropertyFurnishing.GetDefaultFurnishings();

        public string[] DefaultCountries { get; } = Countries.GetDefaultCountries();

        public string[] DefaultMortgageProviders { get; } = MortgageProviders.GetDefaultMortgageProviders();

        public PropertyImageViewModel LeadImage { get; set; }

        public ICollection<PropertyImageViewModel> PropertyImages { get; set; }

        [ValidateGuid]
        public Guid Id { get; set; }
        
        public decimal? MonthlyPayment { get; set; }

        [ValidateGuid]
        public Guid PortfolioId { get; set; }

        public Portfolio Portfolio { get; set; }
    }
}