using System;
using System.ComponentModel.DataAnnotations;

public class MultipleOfCustomizedAttribute : ValidationAttribute
{
    private readonly int[] _validMultiples;

    public MultipleOfCustomizedAttribute(params int[] validMultiples)
    {
        _validMultiples = validMultiples;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            int intValue;
            if (int.TryParse(value.ToString(), out intValue))
            {
                foreach (int multiple in _validMultiples)
                {
                    if (intValue % multiple == 0)
                    {
                        return ValidationResult.Success;
                    }
                }
            }

            return new ValidationResult($"The field {validationContext.DisplayName} must be a multiple of 5, 10, 20, 50, or 100.");
        }

        return ValidationResult.Success;
    }
}