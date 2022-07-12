namespace KueiPackagesTests.Common.ArrayOfTTests;

[Category("ArrayOfT")]
public class IndexerTests
{
    [Test]
    public void ChangeIndex1Value()
    {
        var actual = Enumerable.Range(1, 3)
                               .ToArrayOf();
        actual[1] = 0;

        var expected = new[] { 1, 0, 3 };

        expected.Should().BeEquivalentTo(actual);
    }
}
