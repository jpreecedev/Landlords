namespace Model
{
    using Model.Database;
    using Model.Validation;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class BaseModel
    {
        [ValidateGuid]
        public Guid Id { get; set; }

        [ValidateGuid]
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Deleted { get; set; }

        [NotMapped]
        public bool IsDeleted
        {
            get { return Deleted != null; }
        }

        [NotMapped]
        public bool IsNewEntity
        {
            get { return Id == default(Guid); }
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        public virtual T Clone<T>()
        {
            return (T)Clone();
        }
    }
}