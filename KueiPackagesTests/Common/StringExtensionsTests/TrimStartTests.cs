namespace KueiPackagesTests.Common.StringExtensionsTests
{
    public class TrimStartTests
    {
        [Test]
        public void Case01()
        {
            var actual = "abcd".TrimStart("ab");

            var expected = "cd";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Case02()
        {
            var actual = "abcd".TrimStart("ab", StringComparison.Ordinal);

            var expected = "cd";

            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void Case03()
        {
            var actual = "abcd".TrimStart("aB", StringComparison.Ordinal);

            var expected = "abcd";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Case04()
        {
            var actual = "abcd".TrimStart("bc");

            var expected = "abcd";

            Assert.AreEqual(expected, actual);
        }
    }
}
