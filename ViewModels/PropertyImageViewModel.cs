namespace Landlords.ViewModels
{
    using System;
    using Model;

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
        public Guid PropertyId { get; set; }
    }
}