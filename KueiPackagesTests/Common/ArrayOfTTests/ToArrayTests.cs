using KueiPackages.Types;

namespace KueiPackagesTests.Common.ArrayOfTTests;

[Category("ArrayOfT")]
public class ToArrayTests
{
    [Test]
    public void FromIEnumerable()
    {
        Array<int> actual = Enumerable.Range(1, 3)
                                      .ToArrayOf();

        var expected = new[] { 1, 2, 3 };

        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void FromParamsArray()
    {
        Array<int> actual = Enumerable.Range(1, 3)
                                      .ToArrayOf();

        var expected = new Array<int>(1, 2, 3);

        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void FromArray()
    {
        Array<int> actual = Enumerable.Range(1, 3)
                                      .ToArray()
                                      .ToArrayOf();

        var expected = new[] { 1, 2, 3 };

        actual.Should().BeEquivalentTo(expected);
    }
}
