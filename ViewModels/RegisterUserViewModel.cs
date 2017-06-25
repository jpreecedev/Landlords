namespace Landlords.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterUserViewModel
    {
        [Display(Name = "First name")]
        [Required, MinLength(2)]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required, MinLength(2)]
        public string LastName { get; set; }

        [Display(Name = "Email address")]
        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [Required, MinLength(8)]
        public string Password { get; set; }
    }
}