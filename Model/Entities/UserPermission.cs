namespace Model.Entities
{
    using System;
    using Database;
    using Model.Validation;

    public class UserPermission : BaseModel, IUserEntity
    {
        public ApplicationUser User { get; set; }

        [RequiredGuid]
        public Guid UserId { get; set; }
        
        public Permission Permission { get; set; }

        [RequiredGuid]
        public Guid PermissionId { get; set; }
    }
}