namespace Model
{
    using System.ComponentModel.DataAnnotations;

    public enum Permissions
    {
        [Display(Name = "Update profile")]
        ProfileUpdate,
        [Display(Name = "View profile")]
        ProfileView
    }
}