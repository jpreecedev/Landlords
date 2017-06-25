namespace Landlords.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Model;
    using Model.Validation;

    public class PermissionViewModel : IPermission
    {
        [RequiredGuid]
        public Guid PermissionId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string RouteId { get; set; }
    }
}