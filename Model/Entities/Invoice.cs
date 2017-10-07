namespace Model.Entities
{
    using System;
    using System.Collections.Generic;

    public class Invoice : BaseModel
    {
        public Supplier Supplier { get; set; }

        public Guid SupplierId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public DateTime DueDate { get; set; }

        public string PoNumber { get; set; }

        public decimal SubTotal { get; set; }

        public decimal VAT { get; set; }

        public decimal Total { get; set; }

        public Guid PortfolioId { get; set; }

        public Portfolio Portfolio { get; set; }

        public ICollection<InvoiceLine> Lines { get; set; }
    }
}