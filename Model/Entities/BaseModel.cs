namespace Model.Entities
{
    using Model.Validation;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Reflection;

    public class BaseModel
    {
        [RequiredGuid]
        public Guid Id { get; set; }

        [LLDate]
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

        public void MapFrom(object viewModel)
        {
            EntityMapper.MapFrom(this, viewModel);
        }
    }
}