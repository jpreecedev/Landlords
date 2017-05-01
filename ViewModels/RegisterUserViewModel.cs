namespace Landlords.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "First name is required"), MinLength(2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required"), MinLength(2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email address is required"), EmailAddress]
        public string EmailAddress { get; set; }

        [Required, MinLength(8)]
        public string Password { get; set; }
    }
}