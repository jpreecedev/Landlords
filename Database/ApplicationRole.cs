namespace Landlords.Database
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using System;

    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string name)
        {
            Name = name;
        }
    }
}