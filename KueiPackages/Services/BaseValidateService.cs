using KueiPackages.Extensions;
using KueiPackages.Models;

namespace KueiPackages.Services;

/// <summary>
/// 驗証資料父類別
/// </summary>
public class BaseValidateService
{
    // 設計想法
    // Validate 開頭的方法
    // -- 目的
    // ---- 驗証失敗，立即中斷，不會繼續執行下去
    // ------ 中斷方式：立即 throw ApiResponseException<>
    // ---- 避免被滲透攻擊成功
    // AddError 開頭的方法
    // -- 目的
    // ---- 正常操作，可暫時取代前端驗證，但不會立即中斷，會繼續執行下去
    // ---- 驗証失敗，會將錯誤訊息加入 Errors，最後 Validate 方法會檢查 Errors 是否有錯誤訊息，有的話才會 throw ApiResponseException<>

    public List<string> Errors { get; } = new();

    #region Validate

    /// <summary>
    /// 包含 string.IsNullOrWhiteSpace & Array.Length == 0 & oject is null
    /// </summary>
    protected void ValidateRequired<T>(T source, string errorMessage)
    {
        if (source is null)
        {
            throw new ApiResponseException
                  {
                      AlertMessage = errorMessage,
                  };
        }

        if (source is string s
         && string.IsNullOrWhiteSpace(s))
        {
            throw new ApiResponseException
                  {
                      AlertMessage = errorMessage,
                  };
        }

        if (source is Array array
         && array.Length == 0)
        {
            throw new ApiResponseException
                  {
                      AlertMessage = errorMessage,
                  };
        }
    }

    public void ValidateNotEqual<T>(T source, T target, string errorMessage)
    {
        if (!Equals(source, target))
        {
            throw new ApiResponseException
                  {
                      AlertMessage = errorMessage,
                  };
        }
    }

    public void ValidateEqual<T>(T source, T target, string errorMessage)
    {
        if (Equals(source, target))
        {
            throw new ApiResponseException
                  {
                      AlertMessage = errorMessage,
                  };
        }
    }

    protected void ValidateCustom(bool isInvalid, string errorMessage)
    {
        if (isInvalid)
        {
            throw new ApiResponseException
                  {
                      AlertMessage = errorMessage,
                  };
        }
    }

    public void ValidateEquals<T>(T source, T value, string errorMessage)
    {
        if (source.Equals(value))
        {
            throw new ApiResponseException
                  {
                      AlertMessage = errorMessage,
                  };
        }
    }

    public void ValidateNotEquals<T>(T source, T value, string errorMessage)
    {
        if (source.Equals(value) == false)
        {
            throw new ApiResponseException
                  {
                      AlertMessage = errorMessage,
                  };
        }
    }

    public void ValidateContains<T>(T source, T[] values, string errorMessage)
    {
        if (values.Contains(source) == false)
        {
            throw new ApiResponseException
                  {
                      AlertMessage = errorMessage,
                  };
        }
    }

    #endregion

    #region AddError

    public void AddErrorForDateFormat(string source, string format, string errorMessage)
    {
        if (DateTime.TryParseExact(source,
                                   format,
                                   CultureInfo.InvariantCulture,
                                   DateTimeStyles.None,
                                   out _) == false)
        {
            AddError(errorMessage);
        }
    }

    /// <summary>
    /// 驗証通過，回傳 false
    /// </summary>
    /// <remarks>
    /// 回傳 true，代表有 AddError
    /// </remarks>
    public bool AddErrorForRequired<T>(T source, string errorMessage)
    {
        if (source is null)
        {
            AddError(errorMessage);
            return true;
        }

        if (source is string s
         && string.IsNullOrWhiteSpace(s))
        {
            AddError(errorMessage);
            return true;
        }

        if (source is Array array
         && array.Length > 0 == false)
        {
            AddError(errorMessage);
            return true;
        }

        return false;
    }

    public bool AddErrorForBool(bool source, string errorMessage)
    {
        if (source)
        {
            AddError(errorMessage);
        }

        return source;
    }

    public void AddErrorForIn<T>(T      source,
                                 T[]    validValues,
                                 string errorMessage)
    {
        if (source is null)
        {
            return;
        }

        if (validValues.Contains(source) == false)
        {
            AddError(errorMessage);
        }
    }

