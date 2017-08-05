namespace Landlords.ViewModels
{
    using System;
    using Model.Database;

    public class ContactViewModel
    {
        public ContactViewModel()
        {
            
        }

        public ContactViewModel(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
            {
                return;
            }
            
            UserId = applicationUser.Id;
            FirstName = applicationUser.FirstName;
            LastName = applicationUser.LastName;
        }
        
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}