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
            PhoneNumber = applicationUser.PhoneNumber;
            SecondaryPhoneNumber = applicationUser.SecondaryPhoneNumber;
            EmailAddress = applicationUser.Email;
            EmailConfirmed = applicationUser.EmailConfirmed;
        }

        [RequiredGuid]
        public Guid UserId { get; set; }
       
        [Display(Name = "first name")]
        [Required, MinLength(2)]
        public string FirstName { get; set; }

        [Display(Name = "last name")]
        [Required, MinLength(2)]
        public string LastName { get; set; }

        public DateTime? AvailableFrom { get; set; }

        public DateTime? AvailableTo { get; set; }

        public string PhoneNumber { get; set; }

        public string SecondaryPhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public bool EmailConfirmed { get; set; }
    }
}