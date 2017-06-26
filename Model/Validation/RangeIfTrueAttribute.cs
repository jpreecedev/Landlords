namespace Model.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Property)]
    public class RangeIfTrueAttribute : RangeAttribute
    {
        private readonly string _property;

        public RangeIfTrueAttribute(string property, int min, int max) : base(min, max)
        {
            _property = property;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext == null)
            {
                return null;
            }

            var property = validationContext.ObjectType.GetProperty(_property);
            if (property == null)
            {
                return new ValidationResult($"{_property} not found");
            }

            var boolVal = property.GetValue(validationContext.ObjectInstance, null);

            if (boolVal == null || boolVal.GetType() != typeof(bool))
            {
                return new ValidationResult($"{_property} not boolean");
            }

            if ((bool)boolVal)
            {
                return base.IsValid(value, validationContext);
            }
            return null;
        }
    }
}