using System.ComponentModel.DataAnnotations;

namespace KueiPackagesTests.Microsoft.AspNetCore.PropertyInfoServiceTests;

public class GetValidationRuleStringLengthTests : GetValidationRuleTests
{
    private class TestStringMaxLengthDto
    {
        [StringLength(10)]
        public string Name { get; set; }
    }

    [Test]
    public void TestStringMaxLength()
    {
        var target = new PropertyInfoService();

        var actual = GetValidationRulesResult(target.GetProperties(typeof(TestStringMaxLengthDto)));

        var expected = new Dictionary<string, Dictionary<string, object>>
                       {
                           ["Name"] = new()
                                      {
                                          ["maxlength"] = 10,
                                      }
                       };

        actual.Should().BeEquivalentTo(expected);
    }

    private class TestStringMaxMinLengthDto
    {
        [StringLength(10, MinimumLength = 3)]
        public string Name { get; set; }
    }

    [Test]
    public void TestStringMaxMinLength()
    {
        var target = new PropertyInfoService();

        var actual = GetValidationRulesResult(target.GetProperties(typeof(TestStringMaxMinLengthDto)));

        var expected = new Dictionary<string, Dictionary<string, object>>
                       {
                           ["Name"] = new()
                                      {
                                          ["maxlength"] = 10,
                                          ["minlength"] = 3,
                                      }
                       };

        actual.Should().BeEquivalentTo(expected);
    }
}
