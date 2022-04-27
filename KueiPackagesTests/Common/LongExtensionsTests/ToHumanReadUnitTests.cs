namespace KueiPackagesTests.Common.LongExtensionsTests;

public class ToHumanReadUnitTests
{
    [Test]
    public void 無_1234()
    {
        var actual = 1234L.ToHumanReadUnit(1);

        var expected = "1234";

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void 萬_12345()
    {
        var actual = 12345L.ToHumanReadUnit(2);

        var expected = "1.23萬";

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void 億_12345()
    {
        var actual = 1234567890L.ToHumanReadUnit(3);

        var expected = "12.346億";

        Assert.AreEqual(expected, actual);
    }
}
