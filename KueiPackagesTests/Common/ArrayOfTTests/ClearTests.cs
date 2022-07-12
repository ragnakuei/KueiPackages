namespace KueiPackagesTests.Common.ArrayOfTTests;

[Category("ArrayOfT")]
public class ClearTests
{
    [Test]
    public void ClearAll()
    {
        var actual = Enumerable.Range(1, 3)
                               .ToArrayOf();
        actual.Clear();

        var expected = Enumerable.Repeat(0, 3).ToArray();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void ClearIndex2()
    {
        var actual = Enumerable.Range(1, 3)
                               .ToArrayOf();
        actual.Clear(2);

        var expected = new[] { 1, 2, 0 };

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void ClearIndex1Length3()
    {
        var actual = Enumerable.Range(1, 3)
                               .ToArrayOf();
        actual.Clear(1, 3);

        var expected = new[] { 1, 0, 0 };

        Assert.AreEqual(expected, actual);
    }
}
