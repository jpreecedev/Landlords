namespace Model
{
    using Model.Entities;

    public interface IPropertyImage
    {
        string FileName { get; set; }
        PropertyDetails Property { get; set; }
    }
}