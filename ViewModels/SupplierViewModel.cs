namespace Landlords.ViewModels
{
    using System;
    using Model.Entities;

    public class SupplierViewModel
    {
        public SupplierViewModel()
        {
        }

        public SupplierViewModel(Supplier supplier)
        {
            if (supplier == null)
            {
                return;
            }

            Id = supplier.Id;
            Name = supplier.Name;
            AddressLine1 = supplier.AddressLine1;
            AddressLine2 = supplier.AddressLine2;
            AddressLine3 = supplier.AddressLine3;
            TownOrCity = supplier.TownOrCity;
            PostCode = supplier.PostCode;
            MainContactNumber = supplier.MainContactNumber;
            SecondaryContactNumber = supplier.SecondaryContactNumber;
            EmailAddress = supplier.EmailAddress;
            PortfolioId = supplier.PortfolioId;
        }

        public Guid Id { get; set; }

        public Guid PortfolioId { get; set; }

        public string Name { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string TownOrCity { get; set; }

        public string PostCode { get; set; }

        public string MainContactNumber { get; set; }

        public string SecondaryContactNumber { get; set; }

        public string EmailAddress { get; set; }

        public string Text => Name;

        public string Value => Id.ToString();
    }
}