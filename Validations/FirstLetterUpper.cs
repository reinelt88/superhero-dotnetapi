using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI.Validations;

public class FirstLetterUpper : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var stringValue = value.ToString();
        if (string.IsNullOrEmpty(stringValue))
        {
            return ValidationResult.Success;
        }

        // check if stringValue has the first letter in uppercase
        if (char.IsLower(stringValue[0]))
        {
            return new ValidationResult("First letter must be uppercase");
        }
        
        return ValidationResult.Success;
    }
}