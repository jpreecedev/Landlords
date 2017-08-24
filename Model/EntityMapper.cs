namespace Model
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class EntityMapper
    {
        public static void MapFrom(object instance, object model)
        {
            if (model == null)
            {
                return;
            }

            var properties = instance.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var p in properties.Where(c => !c.Name.Contains("Id") && c.Name != "IsDeleted"))
            {
                var property = model.GetType().GetProperty(p.Name);
                if (property != null)
                {
                    var pv = property.GetValue(model);
                    if (pv != null)
                    {
                        try
                        {
                            p.SetValue(instance, pv);
                        }
                        catch (Exception)
                        {
                            //TODO: Remove this.  This method doesn't like generic collections
                        }
                    }
                }
            }

            foreach (var p in properties.Where(c => c.Name == "IsDeleted"))
            {
                var property = model.GetType().GetProperty("IsDeleted");
                if (property != null)
                {
                    var pv = (bool) property.GetValue(model);
                    if (pv)
                    {
                        var deletedProperty = instance.GetType().GetProperty("Deleted");
                        var deletedValue = deletedProperty.GetValue(instance);
                        if (deletedValue == null)
                        {
                            try
                            {
                                deletedProperty.SetValue(instance, DateTime.Now);
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