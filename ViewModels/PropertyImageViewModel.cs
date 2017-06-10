namespace Landlords.ViewModels
{
    using System;
    using Model;
    using Model.Validation;
    using Model.Entities;

    public class PropertyImageViewModel : IPropertyImage
    {
        public PropertyImageViewModel()
        {
            
        }

        public PropertyImageViewModel(PropertyImage propertyImage)
        {
            if (propertyImage == null) throw new ArgumentNullException(nameof(propertyImage));

            FileName = propertyImage.FileName;
            PropertyId = propertyImage.PropertyId;
            Id = propertyImage.Id;
        }

        public string FileName { get; set; }

        [ValidateGuid]
        public Guid Id { get; set; }

        [ValidateGuid]
        public Guid PropertyId { get; set; }

        public PropertyDetails Property { get; set; }
    }
}