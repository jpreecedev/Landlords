namespace Landlords.ViewModels
{
    using Model;

    public class UserPermissionViewModel : IPermission
    {
        public UserPermissionViewModel(string description, string routeId)
        {
            Description = description;
            RouteId = routeId;
        }

        public string Description { get; set; }
        public string RouteId { get; set; }
    }
}