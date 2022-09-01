using System.Linq;
using System.Linq.Expressions;
using KueiPackages.Dapper.Generator;
using KueiPackages.Dapper.Generator.Models;
using KueiPackages.Dapper.Models;

namespace KueiPackages.Dapper;

public class ValidateService<T> : IValidateService<T>
{
    private readonly PropertyInfoService _propertyInfoService;
    private readonly IList<string>       _errorMessages = new List<string>();

    private T _dto;

    public ValidateService(PropertyInfoService propertyInfoService)
    {
        _propertyInfoService = propertyInfoService;
    }

    public IValidateService<T> Validate(T? dto)
    {
        if (dto == null)
        {
            throw new Exception("錯誤資料");
        }

        _dto = dto;

        return this;
    }

    public IValidateService<T> ValidateString(Expression<Func<T, string?>> expression,
                                              bool                         required             = false,
                                              string?                      requiredMessage      = null,
                                              long?                        maxLength            = null,
                                              string?                      maxLengthMessage     = null,
                                              long?                        minLength            = null,
                                              string?                      minLengthMessage     = null,
                                              bool                         validateSqlInjection = false)
    {
        var propertyName    = GetPropertyName(expression);
        var propertyInfoDto = GetPropertyInfoDto(propertyName);

        var value = expression.Compile().Invoke(_dto);
        if (required && string.IsNullOrEmpty(value))
        {
            _errorMessages.Add(GetRequiredMessage(propertyInfoDto.DisplayName, requiredMessage));
        }
        else if (maxLength != null && value.Length > maxLength)
        {
            _errorMessages.Add(GetMaxLengthMessage(propertyInfoDto.DisplayName, maxLengthMessage, maxLength.Value));
        }
        else if (minLength != null && value.Length < minLength)
        {
            _errorMessages.Add(GetMinLengthMessage(propertyInfoDto.DisplayName, minLengthMessage, minLength.Value));
        }
        else if (validateSqlInjection)
        {
            // TODO
        }

        return this;
    }

    public IValidateService<T> ValidateLong(Expression<Func<T, long?>> expression,
                                            bool                       required        = false,
                                            string?                    requiredMessage = null,
                                            long?                      max             = null,
                                            string?                    maxMessage      = null,
                                            long?                      min             = null,
                                            string?                    minMessage      = null)
    {
        var propertyName    = GetPropertyName(expression);
        var propertyInfoDto = GetPropertyInfoDto(propertyName);

        var value = expression.Compile().Invoke(_dto);
        if (required && value == 0)
        {
            _errorMessages.Add(GetRequiredMessage(propertyInfoDto.DisplayName, requiredMessage));
        }
        else if (max != null && value > max)
        {
            _errorMessages.Add(GetMaxMessage(propertyInfoDto.DisplayName, maxMessage, max.Value));
        }
        else if (min != null && value < min)
        {
            _errorMessages.Add(GetMinMessage(propertyInfoDto.DisplayName, minMessage, min.Value));
        }

        return this;
    }

    public IValidateService<T> ValidateInt(Expression<Func<T, int?>> expression,
                                           bool                      required        = false,
                                           string?                   requiredMessage = null,
                                           int?                      max             = null,
                                           string?                   maxMessage      = null,
                                           int?                      min             = null,
                                           string?                   minMessage      = null)
    {
        var propertyName    = GetPropertyName(expression);
        var propertyInfoDto = GetPropertyInfoDto(propertyName);

        var value = expression.Compile().Invoke(_dto);
        if (required && value == 0)
        {
            _errorMessages.Add(GetRequiredMessage(propertyInfoDto.DisplayName, requiredMessage));
        }
        else if (max != null && value > max)
        {
            _errorMessages.Add(GetMaxMessage(propertyInfoDto.DisplayName, maxMessage, max.Value));
        }
        else if (min != null && value < min)
        {
            _errorMessages.Add(GetMinMessage(propertyInfoDto.DisplayName, minMessage, min.Value));
        }

        return this;
    }

    public IValidateService<T> ValidateDecimal(Expression<Func<T, decimal?>> expression,
                                               bool                          required        = false,
                                               string?                       requiredMessage = null,
                                               decimal?                      max             = null,
                                               string?                       maxMessage      = null,
                                               decimal?                      min             = null,
                                               string?                       minMessage      = null)
    {
        var propertyName    = GetPropertyName(expression);
        var propertyInfoDto = GetPropertyInfoDto(propertyName);

        var value = expression.Compile().Invoke(_dto);
        if (required && value == 0)
        {
            _errorMessages.Add(GetRequiredMessage(propertyInfoDto.DisplayName, requiredMessage));
        }
        else if (max != null && value > max)
        {
            _errorMessages.Add(GetMaxMessage(propertyInfoDto.DisplayName, maxMessage, max.Value));
        }
        else if (min != null && value < min)
        {
            _errorMessages.Add(GetMinMessage(propertyInfoDto.DisplayName, minMessage, min.Value));
        }

        return this;
    }

    private string GetPropertyName<T, TKey>(Expression<Func<T, TKey>> expression)
    {
        var memberExpression = expression?.Body as MemberExpression;
        var propertyName     = memberExpression.Member?.Name;
        return propertyName;
    }

    private PropertyInfoDto GetPropertyInfoDto(string propertyName)
    {
        var propertyInfo = _propertyInfoService.GetProperties<T>().GetValueOrDefault(propertyName);
        if (propertyInfo == null)
        {
            throw new Exception("錯誤資料");
        }

        return propertyInfo;
    }

    private string GetRequiredMessage(string? displayName, string customErrorMessage)
    {
        return string.IsNullOrEmpty(customErrorMessage) ? $"{displayName}不能為空" : customErrorMessage;
    }

    private string GetMaxLengthMessage(string? displayName, string customErrorMessage, long maxLength)
    {
        return string.IsNullOrEmpty(customErrorMessage) ? $"{displayName}長度不能大於{maxLength}" : customErrorMessage;
    }

    private string GetMinLengthMessage(string? displayName, string customErrorMessage, long minLength)
    {
        return string.IsNullOrEmpty(customErrorMessage) ? $"{displayName}長度不能小於{minLength}" : customErrorMessage;
    }

    private string GetMaxMessage(string? displayName, string customErrorMessage, long max)
    {
        return string.IsNullOrEmpty(customErrorMessage) ? $"{displayName}不能大於{max}" : customErrorMessage;
    }

    private string GetMinMessage(string? displayName, string customErrorMessage, long min)
    {
        return string.IsNullOrEmpty(customErrorMessage) ? $"{displayName}不能小於{min}" : customErrorMessage;
    }

    private string GetMaxMessage(string? displayName, string customErrorMessage, decimal max)
    {
        return string.IsNullOrEmpty(customErrorMessage) ? $"{displayName}不能大於{max}" : customErrorMessage;
    }

    private string GetMinMessage(string? displayName, string customErrorMessage, decimal min)
    {
        return string.IsNullOrEmpty(customErrorMessage) ? $"{displayName}不能小於{min}" : customErrorMessage;
    }

    public void ThrowExceptionWhenInvalid()
    {
        if (_errorMessages.Any())
        {
            var errorMessage = "表單驗証失敗：" + Environment.NewLine + string.Join(Environment.NewLine, _errorMessages);
            throw new AlertException(errorMessage);
        }
    }
}