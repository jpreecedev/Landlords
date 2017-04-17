namespace Model
{
    using System;

    //EF Core does not support complex types

    public class PropertyDetails : BaseModel, IPropertyDetails
    {
        public string Reference { get; set; }

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
    }
}