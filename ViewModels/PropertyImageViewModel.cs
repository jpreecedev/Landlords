namespace Landlords.ViewModels
{
    using System;
    using Model;
    using Model.Validation;

    public class PropertyImageViewModel : IPropertyImage, IEntity<PropertyImage>
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

        public PropertyImage Map()
        {
            return new PropertyImage
            {
                FileName = FileName,
                PropertyId = PropertyId
            };
        }

        public string FileName { get; set; }

        [ValidateGuid]
        public Guid PropertyId { get; set; }

        [ValidateGuid]
        public Guid Id { get; set; }
    }
}