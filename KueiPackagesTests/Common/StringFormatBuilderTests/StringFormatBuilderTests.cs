namespace KueiPackagesTests.Common.StringFormatBuilderTests;

public class StringFormatBuilderTests
{
    [Test]
    public void Case01()
    {
        var actual = new StringFormatBuilder()
                    .SetFileName("a.jpg")
                    .AppendDateTime(date: new DateTime(2020, 8, 3), format: "yyyy-MM-dd")
                    .SetDelimiter("_")
                    .ToString();

        var expected = "a_2020-08-03.jpg";

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void Case02()
    {
        var actual = new StringFormatBuilder()
                    .SetFileName("bb.jpg")
                    .AppendDateTime(date: new DateTime(2020, 8, 3))
                    .ToString();

        var expected = "bb2020/8/3 00:00:00.jpg";

        Assert.AreEqual(expected, actual);
    }
}
