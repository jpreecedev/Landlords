﻿namespace Landlords.DataProviders
{
    using Database;
    using Microsoft.AspNetCore.Hosting;

    public class BaseDataProvider
    {
        private NotificationsDataProvider _notificationsDataProvider;

        public BaseDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context)
        {
            HostingEnvironment = hostingEnvironment;
            Context = context;
        }

        protected LLDbContext Context { get; set; }
        protected IHostingEnvironment HostingEnvironment { get; set; }
    }
}