namespace Landlords.Data
{
    using System.Linq;
    using Model;

    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly IDataContext _dataContext;

        public DataAccessProvider(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public PropertyOverview GetPropertyOverview()
        {
            return _dataContext.PropertyOverview.FirstOrDefault();
        }

        public void SavePropertyOverview(PropertyOverview data)
        {
            _dataContext.PropertyOverview.Add(data);
            _dataContext.SaveChanges();
        }
    }
}
