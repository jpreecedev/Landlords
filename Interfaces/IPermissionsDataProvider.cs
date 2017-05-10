namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;

    public interface IPermissionsDataProvider
    {
        Task<ICollection<UserPermissionViewModel>> GetPermissionsAsync(Guid userId);
    }
}