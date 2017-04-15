namespace Landlords.Database
{
    using Model;

    public interface IDataAccessProvider
    {
        PropertyOverview GetPropertyOverview();
        void SavePropertyOverview(PropertyOverview data);
    }
}