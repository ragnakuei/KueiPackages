using System.ComponentModel;
using System.Linq.Expressions;
using KueiPackages.Models;

namespace KueiPackages.Services;

public class ValidateService<T>
{
    public  List<string>   Errors { get; } = new();
    private PropertyInfo[] _properties = Array.Empty<PropertyInfo>();

    protected void Assign(T dto)
    {
        if (dto == null)
        {
            return;
        }

        _properties = dto.GetType()
                         .GetProperties()
                         .Select((p) =>
                                 {
                                     var displayName = p.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName;

                                     return p;
                                 })
                         .ToArray();
    }

    protected void ValidateNotNull<TObj>(TObj dto, string errorMessage)
    {
        if (dto == null)
        {
            throw new ValidateFormFailedException(errorMessage);
        }
    }

    protected Validator<T, TValue> Validate<TValue>(TValue value, string fieldName)
    {
        return new Validator<T, TValue>(this, value, fieldName);
    }

    protected Validator<T, TValue> Validate<TValue>(Expression<Func<T, TValue>> selector)
    {
        var memberExpression = selector.Body as MemberExpression;
        if (memberExpression == null)
        {
            throw new ArgumentException("value 必須是 MemberExpression");
        }

        var value = 
    }

    protected void Validate()
    {
        if (Errors.Any())
        {
            throw new ValidateFormFailedException(string.Join(Environment.NewLine, Errors));
        }
    }

    private void AddError(string errorMessage)
    {
        Errors.Add(errorMessage);
    }

    public class Validator<TSource, TValue>
    {
        private readonly ValidateService<TSource> _validateService;
        private readonly TValue                   _value;
        private readonly string                   _fieldName;

        public Validator(ValidateService<TSource> validateService, TValue value, string fieldName)
        {
            _validateService = validateService;
            _value           = value;
            _fieldName       = fieldName;
        }

        /// <summary>
        /// 驗證是否已必填
        /// </summary>
        /// <param name="formatErrorMessage">
        /// 會用 fieldName 取代 {0}
        /// </param>
        public Validator<TSource, TValue> Required(string? formatErrorMessage = null)
        {
            if (string.IsNullOrWhiteSpace(formatErrorMessage))
            {
                formatErrorMessage = "請填寫 {0}";
            }

            if (_value is string str
             && string.IsNullOrWhiteSpace(str))
            {
                _validateService.AddError(string.Format(formatErrorMessage, _fieldName));
            }
            else if (_value == null)
            {
                _validateService.AddError(string.Format(formatErrorMessage, _fieldName));
            }

            return this;
        }

        public Validator<TSource, TValue> MaxLength(int length, string? formatErrorMessage = null)
        {
            if (string.IsNullOrWhiteSpace(formatErrorMessage))
            {
                formatErrorMessage = "{0} 長度不可超過 {1} 個字";
            }

            if (_value is string str
             && str.Length > length)
            {
                _validateService.AddError(string.Format(formatErrorMessage, _fieldName, length));
            }

            return this;
        }
    }
}