namespace Model
{
    using System;
    using DataTypes;

    //EF Core does not support complex types

    public class PropertyDetails : BaseModel
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

        public decimal SellingPrice { get; set; }

        public string PropertyStreetAddress { get; set; }

        public string PropertyTownOrCity { get; set; }

        public string PropertyCountyOrRegion { get; set; }

        public string PropertyPostcode { get; set; }

        public string PropertyCountry { get; set; }

        public int ParkingSpaces { get; set; }

        public bool Garage { get; set; }

        public int SmokeAlarms { get; set; }

        public bool HouseAlarm { get; set; }

        public string Notes { get; set; }

        public bool IsAvailableForLetting { get; set; }
    }
}