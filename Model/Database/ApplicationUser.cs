namespace Model.Database
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;

    public class ApplicationUser : IdentityUser<Guid>, IApplicationUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string SecondaryPhoneNumber { get; set; }

        public DateTime? AvailableFrom { get; set; }

        public DateTime? AvailableTo { get; set; }

        public Guid? AgencyId { get; set; }
        
        public Agency Agency { get; set; }

        public ICollection<ApplicationUserPortfolio> Portfolios { get; set; }
    }
}