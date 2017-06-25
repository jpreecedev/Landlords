namespace Model.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Validation;

    public class Tenancy : BaseModel
    {
        [RequiredGuid]
        public Guid PropertyDetailsId { get; set; }

        public PropertyDetails PropertyDetails { get; set; }

        [LLDate]
        public DateTime StartDate { get; set; }

        [LLDate]
        public DateTime EndDate { get; set; }

        [Display(Name = "Tenancy type")]
        [Required, MinLength(2)]
        public string TenancyType { get; set; }

        [Display(Name = "Rental Amount")]
        [Range(1, 50000)]
        public decimal RentalAmount { get; set; }

        [Display(Name = "Rental frequency")]
        [Required]
        public string RentalFrequency { get; set; }

        [Display(Name = "Rental payment reference")]
        [Required]
        public string RentalPaymentReference { get; set; }
    }
}