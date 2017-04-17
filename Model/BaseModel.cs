﻿namespace Model
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BaseModel
    {
        public Guid Id { get; set; }

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