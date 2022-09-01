using System.ComponentModel.DataAnnotations;
using PropertyInfoService = KueiPackages.Microsoft.AspNetCore.Services.PropertyInfoService;

namespace KueiPackagesTests.Microsoft.AspNetCore.PropertyInfoServiceTests;

public class GetValidationRuleRequiredTests : GetValidationRuleTests
{
    private class TestRequiredDto
    {
        [Required]
        public string Name { get; set; }
    }

    [Test]
    public void TestRequiredDto01()
    {
        var target = new PropertyInfoService();

        var actual = GetValidationRulesResult(target.GetProperties(typeof(TestRequiredDto)));

        var expected = new Dictionary<string, Dictionary<string, object>>
                       {
                           ["Name"] = new()
                                      {
                                          ["required"] = true,
                                      }
                       };

        actual.Should().BeEquivalentTo(expected);
    }
}
