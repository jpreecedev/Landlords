namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;

    public interface ILandlordDataProvider
    {
        Task<ICollection<LandlordListViewModel>> GetListAsync(Guid agencyId);
    }
}