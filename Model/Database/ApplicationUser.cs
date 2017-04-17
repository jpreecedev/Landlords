namespace Model.Database
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using System;

    public class ApplicationUser : IdentityUser<Guid>
    {
    }
}