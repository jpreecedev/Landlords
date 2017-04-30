namespace Model
{
    using System;

    public interface IPropertyImage
    {
        string FileName { get; set; }
        Guid PropertyId { get; set; }
    }
}