namespace Model.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    public class ValidateGuidAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "'{0}' does not contain a valid guid";

        public ValidateGuidAttribute() : base(DefaultErrorMessage)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var input = Convert.ToString(value, CultureInfo.CurrentCulture);

            if (string.IsNullOrWhiteSpace(input))
                return null;

            Guid guid;
            if (!Guid.TryParse(input, out guid))
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

            return ValidationResult.Success;
        }
    }
}