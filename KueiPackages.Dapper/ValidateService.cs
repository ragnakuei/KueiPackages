using System.Linq;
using System.Linq.Expressions;
using KueiPackages.Dapper.Generator;
using KueiPackages.Models;
using PropertyInfoDto = KueiPackages.Dapper.Generator.Models.PropertyInfoDto;

namespace KueiPackages.Dapper;

public interface IValidateService
{
    PropertyValidator<T> Validate<T>(T?      dto,
                                     int?    index              = null,
                                     bool    required           = true,
                                     string? requiredMessage    = null,
                                     string? errorMessagePrefix = "");

    void ThrowExceptionWhenInvalid();
}

public class ValidateService : IValidateService
{
    private readonly PropertyInfoService  _propertyInfoService;
    private readonly IList<ValidatorBase> _validators = new List<ValidatorBase>();

    public ValidateService(PropertyInfoService propertyInfoService)
    {
        _propertyInfoService = propertyInfoService;
    }

    public PropertyValidator<T> Validate<T>(T?      dto,
                                            int?    index              = null,
                                            bool    required           = true,
                                            string? requiredMessage    = null,
                                            string? errorMessagePrefix = "")
    {
        var validator = new PropertyValidator<T>(this, _propertyInfoService.GetProperties<T>(), errorMessagePrefix);

        _validators.Add(validator);

        return validator.Validate(dto, index, required, requiredMessage);
    }

    public void ThrowExceptionWhenInvalid()
    {
        var allErrorMessages = _validators.SelectMany(x => x.ErrorMessages);

        if (allErrorMessages.Any())
        {
            throw new ApiResponseException<string[]>
                  {
                      IsFormValid  = false,
                      AlertMessage = "表單驗証失敗",
                      Data         = allErrorMessages.ToArray(),
                  };
        }
    }
}

public abstract class ValidatorBase
{
    protected readonly IValidateService _validateService;

    // TODO：要改成含有 PropertyInfoDto 的 Model
    public readonly IList<string> ErrorMessages = new List<string>();

    protected ValidatorBase(IValidateService validateService, string errorMessagePrefix)
    {
        _validateService    = validateService;
        _errorMessagePrefix = errorMessagePrefix;
    }

    protected int? _arrayIndex;

    /// <summary>
    /// 用於更深的巢狀物件前置錯誤訊息
    /// </summary>
    private readonly string? _errorMessagePrefix;

    private Dictionary<ValidatorType, Func<string, string, string>> _messageMap
        = new()
          {
              { ValidatorType.MaxLength, (displayName,     maxLength) => $"{displayName}不能超過{maxLength}個字元" },
              { ValidatorType.MinLength, (displayName,     minLength) => $"{displayName}不能少於{minLength}個字元" },
              { ValidatorType.Max, (displayName,           max) => $"{displayName}不能大於{max}" },
              { ValidatorType.Min, (displayName,           min) => $"{displayName}不能小於{min}" },
              { ValidatorType.Required, (displayName,      _) => $"{displayName}不能為空" },
              { ValidatorType.SqlInjection, (displayName,  _) => $"{displayName}含有非法字元" },
              { ValidatorType.DecimalDigits, (displayName, n) => $"{displayName}整數位數不能超過{n}位數" },
              { ValidatorType.DecimalPlace, (displayName,  n) => $"{displayName}小數位數不能超過{n}位數" },
          };

    protected void AddErrorMessage(ValidatorType validatorType,
                                   string?       displayName,
                                   string        customErrorMessage,
                                   string        argrument = "")
    {
        var message = string.IsNullOrWhiteSpace(customErrorMessage)
                          ? _messageMap[validatorType].Invoke(displayName, argrument)
                          : customErrorMessage;

        AddErrorMessage(displayName, message);
    }

