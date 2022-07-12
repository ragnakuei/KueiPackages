using KueiPackages.Types;

namespace KueiPackagesTests.Common.ArrayOfTTests;

[Category("ArrayOfT")]
public class ResizeTests
{
    [Test]
    public void ResizeOfInt()
    {
        Array<int> actual = Enumerable.Range(1, 3)
                                      .ToArrayOf();
        actual.Resize(4);

        var expected = new[] { 1, 2, 3, 0 };

        expected.Should().BeEquivalentTo(actual);
    }

    [Test]
    public void ResizeOfNullableInt()
    {
        var actual = Enumerable.Range(1, 3)
                               .Cast<int?>()
                               .ToArrayOf();
        actual.Resize(4);

        var expected = new int?[] { 1, 2, 3, null };

        expected.Should().BeEquivalentTo(actual);
    }
}
