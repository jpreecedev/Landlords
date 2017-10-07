namespace Model.Validation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Landlords.ViewModels;

    public class InvoiceLineRequiredAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext == null)
            {
                return null;
            }

            var invoiceLines = (IList<InvoiceLineViewModel>) validationContext.ObjectInstance;
            if (invoiceLines == null)
            {
                return new ValidationResult($"Not found");
            }

            if (invoiceLines.All(c => !c.IsDeleted || c.IsLineEmpty()))
            {
                return ValidationResult.Success;
            }

            return null;
        }
    }
}