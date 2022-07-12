using KueiPackages.Types;

namespace KueiPackagesTests.Common.ArrayOfTTests;

[Category("ArrayOfT")]
public class ForTests
{
    [Test]
    public void ForEach()
    {
        Array<int> source = Enumerable.Range(1, 3).ToArrayOf();

        Array<int> actual = new Array<int>(3);

        for (int i = 0; i < source.Length; i++)
        {
            actual[i] = source[i];
        }

        var expected = new[] { 1, 2, 3 };

        expected.Should().BeEquivalentTo(actual);
    }

    [Test]
    public void For()
    {
        IEnumerable<int> GetForEach(Array<int> source)
        {
            foreach (var item in source)
            {
                yield return item;
            }
        }

        Array<int> source = Enumerable.Range(2, 3).ToArrayOf();

        var actual = GetForEach(source).ToArrayOf();

        var expected = new[] { 2, 3, 4 };

        expected.Should().BeEquivalentTo(actual);
    }
}
