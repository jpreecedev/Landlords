namespace Landlords.ViewModels
{
    using System;
    using Model.Entities;

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
        
        public Guid PropertyDetailsId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string TenancyType { get; set; }
        
        public decimal RentalAmount { get; set; }

        public string RentalFrequency { get; set; }
        
        public string RentalPaymentReference { get; set; }
    }
}