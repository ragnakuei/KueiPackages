namespace KueiPackages.Microsoft.AspNetCore.Services;

public abstract class BaseValidator<T>
{
    private Dictionary<string, List<string>> _errors = new();

    public void Validate(ModelStateDictionary modelState, T? dto)
    {
        AddErrors(modelState);
        
        CustomValidate(dto);
        
        CheckErrors();
    }

    protected abstract void CustomValidate(T dto);

    protected void AddError(string propertyName, string errorMessage)
    {
        if (_errors.ContainsKey(propertyName))
        {
            var errorMessages = _errors.GetValueOrDefault(propertyName);
            errorMessages.Add(errorMessage);
        }
        else
        {
            _errors.Add(propertyName, new List<string> { errorMessage });
        }
    }

    protected void AddStringOverMaxLengthError(string propertyName, int maxLength)
    {
        AddError(propertyName, $"此欄位長度不可超過 {maxLength}");
    }

    protected void AddRequiredError(string propertyName)
    {
        AddError(propertyName, "此欄位為必填");
    }

    protected void AddNumberLongMinError(string propertyName, long min)
    {
        AddError(propertyName, $"此欄位釋數值不可低於 {min}");
    }

    protected void AddNumberLongMaxError(string propertyName, long max)
    {
        AddError(propertyName, $"此欄位釋數值不可高於 {max}");
    }

    protected DateTime? ValidateDateText(string propertyName, string dateText)
    {
        if (string.IsNullOrWhiteSpace(dateText))
        {
            return null;
        }

        if (DateTime.TryParse(dateText, out var d) == false)
        {
            AddError(propertyName, "格式有誤");
            return null;
        }

        return d;
    }

    private void AddErrors(ModelStateDictionary modelState)
    {
        foreach (var kv in modelState)
        foreach (var error in kv.Value.Errors)
        {
            AddError(kv.Key, error.ErrorMessage);
        }
    }

    private void CheckErrors()
    {
        if (_errors.Count > 0)
        {
            throw new ValidateFormFailedException()
                  {
                      ValidateResult = _errors
                  };
        }
    }
}