    protected void AddErrorMessage(string? displayName, string errorMessage)
    {
        var arrayItemErrorMessagePrefix = _arrayIndex.HasValue
                                              ? $"{displayName} 第 {_arrayIndex.Value + 1} 筆資料 "
                                              : string.Empty;

        ErrorMessages.Add(_errorMessagePrefix + arrayItemErrorMessagePrefix + errorMessage);
    }

    public void ThrowExceptionWhenInvalid()
    {
        _validateService.ThrowExceptionWhenInvalid();
    }

    protected enum ValidatorType
    {
        Required,
        MaxLength,
        MinLength,
        Max,
        Min,
        SqlInjection,
        DecimalDigits,
        DecimalPlace
    }
}

public class PropertyValidator<T> : ValidatorBase
{
    private T? _dto;

    /// <summary>
    /// 目前此 Object Properties 的資料 
    /// </summary>
    private readonly Dictionary<string?, PropertyInfoDto> _propertyInfoDto;

    public PropertyValidator(IValidateService                     validateService,
                             Dictionary<string?, PropertyInfoDto> propertyInfoDto,
                             string?                              errorMessagePrefix)
        : base(validateService, errorMessagePrefix)
    {
        _propertyInfoDto = propertyInfoDto;
    }

    public PropertyValidator<T> Validate(T?      dto,
                                         int?    index           = null,
                                         bool    required        = true,
                                         string? requiredMessage = null)
    {
        _arrayIndex = index;
        _dto        = dto;

        // check required
        if (required && dto == null)
        {
            AddErrorMessage(ValidatorType.Required, null, requiredMessage);
        }

        if (index != null)
        {
            _arrayIndex = index;
        }

        return this;
    }

    public ArrayValidator<TItem> Array<TItem>(Expression<Func<T, TItem[]?>> expression,
                                              bool                          required               = false,
                                              string?                       requiredMessage        = null,
                                              bool                          itemRequired           = true,
                                              string?                       itemRequiredMessage    = null,
                                              string?                       itemErrorMessagePrefix = "")
    {
        var propertyName    = GetPropertyName(expression);
        var propertyInfoDto = GetPropertyInfoDto(propertyName);

        if (_dto == null)
        {
            return new ArrayValidator<TItem>(_validateService, null);
        }

        TItem[]? arrayValue = expression.Compile().Invoke(_dto);
        if (required && arrayValue?.Length > 0 == false)
        {
            AddErrorMessage(ValidatorType.Required, propertyInfoDto.DisplayName, requiredMessage);
        }

        return new ArrayValidator<TItem>(_validateService,
                                         arrayValue,
                                         itemRequired,
                                         itemRequiredMessage,
                                         itemErrorMessagePrefix);
        ;
    }

    public ObjectValidator<TObject?> Object<TObject>(Expression<Func<T, TObject?>> expression,
                                                     bool                          required               = false,
                                                     string?                       requiredMessage        = null,
                                                     string?                       itemErrorMessagePrefix = "")
    {
        var propertyName    = GetPropertyName(expression);
        var propertyInfoDto = GetPropertyInfoDto(propertyName);

        if (_dto == null)
        {
            throw new ApiResponseException<string[]>
                  {
                      IsFormValid  = false,
                      AlertMessage = "表單驗証失敗",
                      Data         = new[] { "參數錯誤" },
                  };
        }

        var value = expression.Compile().Invoke(_dto);

        if (required && value == null)
        {
            AddErrorMessage(ValidatorType.Required, propertyInfoDto.DisplayName, requiredMessage);
        }

        return new ObjectValidator<TObject?>(_validateService, value, errorMessagePrefix: itemErrorMessagePrefix);
    }


