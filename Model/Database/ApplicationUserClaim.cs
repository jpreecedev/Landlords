namespace Model.Database
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using System;

    public class ApplicationUserClaim : IdentityUserClaim<Guid>
    {
    }
}