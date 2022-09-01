using System.Linq.Expressions;

namespace KueiPackages.Dapper;

public interface IValidateService<T>
{
    IValidateService<T> Validate(T? dto);

    IValidateService<T> ValidateString(Expression<Func<T, string>> expression,
                                       bool                        required             = false,
                                       string?                     requiredMessage      = null,
                                       long?                       maxLength            = null,
                                       string?                     maxLengthMessage     = null,
                                       long?                       minLength            = null,
                                       string?                     minLengthMessage     = null,
                                       bool                        validateSqlInjection = false);

    IValidateService<T> ValidateLong(Expression<Func<T, long?>> expression,
                                     bool                       required        = false,
                                     string?                    requiredMessage = null,
                                     long?                      max             = null,
                                     string?                    maxMessage      = null,
                                     long?                      min             = null,
                                     string?                    minMessage      = null);

    IValidateService<T> ValidateInt(Expression<Func<T, int?>> expression,
                                    bool                      required        = false,
                                    string?                   requiredMessage = null,
                                    int?                      max             = null,
                                    string?                   maxMessage      = null,
                                    int?                      min             = null,
                                    string?                   minMessage      = null);

    IValidateService<T> ValidateDecimal(Expression<Func<T, decimal?>> expression,
                                        bool                          required        = false,
                                        string?                       requiredMessage = null,
                                        decimal?                      max             = null,
                                        string?                       maxMessage      = null,
                                        decimal?                      min             = null,
                                        string?                       minMessage      = null);

    void ThrowExceptionWhenInvalid();
}
