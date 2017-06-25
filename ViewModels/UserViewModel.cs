namespace Landlords.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Model.Validation;

    public class UserViewModel
    {
        [RequiredGuid]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public ICollection<UserPermissionViewModel> Permissions { get; set; }
    }
}