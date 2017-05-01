namespace Landlords.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Model.Database;
    using Model.Validation;

    public class ProfileViewModel : IApplicationUser
    {
        public ProfileViewModel()
        {
            
        }

        public ProfileViewModel(ApplicationUser applicationUser)
        {
            if (applicationUser == null) throw new ArgumentNullException(nameof(applicationUser));

            UserId = applicationUser.Id;
            FirstName = applicationUser.FirstName;
            LastName = applicationUser.LastName;
            AvailableFrom = applicationUser.AvailableFrom;
            AvailableTo = applicationUser.AvailableTo;
            MainPhoneNumber = applicationUser.MainPhoneNumber;
            SecondaryPhoneNumber = applicationUser.SecondaryPhoneNumber;
        }

        [ValidateGuid]
        public Guid UserId { get; set; }
       
        [Required(ErrorMessage = "First name is required")]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MinLength(2)]
        public string LastName { get; set; }

        public DateTime? AvailableFrom { get; set; }

        public DateTime? AvailableTo { get; set; }

        public string MainPhoneNumber { get; set; }

        public string SecondaryPhoneNumber { get; set; }
    }
}