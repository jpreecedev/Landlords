namespace Landlords.ViewModels
{
    using System;
    using Model.Entities;

    public class InvoiceLineViewModel
    {
        public InvoiceLineViewModel()
        {
        }

        public InvoiceLineViewModel(InvoiceLine invoiceLine)
        {
            if (invoiceLine == null)
            {
                return;
            }

            Id = invoiceLine.Id;
            Item = invoiceLine.Item;
            Description = invoiceLine.Description;
            UnitCost = invoiceLine.UnitCost;
            Quantity = invoiceLine.Quantity;
            VAT = invoiceLine.VAT;
            Total = invoiceLine.Total;
        }

        public Guid Id { get; set; }

        public string Item { get; set; }

        public string Description { get; set; }

        public string UnitCost { get; set; }

        public string Quantity { get; set; }

        public decimal VAT { get; set; }

        public decimal Total { get; set; }
    }
}