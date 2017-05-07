namespace Landlords.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using ViewModels;

    public interface IPermissionsDataProvider
    {
        Task<UserMenuStructureViewModel> GetMenuStructureAsync(Guid userId);
    }
}