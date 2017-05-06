namespace Model
{
    using System;
    using Database;

    public interface IUserEntity
    {
        Guid UserId { get; set; }

        ApplicationUser User { get; set; }
    }
}