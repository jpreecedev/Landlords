namespace Landlords.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Model;
    using Model.DataTypes;
    using Model.Validation;
    using System.Collections.Generic;
    using Interfaces;
    using Model.Entities;

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

        public PropertyDetailsViewModel(Guid id, Guid portfolioId)
        {
            Id = id;
            PortfolioId = portfolioId;
        }

        public DateTime? ConstructionDate { get; set; }

        [Required, MaxLength(255)]
        public string Furnishing { get; set; }

        [Required, Range(0, 20)]
        public int Bedrooms { get; set; }

        [Display(Name = "Is available for letting")]
        [Required]
        public bool IsAvailableForLetting { get; set; }

        [MaxLength(255)]
        public string PaymentTerm { get; set; }

        [Display(Name = "Country")]
        [Required, MaxLength(255)]
        public string PropertyCountry { get; set; }

        [Display(Name = "Country or region")]
        [MaxLength(255)]
        public string PropertyCountyOrRegion { get; set; }

        [Display(Name = "Postcode")]
        [Required, MinLength(5), MaxLength(7)]
        public string PropertyPostcode { get; set; }

        [Display(Name = "Street address")]
        [Required, MinLength(2), MaxLength(255)]
        public string PropertyStreetAddress { get; set; }

        [Display(Name = "Town or city")]
        [Required, MinLength(2), MaxLength(255)]
        public string PropertyTownOrCity { get; set; }
        
        [Display(Name = "Property type")]
        [Required, MaxLength(255)]
        public string PropertyType { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public decimal? PurchasePrice { get; set; }

        public decimal? MortgageAmount { get; set; }

        public double? InterestRate { get; set; }

        public string MortgageProvider { get; set; }

        public DateTime? CurrentDealExpirationDate { get; set; }
        
        [Required, MinLength(2), MaxLength(255)]
        public string Reference { get; set; }

        public DateTime? SellingDate { get; set; }

        public decimal? SellingPrice { get; set; }

        public decimal? TargetRent { get; set; }

        public LLDataType[] DefaultPropertyTypes { get; } = Model.DataTypes.PropertyType.GetDefaultPropertyTypes();

        public LLDataType[] DefaultPaymentTerms { get; } = PaymentTerms.GetDefaultPaymentTerms();

        public LLDataType[] DefaultFurnishings { get; } = PropertyFurnishing.GetDefaultFurnishings();

        public LLDataType[] DefaultCounties { get; } = Counties.GetDefaultCounties();

        public LLDataType[] DefaultCountries { get; } = Countries.GetDefaultCountries();

        public LLDataType[] DefaultMortgageProviders { get; } = MortgageProviders.GetDefaultMortgageProviders();

        public PropertyImageViewModel LeadImage { get; set; }

        public ICollection<PropertyImageViewModel> PropertyImages { get; set; }

        [RequiredGuid]
        public Guid Id { get; set; }
        
        public decimal? MonthlyPayment { get; set; }

        [RequiredGuid]
        public Guid PortfolioId { get; set; }
    }
}