namespace KueiPackagesTests.Common.StringExtensionsTests
{
    public class TrimEndTests
    {
        [Test]
        public void Case01()
        {
            var actual = "abcd".TrimEnd("cd");

            var expected = "ab";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Case02()
        {
            var actual = "abcd".TrimEnd("cd", StringComparison.Ordinal);

            var expected = "ab";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Case03()
        {
            var actual = "abcd".TrimEnd("Cd", StringComparison.Ordinal);

            var expected = "abcd";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Case04()
        {
            var actual = "abcd".TrimEnd("bc", StringComparison.Ordinal);

            var expected = "abcd";

            Assert.AreEqual(expected, actual);
        }
    }
}
