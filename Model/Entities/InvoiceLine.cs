namespace Model.Entities
{
    using System;

    public class InvoiceLine : BaseModel
    {
        public Invoice Invoice { get; set; }

        public Guid InvoiceId { get; set; }

        public string Item { get; set; }

        public string Description { get; set; }

        public decimal UnitCost { get; set; }

        public decimal Quantity { get; set; }

        public decimal VAT { get; set; }

        public decimal Total { get; set; }
    }
}