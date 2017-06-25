namespace Landlords.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Model.Entities;
    using Model.Validation;

    public class TenancyViewModel
    {
        public TenancyViewModel()
        {
        }

        public TenancyViewModel(Tenancy tenancy, Guid propertyDetailsId)
        {
            if (tenancy == null)
            {
                return;
            }

            Id = tenancy.Id;
            PropertyDetailsId = propertyDetailsId;
            StartDate = tenancy.StartDate;
            EndDate = tenancy.EndDate;
            TenancyType = tenancy.TenancyType;
            RentalAmount = tenancy.RentalAmount;
            RentalFrequency = tenancy.RentalFrequency;
            RentalPaymentReference = tenancy.RentalPaymentReference;
        }

        public Guid Id { get; set; }

        [RequiredGuid]
        public Guid PropertyDetailsId { get; set; }

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