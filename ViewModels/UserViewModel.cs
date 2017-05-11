namespace Landlords.ViewModels
{
    using System;
    using Model.Validation;

    public class UserViewModel
    {
        [ValidateGuid]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }
    }
}