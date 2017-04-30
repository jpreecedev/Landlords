namespace Landlords.DataProviders
{
    using Model;
    using Landlords.Database;
    using Landlords.ViewModels;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Core;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;

    public class PropertyDataProvider : BaseDataProvider<PropertyDetails>, IPropertyDataProvider
    {
        public PropertyDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task CreateAsync(ClaimsPrincipal user, PropertyDetailsViewModel viewModel)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            PropertyDetails entity = viewModel.Map();

            var applicationUser = user.GetApplicationUser(Context);
            PopulateNewEntity(applicationUser, entity);

            await Context.PropertyDetails.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<PropertyDetailsViewModel> GetDetailsAsync(Guid userId, Guid propertyId)
        {
            return await (from details in Context.PropertyDetails
                          join images in Context.PropertyImages on details.Id equals images.PropertyId into imageJoin
                          from img in imageJoin.DefaultIfEmpty()
                          where details.UserId == userId && details.Id == propertyId && !details.IsDeleted
                          select new PropertyDetailsViewModel(details.Id, userId)
                          {
                              Reference = details.Reference,
                              PropertyStreetAddress = details.PropertyStreetAddress,
                              PropertyImages = imageJoin.Where(c => !c.IsDeleted).Select(c => new PropertyImageViewModel(c)).ToList()
                          })
                         .FirstOrDefaultAsync();
        }

        public async Task<ICollection<PropertyDetailsViewModel>> GetListAsync(Guid userId)
        {
            return await (from details in Context.PropertyDetails
                          join images in Context.PropertyImages on details.Id equals images.PropertyId into imageJoin
                          where details.UserId == userId && !details.IsDeleted
                          select new PropertyDetailsViewModel(details.Id, userId)
                          {
                              Reference = details.Reference,
                              PropertyStreetAddress = details.PropertyStreetAddress,
                              LeadImage = imageJoin.Where(c => !c.IsDeleted).Select(c => new PropertyImageViewModel(c)).FirstOrDefault()
                          })
                          .ToListAsync();
        }
    }
}
