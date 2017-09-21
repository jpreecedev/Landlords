namespace Landlords.ViewModels
{
    using Model.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class InvoiceViewModel
    {
        public InvoiceViewModel()
        {
            
        }

        public InvoiceViewModel(Invoice invoice)
        {
            if (invoice == null)
            {
                return;
            }

            Id = invoice.Id;
            Date = invoice.Date;
            Number = invoice.Number;
            DueDate = invoice.DueDate;
            PoNumber = invoice.PoNumber;
            SubTotal = invoice.SubTotal;
            VAT = invoice.VAT;
            Total = invoice.Total;

            if (invoice.Supplier != null)
            {
                Supplier = new SupplierViewModel(invoice.Supplier);
            }
            if (invoice.Lines != null)
            {
                Lines = invoice.Lines.Select(c => new InvoiceLineViewModel(c)).ToList();
            }
        }

        public SupplierViewModel Supplier { get; set; }

        public ICollection<InvoiceLineViewModel> Lines { get; set; }

        public Guid Id { get; set; }

        public DateTime? Date { get; set; }

        public string Number { get; set; }

        public DateTime? DueDate { get; set; }

        public string PoNumber { get; set; }

        public decimal SubTotal { get; set; }

        public decimal VAT { get; set; }

        public decimal Total { get; set; }
    }
}