namespace Model.Database
{
    using System;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class ApplicationUserLogin : IdentityUserLogin<Guid>
    {
    }
}