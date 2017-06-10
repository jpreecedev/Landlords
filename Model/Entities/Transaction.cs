namespace Model.Entities
{
    using System;
    using Validation;

    public class Transaction : BaseModel
    {
        [ValidateGuid]
        public Guid AccountId { get; set; }

        public Account Account { get; set; }

        public DateTime Date { get; set; }

        public string PaymentType { get; set; }

        public string Reference { get; set; }

        public string DestinationAccountNumber { get; set; }

        public string DestinationSortCode { get; set; }

        public decimal In { get; set; }

        public decimal Out { get; set; }

        public decimal Balance { get; set; }
    }
}