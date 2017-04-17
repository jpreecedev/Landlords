namespace Landlords.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Model;
    using Model.Database;
    using Model.DataTypes;

    //TODO: Use automapper
    public class PropertyDetailsViewModel : IPropertyDetails, IEntity<PropertyDetails>
    {
        public PropertyDetailsViewModel()
        {
        }

        public PropertyDetailsViewModel(PropertyDetails propertyDetails)
        {
            Id = propertyDetails.Id;
            UserId = propertyDetails.User;
            ConstructionDate = propertyDetails.ConstructionDate;
            Furnishing = propertyDetails.Furnishing;
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
        }

        public DateTime? ConstructionDate { get; set; }

        [Required]
        [MaxLength(255)]
        public string Furnishing { get; set; }

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

        [MaxLength(255)]
        public string Reference { get; set; }

        public DateTime? SellingDate { get; set; }

        public decimal? SellingPrice { get; set; }

        public decimal? TargetRent { get; set; }

        public string[] DefaultPropertyTypes { get; } = Model.DataTypes.PropertyType.GetDefaultPropertyTypes();

        public string[] DefaultPaymentTerms { get; } = PaymentTerms.GetDefaultPaymentTerms();

        public string[] DefaultFurnishings { get; } = PropertyFurnishing.GetDefaultFurnishings();

        public string[] DefaultCountries { get; } = Countries.GetDefaultCountries();

        public Guid Id { get; private set; }

        public ApplicationUser UserId { get; private set; }

        public PropertyDetails Map()
        {
            return new PropertyDetails
            {
                Reference = Reference,
                Furnishing = Furnishing,
                PropertyType = PropertyType,
                ConstructionDate = ConstructionDate,
                TargetRent = TargetRent,
                PaymentTerm = PaymentTerm,
                PurchaseDate = PurchaseDate,
                PurchasePrice = PurchasePrice,
                SellingDate = SellingDate,
                SellingPrice = SellingPrice,
                PropertyStreetAddress = PropertyStreetAddress,
                PropertyTownOrCity = PropertyTownOrCity,
                PropertyCountyOrRegion = PropertyCountyOrRegion,
                PropertyPostcode = PropertyPostcode,
                PropertyCountry = PropertyCountry,
                IsAvailableForLetting = IsAvailableForLetting
            };
        }
    }
}