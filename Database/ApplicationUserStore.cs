namespace Landlords.Database
{
    using System;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Model.Database;

    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, LLDbContext, Guid>
    {
        public ApplicationUserStore(LLDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }
    }
}