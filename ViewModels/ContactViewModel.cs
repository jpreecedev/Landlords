namespace Landlords.ViewModels
{
    using System;
    using Model.Database;

    public class ContactViewModel
    {
        public ApplicationUser User { get; set; }

        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}