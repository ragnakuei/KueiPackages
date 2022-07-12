using KueiPackages.Types;

namespace KueiPackagesTests.Common.ArrayOfTTests;

[Category("ArrayOfT")]
public class EmptyTests
{
    [Test]
    public void Case01()
    {
        var actual = Array<int>.Empty();

        var expected = Array.Empty<int>();

        expected.Should().Equal(actual);
    }
}
