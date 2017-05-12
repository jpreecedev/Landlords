namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;

    public interface IPermissionsDataProvider
    {
        Task<ICollection<PermissionViewModel>> GetPermissionsAsync();
        Task<ICollection<UserPermissionViewModel>> GetUserPermissionsAsync(Guid userId);
        Task<ICollection<UserViewModel>> GetUsersAsync();
        Task RemovePermissionAsync(Guid userId, Guid permissionId);
        Task AddPermissionAsync(Guid userId, Guid permissionId);
    }
}