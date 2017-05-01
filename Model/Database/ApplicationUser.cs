namespace Model.Database
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using System;

    public class ApplicationUser : IdentityUser<Guid>, IApplicationUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MainPhoneNumber { get; set; }

        public string SecondaryPhoneNumber { get; set; }

        public DateTime? AvailableFrom { get; set; }

        public DateTime? AvailableTo { get; set; }
    }
}