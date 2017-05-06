namespace Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PropertyImage : BaseModel, IPropertyImage
    {
        public Guid PropertyId { get; set; }

        public PropertyDetails Property { get; set; }

        [MaxLength(255)]
        public string FileName { get; set; }
    }
}