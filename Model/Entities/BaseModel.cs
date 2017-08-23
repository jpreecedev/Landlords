﻿namespace Model.Entities
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
            if (viewModel == null)
            {
                return;
            }

            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo p in properties.Where(c => !c.Name.Contains("Id") && c.Name != "IsDeleted"))
            {
                var property = viewModel.GetType().GetProperty(p.Name);
                if (property != null)
                {
                    var pv = property.GetValue(viewModel);
                    if (pv != null)
                    {
                        try
                        {
                            p.SetValue(this, pv);
                        }
                        catch (Exception)
                        {
                            //TODO: Remove this.  This method doesn't like generic collections
                        }
                    }
                }
            }

            foreach (PropertyInfo p in properties.Where(c => c.Name == "IsDeleted"))
            {
                var property = viewModel.GetType().GetProperty("IsDeleted");
                if (property != null)
                {
                    var pv = (bool)property.GetValue(viewModel);
                    if (pv)
                    {
                        var deletedProperty = GetType().GetProperty("Deleted");
                        var deletedValue = deletedProperty.GetValue(this);
                        if (deletedValue == null)
                        {
                            try
                            {
                                deletedProperty.SetValue(this, DateTime.Now);
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }
                    }
                }
            }
        }
    }
}