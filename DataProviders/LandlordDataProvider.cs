namespace Landlords.DataProviders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Database;
    using Landlords.ViewModels;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using Model.Database;
    using Landlords.Interfaces;

    public class LandlordDataProvider : BaseDataProvider, ILandlordDataProvider
    {
        public LandlordDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task<ICollection<LandlordListViewModel>> GetListAsync(Guid agencyId)
        {
            //TODO : Inefficient query.  Reduce number of 'from' 

            return await (from link in Context.ApplicationUserPortfolios
                          join property in Context.PropertyDetails on link.PortfolioId equals property.PortfolioId into propertyJoin
                          from user in Context.Users
                          join userRole in Context.UserRoles on user.Id equals userRole.UserId
                          join role in Context.Roles on userRole.RoleId equals role.Id
                          where link.AgencyId == agencyId && user.Id == link.UserId && role.Name == ApplicationRoles.Landlord
                          select new LandlordListViewModel
                          {
                              LandlordName = user.FirstName + " " + user.LastName,
                              MainPhoneNumber = user.PhoneNumber,
                              SecondaryPhoneNumber = user.SecondaryPhoneNumber,
                              Properties = propertyJoin.Count()
                          })
                .ToListAsync();
        }
    }
}