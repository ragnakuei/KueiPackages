namespace KueiPackagesTests.Microsoft.AspNetCore.PropertyInfoServiceTests;

public class GetValidationRuleTests
{
    [SetUp]
    public void Setup()
    {
    }

    protected Dictionary<string, Dictionary<string, object>> GetValidationRulesResult(Dictionary<string, PropertyInfoDto> properties)
    {
        var result = properties.ToDictionary(kv => kv.Key, kv => kv.Value.ValidationRules);
        return result;
    }
}
