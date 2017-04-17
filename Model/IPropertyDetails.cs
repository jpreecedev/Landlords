﻿using System;

namespace Model
{
    public interface IPropertyDetails
    {
        DateTime? ConstructionDate { get; set; }
        string Furnishing { get; set; }
        bool IsAvailableForLetting { get; set; }
        string PaymentTerm { get; set; }
        string PropertyCountry { get; set; }
        string PropertyCountyOrRegion { get; set; }
        string PropertyPostcode { get; set; }
        string PropertyStreetAddress { get; set; }
        string PropertyTownOrCity { get; set; }
        string PropertyType { get; set; }
        DateTime? PurchaseDate { get; set; }
        decimal? PurchasePrice { get; set; }
        string Reference { get; set; }
        DateTime? SellingDate { get; set; }
        decimal? SellingPrice { get; set; }
        decimal? TargetRent { get; set; }
    }
}