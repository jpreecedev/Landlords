namespace Model.Entities
{
    using System;
    using Validation;

    public class Tenancy : BaseModel
    {
        [ValidateGuid]
        public Guid PropertyDetailsId { get; set; }

        public PropertyDetails PropertyDetails { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string TenancyType { get; set; }

        public decimal RentalAmount { get; set; }

        public string RentalFrequency { get; set; }

        public string RentalPaymentReference { get; set; }
    }
}