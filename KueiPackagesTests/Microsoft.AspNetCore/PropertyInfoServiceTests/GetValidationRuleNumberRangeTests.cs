using PropertyInfoService = KueiPackages.Microsoft.AspNetCore.Services.PropertyInfoService;

namespace KueiPackagesTests.Microsoft.AspNetCore.PropertyInfoServiceTests;

public class GetValidationRuleNumberRangeTests : GetValidationRuleTests
{
    private class TestNumberMinMaxLengthDto
    {
        [global::System.ComponentModel.DataAnnotations.Range(10, 20)]
        public string Name { get; set; }
    }

    [Test]
    public void TestNumberMinMaxLength()
    {
        var target = new PropertyInfoService();

        var actual = GetValidationRulesResult(target.GetProperties(typeof(TestNumberMinMaxLengthDto)));

        var expected = new Dictionary<string, Dictionary<string, object>>
                       {
                           ["Name"] = new()
                                      {
                                          ["min"] = 10,
                                          ["max"] = 20,
                                      }
                       };

        actual.Should().BeEquivalentTo(expected);
    }
}
