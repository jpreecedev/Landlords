namespace Model
{
    using System;
    using Database;
    using Model.Validation;

    public class UserPermission : BaseModel, IUserEntity, IPermission
    {
        public string RouteId { get; set; }

        public string Description { get; set; }

        public string DisplayText { get; set; }

        [ValidateGuid]
        public Guid? ParentPermissionId { get; set; }

        public ApplicationUser User { get; set; }

        [ValidateGuid]
        public Guid UserId { get; set; }
    }
}