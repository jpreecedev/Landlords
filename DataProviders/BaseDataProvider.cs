namespace Landlords.DataProviders
{
    using Database;
    using Microsoft.AspNetCore.Hosting;
    using Model;

    public class BaseDataProvider<T> where T : BaseModel
    {
        public BaseDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context)
        {
            HostingEnvironment = hostingEnvironment;
            Context = context;
        }

        protected LLDbContext Context { get; set; }
        protected IHostingEnvironment HostingEnvironment { get; set; }
    }
}