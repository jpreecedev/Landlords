namespace Model.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    public class MustMatchGuidAttribute : ValidationAttribute
    {
        private readonly string[] _properties;

        public MustMatchGuidAttribute(params string[] properties)
        {
            _properties = properties;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext == null)
            {
                return null;
            }

            foreach (var property in _properties)
            {
                var prop = validationContext.ObjectType.GetProperty(property);
                if (prop == null)
                {
                    return new ValidationResult($"{prop} not found");
                }

                var guidValue = (Guid) prop.GetValue(validationContext.ObjectInstance, null);
                if ((Guid) value == guidValue)
                {
                    return ValidationResult.Success;
                }
            }

            return null;
        }
    }
}