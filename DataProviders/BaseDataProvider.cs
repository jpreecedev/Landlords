namespace Landlords.DataProviders
{
    using System;
    using Model;
    using Model.Database;

    public class BaseDataProvider<T> where T : BaseModel
    {
        protected void PopulateNewEntity(ApplicationUser user, T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.User = user;
            entity.UserId = user.Id;
            entity.Created = DateTime.Now;
        }
    }
}