namespace Landlords.ViewModels
{
    using System;
    using Model.Validation;

    public class PropertyBasicDetailsViewModel
    {
        [ValidateGuid]
        public Guid Id { get; set; }
        
        public string PropertyReference { get; set; }

        public string PropertyStreetAddress { get; set; }

        public Guid PortfolioId { get; set; }

        public string PortfolioName { get; set; }
    }
}