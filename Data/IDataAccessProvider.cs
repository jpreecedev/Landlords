namespace Landlords.Data
{
    using Model;

    public interface IDataAccessProvider
    {
        PropertyOverview GetPropertyOverview();
        void SavePropertyOverview(PropertyOverview data);
    }
}