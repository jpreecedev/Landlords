namespace Model.Database
{
    using System;

    public interface IApplicationUser
    {
        DateTime? AvailableFrom { get; set; }
        DateTime? AvailableTo { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        string SecondaryPhoneNumber { get; set; }
    }
}