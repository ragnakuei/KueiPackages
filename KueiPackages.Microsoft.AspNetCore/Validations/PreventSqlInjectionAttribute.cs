namespace KueiPackages.Microsoft.AspNetCore.Validations;

public class PreventSqlInjectionAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var sqlInjectionValidateStringService = validationContext.GetService<ISqlInjectionValidateStringService>()
                                             ?? throw new ArgumentNullException("validationContext.GetService<ISqlInjectionValidateStringService>()");

        if (value == null)
        {
            return ValidationResult.Success;
        }
        
        if (value.GetType().IsDictionary())
        {
            if (value is IDictionary<string, string> dict)
            {
                foreach (var key in dict.Keys)
                {
                    sqlInjectionValidateStringService.Validate(key);
                    sqlInjectionValidateStringService.Validate(dict[key]);
                }
            }
        }
        else if (value is IFormFile formFile)
        {
            sqlInjectionValidateStringService.Validate(formFile.FileName);
        }
        else
        {
            sqlInjectionValidateStringService.Validate(value?.ToString());
        }

        return ValidationResult.Success;
    }
}
