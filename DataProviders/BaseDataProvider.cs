namespace Landlords.DataProviders
{
    using Database;
    using Microsoft.AspNetCore.Hosting;

    public class BaseDataProvider
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