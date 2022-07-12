namespace KueiPackagesTests.Common.ArrayOfTTests;

[Category("ArrayOfT")]
public class LengthTests
{
    [Test]
    public void Length()
    {
        var actual = Enumerable.Range(1, 3)
                               .ToArrayOf();
        var expected = 3;

        Assert.AreEqual(expected, actual.Length);
    }
    
    [Test]
    public void Count()
    {
        var actual = Enumerable.Range(1, 3)
                               .ToArrayOf();
        var expected = 3;

        Assert.AreEqual(expected, actual.Count);
    }
}
