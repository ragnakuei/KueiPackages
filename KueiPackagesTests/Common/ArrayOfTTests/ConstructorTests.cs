using System.Drawing;
using KueiPackages.Types;

namespace KueiPackagesTests.Common.ArrayOfTTests;

[Category("ArrayOfT")]
public class ConstructorTests
{
    [Test]
    public void Initializer()
    {
        var actual = new Array<int>()
                     {
                         1, 2, 3
                     };

        var expected = new[] { 1, 2, 3 };

        expected.Should().Equal(actual);
    }

    [Test]
    public void Items()
    {
        var actual = new Array<int>(items: 1);

        var expected = new[] { 1 };
        
        expected.Should().Equal(actual);
    }

    [Test]
    public void Size()
    {
        var actual = new Array<int>(size: 3);
        for (int i = 0; i < actual.Length; i++)
        {
            actual[i] = i + 1;
        }

        var expected = new[] { 1, 2, 3 };

        expected.Should().Equal(actual);
    }
}
