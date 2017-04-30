namespace Landlords.DataProviders
{
    using System;
    using System.Threading.Tasks;
    using Database;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Model;
    using Model.Database;
    using System.Linq;

    public class BaseDataProvider<T> where T : BaseModel
    {
        public BaseDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context)
        {
            HostingEnvironment = hostingEnvironment;
            Context = context;
        }

        protected LLDbContext Context { get; set; }
        protected IHostingEnvironment HostingEnvironment { get; set; }

        protected void PopulateNewEntity(ApplicationUser user, T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.User = user;
            entity.UserId = user.Id;
            entity.Created = DateTime.Now;
        }

        protected void PopulateNewEntity(Guid userId, T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.UserId = userId;
            entity.Created = DateTime.Now;
        }

        public async Task DeleteAsync(Guid userId, Guid entityId)
        {
            var entity = await Context.Set<T>()
                .Where(c => !c.IsDeleted)
                .FirstOrDefaultAsync(c => c.UserId == userId && c.Id == entityId);

            if (entity != null)
            {
                entity.Deleted = DateTime.Now;
                await Context.SaveChangesAsync();
            }
        }
    }
}