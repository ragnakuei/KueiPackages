using KueiPackages.Helpers;

namespace KueiPackagesTests.Common.StringHelpersTests;

public class ToDateTimeTests
{
    [Test]
    [TestCase("0001/01/01 00:00:00", "0001/01/01 00:00:00")]
    public void 有效格式(string dateTime, DateTime expected)
    {
        var result = StringHelpers.ToDateTime(dateTime);

        Assert.AreEqual(expected, result);
    }

    [Test]
    [TestCase(null,                  "0001/01/01 00:00:00", "0001/01/01 00:00:00")]
    [TestCase("",                    "0001/01/01 00:00:00", "0001/01/01 00:00:00")]
    [TestCase("0000/00/00",          "0001/01/01 00:00:00", "0001/01/01 00:00:00")]
    [TestCase("9999/99/99 99:99:99", "0001/01/01 00:00:00", "0001/01/01 00:00:00")]
    public void 無效格式_給定_Default值(string s, DateTime defaultDateTime, DateTime expected)
    {
        var result = StringHelpers.ToDateTime(s, defaultDateTime);

        Assert.AreEqual(expected, result);
    }
}
