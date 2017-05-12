namespace Landlords.ViewModels
{
    using System;
    using Model;

    public class UserPermissionViewModel : IPermission
    {
        public UserPermissionViewModel(string description, string routeId)
        {
            Description = description;
            RouteId = routeId;
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public string RouteId { get; set; }
    }
}