    public PropertyValidator<T> String(Expression<Func<T, string?>> expression,
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

        if (_dto == null)
        {
            return this;
        }

        var value              = expression.Compile().Invoke(_dto);
        var valueIsNullOrEmpty = string.IsNullOrEmpty(value);
        if (required && valueIsNullOrEmpty)
        {
            AddErrorMessage(ValidatorType.Required, propertyInfoDto.DisplayName, requiredMessage);
        }
        else if (valueIsNullOrEmpty == false
              && maxLength          != null
              && value.Length       > maxLength)
        {
            AddErrorMessage(ValidatorType.MaxLength, propertyInfoDto.DisplayName, maxLengthMessage, maxLength.Value.ToString());
        }
        else if (valueIsNullOrEmpty == false
              && minLength          != null
              && value.Length       < minLength)
        {
            AddErrorMessage(ValidatorType.MinLength, propertyInfoDto.DisplayName, minLengthMessage, minLength.Value.ToString());
        }
        else if (valueIsNullOrEmpty == false
              && validateSqlInjection)
        {
            // TODO
        }

        return this;
    }

    public PropertyValidator<T> Long(Expression<Func<T, long?>> expression,
                                     bool                       required        = false,
                                     string?                    requiredMessage = null,
                                     long?                      max             = null,
                                     string?                    maxMessage      = null,
                                     long?                      min             = null,
                                     string?                    minMessage      = null)
    {
        var propertyName    = GetPropertyName(expression);
        var propertyInfoDto = GetPropertyInfoDto(propertyName);

        if (_dto == null)
        {
            return this;
        }

        var value = expression.Compile().Invoke(_dto);
        if (required && value == null)
        {
            AddErrorMessage(ValidatorType.Required, propertyInfoDto.DisplayName, requiredMessage);
        }
        else if (max != null && value > max)
        {
            AddErrorMessage(ValidatorType.Max, propertyInfoDto.DisplayName, maxMessage, max.Value.ToString());
        }
        else if (min != null && value < min)
        {
            AddErrorMessage(ValidatorType.Min, propertyInfoDto.DisplayName, minMessage, min.Value.ToString());
        }

        return this;
    }

    public PropertyValidator<T> Int(Expression<Func<T, int?>> expression,
                                    bool                      required        = false,
                                    string?                   requiredMessage = null,
                                    int?                      max             = null,
                                    string?                   maxMessage      = null,
                                    int?                      min             = null,
                                    string?                   minMessage      = null)
    {
        var propertyName    = GetPropertyName(expression);
        var propertyInfoDto = GetPropertyInfoDto(propertyName);

        if (_dto == null)
        {
            return this;
        }

        var value = expression.Compile().Invoke(_dto);
        if (required && value == null)
        {
            AddErrorMessage(ValidatorType.Required, propertyInfoDto.DisplayName, requiredMessage);
        }
        else if (max != null && value > max)
        {
            AddErrorMessage(ValidatorType.Max, propertyInfoDto.DisplayName, maxMessage, max.Value.ToString());
        }
        else if (min != null && value < min)
        {
            AddErrorMessage(ValidatorType.Min, propertyInfoDto.DisplayName, minMessage, min.Value.ToString());
        }

        return this;
    }

    public PropertyValidator<T> Decimal(Expression<Func<T, decimal?>> expression,
                                        bool                          required             = false,
                                        string?                       requiredMessage      = null,
                                        decimal?                      max                  = null,
                                        string?                       maxMessage           = null,
                                        decimal?                      min                  = null,
                                        string?                       minMessage           = null,
                                        int                           integerDigits        = 18,
                                        string?                       integerDigitsMessage = null,
                                        int                           decimalPlaces        = 99,
                                        string?                       decimalPlacesMessage = null)
    {
        var propertyName    = GetPropertyName(expression);
        var propertyInfoDto = GetPropertyInfoDto(propertyName);

        if (_dto == null)
        {
            return this;
        }

        var value = expression.Compile().Invoke(_dto);
        if (required && value == null)
        {
            AddErrorMessage(ValidatorType.Required, propertyInfoDto.DisplayName, requiredMessage);
        }
        else if (max != null && value > max)
        {
            AddErrorMessage(ValidatorType.Max, propertyInfoDto.DisplayName, maxMessage, max.Value.ToString());
        }
        else if (min != null && value < min)
        {
            AddErrorMessage(ValidatorType.Min, propertyInfoDto.DisplayName, minMessage, min.Value.ToString());
        }
        else if (!ValidateIntegerDigits(value, integerDigits))
        {
            AddErrorMessage(ValidatorType.Min, propertyInfoDto.DisplayName, integerDigitsMessage, integerDigits.ToString());
        }
        else if (!ValidateDecimalPlaces(value, decimalPlaces))
        {
            AddErrorMessage(ValidatorType.Min, propertyInfoDto.DisplayName, decimalPlacesMessage, decimalPlaces.ToString());
        }

        return this;
    }

