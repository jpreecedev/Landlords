namespace Landlords.Database
{
    using System.Linq;
    using Model;

    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly LLDbContext _dataContext;

        public DataAccessProvider(LLDbContext dataContext)
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
