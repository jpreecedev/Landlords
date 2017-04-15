namespace Landlords.Database
{
    using System;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, LLDbContext, Guid>
    {
        public ApplicationUserStore(LLDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }
    }
}