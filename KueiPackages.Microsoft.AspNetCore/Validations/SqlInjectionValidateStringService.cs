namespace KueiPackages.Microsoft.AspNetCore.Validations;

public class SqlInjectionValidateStringService : ISqlInjectionValidateStringService
{
    private readonly string[] _blockStrings = new[]
                                              {
                                                  ";",
                                                  "'",
                                                  "--",
                                                  "/*",
                                                  "*/",
                                                  "xp_",
                                              };

    public void Validate(string? str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return;
        }

        if (_blockStrings.Any(blockString => str.Contains(blockString)))
        {
            throw new SqlInjectionValidateFailedException(string.Empty);
        }
    }
}
