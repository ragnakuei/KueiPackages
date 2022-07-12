namespace KueiPackagesTests.Common.ArrayOfTTests;

[Category("ArrayOfT")]
public class CopyTests
{
    [Test]
    public void CopyArrayOf()
    {
        var source = Enumerable.Range(1, 3)
                               .ToArrayOf();

        var actual = Enumerable.Range(4, 3)
                               .ToArrayOf();

        source.Copy(actual, 1);

        var expected = new[] { 1, 5, 6 };

        expected.Should().Equal(actual);
    }
    
    [Test]
    public void CopyArray()
    {
        var source = Enumerable.Range(1, 3)
                               .ToArrayOf();

        var actual = Enumerable.Range(4, 3)
                               .ToArray();

        source.Copy(actual, 1);

        var expected = new[] { 1, 5, 6 };

        expected.Should().Equal(actual);
    }
}
