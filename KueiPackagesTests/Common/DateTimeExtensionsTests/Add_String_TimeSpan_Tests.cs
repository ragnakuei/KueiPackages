namespace KueiPackagesTests.Common.DateTimeExtensionsTests;

public class Add_String_TimeSpan_Tests
{
    private static DateTime _zeroDateTime = new DateTime(1, 1, 1, 0, 0, 0);

    static object[] ValidFormats =
    {
        new object[] { _zeroDateTime, "00:01", _zeroDateTime.AddMinutes(1) },
        new object[] { _zeroDateTime, "01:00", _zeroDateTime.AddHours(1) },
        new object[] { _zeroDateTime, "01:02:03:00", _zeroDateTime.AddDays(1).AddHours(2).AddMinutes(3) },
    };

    [Test]
    [TestCaseSource(nameof(ValidFormats))]
    public void 加上有效格式(DateTime dt, string timeSpan, DateTime expected)
    {
        var actual = DateTimeExtensions.Add(dt, timeSpan);

        Assert.AreEqual(expected, actual);
    }

    static object[] CustomFormats =
    {
        new object[] { _zeroDateTime, "01", "mm", _zeroDateTime.AddMinutes(1) },
        new object[] { _zeroDateTime, "01", "hh", _zeroDateTime.AddHours(1) },
        new object[] { _zeroDateTime, "01", "dd", _zeroDateTime.AddDays(1) },
    };

    [Test]
    [TestCaseSource(nameof(CustomFormats))]
    public void 指定格式(DateTime dt,
                     string   timeSpan,
                     string   format,
                     DateTime expected)
    {
        var actual = dt.Add(timeSpan, format);

        Assert.AreEqual(expected, actual);
    }
}