    public void AddErrorForMax<T>(T      source,
                                  int    maxCount,
                                  string errorMessage)
    {
        if (source is null)
        {
            return;
        }

        if (source is string s
         && s.Length > maxCount)
        {
            AddError(errorMessage);
        }

        if (source is int i
         && i > maxCount)
        {
            AddError(errorMessage);
        }

        if (source is long l
         && l > maxCount)
        {
            AddError(errorMessage);
        }
    }

    public void AddErrorForMin<T>(T      source,
                                  int    minCount,
                                  string errorMessage)
    {
        if (source is null)
        {
            return;
        }

        if (source is string s
         && s.Length < minCount)
        {
            AddError(errorMessage);
        }

        if (source is int i
         && i < minCount)
        {
            AddError(errorMessage);
        }

        if (source is long l
         && l < minCount)
        {
            AddError(errorMessage);
        }
    }

    public void AddError(params string[] messages)
    {
        Errors.AddRange(messages);
    }

    public void AddError(List<string> messages)
    {
        Errors.AddRange(messages);
    }

    #endregion

    public Validator<TValue> For<TValue>(TValue value, string fieldName, int? no = null)
    {
        return new(value, fieldName, no, this);
    }

    public ArrayValidator<TValue> For<TValue>(TValue[] value, string fieldName)
    {
        return new(value, fieldName, this);
    }

    protected void Validate()
    {
        if (Errors.Any())
        {
            throw new ApiResponseException<List<string>>
                  {
                      AlertMessage = "表單驗証失敗",
                      Data         = Errors,
                  };
        }
    }
}

public class Validator<TValue>
{
    // 設計想法
    // 外部傳入的 message
    // -- 可以覆蓋預設的 message
    // -- 可以給定 {0} 來取代預設的 message，其中 {0} 會被 _fieldName 取代
    // AddError / Validate 方法的想法，與 BaseValidateService 一致

    private readonly TValue              _value;
    private readonly string              _fieldName;
    private readonly int?                _no;
    private readonly BaseValidateService _baseValidateService;

    // 是否要驗証
    // -- 目的：當該欄位無資料時，後續不再繼續驗証
    private bool _isFutherValidate = true;

    public Validator(TValue              value,
                     string              fieldName,
                     int?                index,
                     BaseValidateService baseValidateService)
    {
        _value               = value;
        _fieldName           = fieldName;
        _no                  = index + 1;
        _baseValidateService = baseValidateService;
    }

    #region AddError

    public Validator<TValue> Required(string? errorMessage = null, bool isUpdateFutherValidate = true)
    {
        var message = string.Format(errorMessage ?? "請填寫 {0}", _fieldName);

        if (_isFutherValidate
         && _baseValidateService.AddErrorForRequired(_value, message)
         && isUpdateFutherValidate)
        {
            _isFutherValidate = false;
        }

        return this;
    }

    public Validator<TValue> In(TValue[] values, string? errorMessage = null, bool isUpdateFutherValidate = true)
    {
        var message = string.Format(errorMessage ?? "請填寫正確的 {0}", _fieldName);

        if (_isFutherValidate)
            _baseValidateService.AddErrorForIn(_value, values, message);

        return this;
    }

    public Validator<TValue> Max(int maxCount, string? errorMessage = null)
    {
        var template = string.Empty;

        if (_value is string)
        {
            template = $"{{0}} 長度不可超過 {maxCount} 個字元";
        }
        else if (_value is int || _value is long || _value is decimal)
        {
            template = $"{{0}} 不可超過 {maxCount}";
        }

        var message = string.Format(errorMessage ?? template, _fieldName);

        if (_isFutherValidate)
            _baseValidateService.AddErrorForMax(_value, maxCount, message);

        return this;
    }

    public Validator<TValue> Min(int minCount, string? errorMessage = null)
    {
        var template = string.Empty;

        if (_value is string)
        {
            template = $"{{0}} 長度不可小於 {minCount} 個字元";
        }
        else if (_value is int || _value is long || _value is decimal)
        {
            template = $"{{0}} 不可小於 {minCount}";
        }

        var message = string.Format(errorMessage ?? template, _fieldName);

        if (_isFutherValidate)
            _baseValidateService.AddErrorForMin(_value, minCount, message);

        return this;
    }

