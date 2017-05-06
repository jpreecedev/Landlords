namespace Model
{
    using System;

    public interface IPropertyImage
    {
        string FileName { get; set; }
        PropertyDetails Property { get; set; }
    }
}