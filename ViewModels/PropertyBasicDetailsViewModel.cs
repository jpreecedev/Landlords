namespace Landlords.ViewModels
{
    using System;
    using Model.Entities;
    using Model.Validation;

    public class PropertyBasicDetailsViewModel
    {
        public PropertyBasicDetailsViewModel()
        {
            
        }

        public PropertyBasicDetailsViewModel(PropertyDetails propertyDetails)
        {
            if (propertyDetails == null)
            {
                return;
            }

            Id = propertyDetails.Id;
            PropertyReference = propertyDetails.Reference;
            PropertyStreetAddress = propertyDetails.PropertyStreetAddress;
            PortfolioId = propertyDetails.PortfolioId;

            if (propertyDetails.Portfolio != null)
            {
                PortfolioName = propertyDetails.Portfolio.DisplayName;
            }
        }

        [RequiredGuid]
        public Guid Id { get; set; }
        
        public string PropertyReference { get; set; }

        public string PropertyStreetAddress { get; set; }

        public Guid PortfolioId { get; set; }

        public string PortfolioName { get; set; }

        public string Text
        {
            get { return PropertyStreetAddress; }
        }
    }
}