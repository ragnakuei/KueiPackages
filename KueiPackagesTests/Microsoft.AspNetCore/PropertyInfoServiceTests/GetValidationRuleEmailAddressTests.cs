using System.ComponentModel.DataAnnotations;

namespace KueiPackagesTests.Microsoft.AspNetCore.PropertyInfoServiceTests;

public class GetValidationRuleEmailAddressTests : GetValidationRuleTests
{
    private class TestDto
    {
        [EmailAddress]
        public string Name { get; set; }
    }

    [Test]
    public void TestNumberMinMaxLength()
    {
        var target = new PropertyInfoService();

        var actual = GetValidationRulesResult(target.GetProperties(typeof(TestDto)));

        var expected = new Dictionary<string, Dictionary<string, object>>
                       {
                           ["Name"] = new()
                                      {
                                          ["email"] = true,
                                      }
                       };

        actual.Should().BeEquivalentTo(expected);
    }
}