    /// <summary>
    /// 驗証整數位數
    /// </summary>
    private bool ValidateIntegerDigits(decimal? value, int integerDigits)
    {
        if (value == null)
        {
            return true;
        }

        var strValue = value.Value.ToString();
        return strValue.Split('.')[0].Length <= integerDigits;
    }

    /// <summary>
    /// 驗証小數位數
    /// </summary>
    private bool ValidateDecimalPlaces(decimal? value, int decimalPlaces)
    {
        if (value == null)
        {
            return true;
        }

        var strValue           = value.Value.ToString();
        var strValueSplitByDot = strValue.Split('.');

        if (strValueSplitByDot.Length == 1)
        {
            return true;
        }

        var decimalPlacesValue = strValueSplitByDot[1].Length;
        return decimalPlacesValue <= decimalPlaces;
    }

    private string? GetPropertyName<TKey>(Expression<Func<T, TKey>> expression)
    {
        var memberExpression = expression.Body as MemberExpression;
        var propertyName     = memberExpression?.Member.Name;
        return propertyName;
    }

    private PropertyInfoDto GetPropertyInfoDto(string? propertyName)
    {
        var propertyInfo = _propertyInfoDto.GetValueOrDefault(propertyName);
        if (propertyInfo == null)
        {
            throw new Exception("錯誤資料");
        }

        return propertyInfo;
    }
}

public class ArrayValidator<TItem> : ValidatorBase
{
    private readonly TItem[]? _arrayDtos;
    private readonly bool     _required;
    private readonly string?  _requiredMessage;
    private readonly string   _errorMessagePrefix;

    public ArrayValidator(IValidateService validateService,
                          TItem[]          arrayDtos,
                          bool             required           = true,
                          string?          requiredMessage    = null,
                          string?          errorMessagePrefix = "")
        : base(validateService, errorMessagePrefix)
    {
        _arrayDtos          = arrayDtos;
        _required           = required;
        _requiredMessage    = requiredMessage;
        _errorMessagePrefix = errorMessagePrefix;
    }

    public IValidateService ArrayItem(Action<PropertyValidator<TItem>> itemValidatorAction)
    {
        for (int i = 0; i < (_arrayDtos?.Length ?? 0); i++)
        {
            var itemValidator = _validateService.Validate(_arrayDtos[i],
                                                          i,
                                                          _required,
                                                          _requiredMessage,
                                                          _errorMessagePrefix);
            itemValidatorAction.Invoke(itemValidator);
        }

        return _validateService;
    }
}

public class ObjectValidator<T> : ValidatorBase
{
    private          T?      _objectDto;
    private readonly string? _errorMessagePrefix;

    public ObjectValidator(IValidateService validateService,
                           T?               objectDto,
                           string?          errorMessagePrefix = "")
        : base(validateService, errorMessagePrefix)
    {
        _objectDto          = objectDto;
        _errorMessagePrefix = errorMessagePrefix;
    }

    public IValidateService ObjectProperty(Action<PropertyValidator<T>> itemValidatorAction)
    {
        var validator = _validateService.Validate(_objectDto,
                                                  errorMessagePrefix: _errorMessagePrefix);

        itemValidatorAction.Invoke(validator);

        return _validateService;
    }
}

public class TestService
{
    public interface ITestService
    {
    }

    public class TestServiceA : ITestService
    {
    }
}
