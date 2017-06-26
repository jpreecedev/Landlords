namespace Model.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredIfFalseAttribute : ValidationAttribute
    {
        private readonly string _property;

        public RequiredIfFalseAttribute(string property)
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

            if (!(bool) boolVal)
            {
                var attribute = new RequiredAttribute {ErrorMessage = $"{value} is required."};
                return attribute.GetValidationResult(value, validationContext);
            }
            return null;
        }
    }
}