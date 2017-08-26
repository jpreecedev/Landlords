namespace Model.Database
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Model.Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ApplicationUser : IdentityUser<Guid>, IApplicationUser
    {
        public string Title { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }
        
        public string SecondaryPhoneNumber { get; set; }

        public DateTime? AvailableFrom { get; set; }

        public DateTime? AvailableTo { get; set; }

        public Guid? AgencyId { get; set; }
        
        public Agency Agency { get; set; }

        public bool IsPermitted { get; set; }

        public ICollection<ApplicationUserPortfolio> Portfolios { get; set; }
        
        public string Name
        {
            get
            {
                var builder = new StringBuilder();

                if (!string.IsNullOrEmpty(Title))
                {
                    builder.AppendFormat("{0} ", Title);
                }
                if (!string.IsNullOrEmpty(FirstName))
                {
                    builder.AppendFormat("{0} ", FirstName);
                }
                if (!string.IsNullOrEmpty(LastName))
                {
                    builder.AppendFormat("{0}", LastName);
                }

                return builder.ToString();
            }
        }

        public void MapFrom(object model)
        {
            EntityMapper.MapFrom(this, model);
        }
    }
}