    public Validator<TValue> DateFormat(string format, string? errorMessage = null)
    {
        var message = string.Format(errorMessage ?? "{0} 日期格式錯誤", _fieldName);

        if (_isFutherValidate)
            _baseValidateService.AddErrorForDateFormat(_value.ToString(), format, message);

        return this;
    }

    public Validator<TValue> Bool(bool b, string? errorMessage = null)
    {
        var message = string.Format(errorMessage ?? "{0} 錯誤", _fieldName);

        if (_isFutherValidate)
            _baseValidateService.AddErrorForBool(b, message);

        return this;
    }

    public Validator<TValue> Custom(Func<bool> predicate, string? errorMessage = null)
    {
        var message = string.Format(errorMessage ?? "{0} 錯誤", _fieldName);

        if (_isFutherValidate)
        {
            var invokteReulst = predicate.Invoke();
            _baseValidateService.AddErrorForBool(invokteReulst, message);
        }

        return this;
    }

    #endregion

    #region Validate

    public Validator<TValue> ValidateNotEqual(TValue target, string? errorMessage = null)
    {
        var message = string.Format(errorMessage ?? "{0} 不可等於 {1}", _fieldName, target);

        if (_isFutherValidate)
            _baseValidateService.ValidateNotEqual(_value, target, message);

        return this;
    }

    public Validator<TValue> ValidateEqual(TValue target, string? errorMessage = null)
    {
        var message = string.Format(errorMessage ?? "{0} 必須等於 {1}", _fieldName, target);

        if (_isFutherValidate)
            _baseValidateService.ValidateEqual(_value, target, message);

        return this;
    }

    public Validator<TValue> ValidateNotEquals(TValue target, string? errorMessage = null)
    {
        var message = string.Format(errorMessage ?? "{0} 不可等於 {1}", _fieldName, target);

        if (_isFutherValidate)
            _baseValidateService.ValidateNotEquals(_value, target, message);

        return this;
    }

    public Validator<TValue> ValidateEquals(TValue target, string? errorMessage = null)
    {
        var message = string.Format(errorMessage ?? "{0} 必須等於 {1}", _fieldName, target);

        if (_isFutherValidate)
            _baseValidateService.ValidateEquals(_value, target, message);

        return this;
    }

    public Validator<TValue> ValidateIn(TValue[] values, string? errorMessage = null)
    {
        if (_isFutherValidate)
            _baseValidateService.ValidateContains(_value, values, errorMessage);

        return this;
    }

    #endregion
}

public class ArrayValidator<TItem>
{
    private readonly TItem[]?            _arrayValue;
    private readonly string              _fieldName;
    private readonly BaseValidateService _baseValidateService;

    private bool                  _isFutherValidate = true;
    private BaseValidateService[] _itemValidatros   = Array.Empty<BaseValidateService>();

    public ArrayValidator(TItem[]?            arrayValue,
                          string              fieldName,
                          BaseValidateService baseValidateService)
    {
        _arrayValue          = arrayValue;
        _fieldName           = fieldName;
        _baseValidateService = baseValidateService;
    }

    public ArrayValidator<TItem> Required(string? errorMessage = null)
    {
        var message = string.Format(errorMessage ?? "請填寫 {0}", _fieldName);

        if (_arrayValue is null
         || _arrayValue.Length == 0)
        {
            _baseValidateService.AddError(message);
            _isFutherValidate = false;
        }
        else
        {
            _itemValidatros = _arrayValue.Select(x => new BaseValidateService()).ToArray();
        }

        return this;
    }

    public ArrayValidator<TItem> Item(Action<BaseValidateService, TItem, int> itemValidate)
    {
        if (_isFutherValidate)
        {
            for (var i = 0; i < _arrayValue!.Length; i++)
            {
                itemValidate.Invoke(_itemValidatros[i], _arrayValue[i], i);
            }
        }

        return this;
    }

    public void End()
    {
        var allErrors = new List<string>();
        for (var i = 0; i < _itemValidatros.Length; i++)
        {
            var itemValidator = _itemValidatros[i];
            if (itemValidator.Errors.Any())
            {
                allErrors.Add($"第 {i + 1} 筆");
                allErrors.AddRange(itemValidator.Errors);
            }
        }

        if (allErrors.Any())
        {
            _baseValidateService.AddError(_fieldName);
            _baseValidateService.AddError(allErrors);
        }
    }
}