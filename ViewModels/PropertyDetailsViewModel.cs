namespace Landlords.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Model;

    public class PropertyDetailsViewModel
    {
        public PropertyDetailsViewModel()
        {
        }

        public PropertyDetailsViewModel(PropertyDetails propertyDetails)
        {
        }
        
        [Required]
        public string Reference { get; set; }

        public string PropertyType { get; set; }

        public string Furnishing { get; set; }

        public DateTime? ConstructionDate { get; set; }

        public decimal TargetRent { get; set; }

        public string PaymentTerm { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public decimal PurchasePrice { get; set; }
    }
}