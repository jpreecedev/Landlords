namespace Model
{
    using System;
    using Database;
    using Model.Validation;

    public class UserPermission : BaseModel, IUserEntity
    {
        public ApplicationUser User { get; set; }

        [ValidateGuid]
        public Guid UserId { get; set; }
        
        public Permission Permission { get; set; }

        [ValidateGuid]
        public Guid PermissionId { get; set; }
    }
}