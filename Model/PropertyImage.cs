namespace Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Validation;

    public class PropertyImage : BaseModel, IPropertyImage
    {
        [ValidateGuid]
        public Guid PropertyId { get; set; }

        [MaxLength(255)]
        public string FileName { get; set; }
    }
}