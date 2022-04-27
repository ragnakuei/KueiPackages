namespace KueiPackagesTests.Common.RandomStringTests;

public class GeneratTests
{
    private readonly int _length = 100;
    private readonly int _count  = 100;

    [Test]
    public void LowerCaseOnly()
    {
        var target = new RandomString(useLowerCase: true);

        for (int i = 0; i < _count; i++)
        {
            var actual      = target.Generate(_length);
            var lowerActual = actual.ToLower();
            Assert.AreEqual(lowerActual, actual);
        }
    }

    [Test]
    public void UpperCaseOnly()
    {
        var target = new RandomString(userUpperCase: true);

        for (int i = 0; i < _count; i++)
        {
            var actual      = target.Generate(_length);
            var lowerActual = actual.ToUpper();
            Assert.AreEqual(lowerActual, actual);
        }
    }

    [Test]
    public void AddiditionalChars()
    {
        var target = new RandomString(additionalChars: new char[] { 'a' });

        var actual         = target.Generate(_length);
        var distinctActual = actual.Distinct().ToArray();

        var expected = new char[] { 'a' };

        expected.Should().BeEquivalentTo(distinctActual);
    }
}
