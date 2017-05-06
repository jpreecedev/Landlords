namespace Model
{
    using System;
    using Database;
    using Validation;

    public class UserPermission : BaseModel, IUserEntity
    {
        public string Permission { get; set; }

        [ValidateGuid]
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}