namespace KueiPackagesTests.Common.StringExtensionsTests
{
    public class ToNullableDecimalTests
    {
        [Test]
        public void One()
        {
            var actual = "1".ToNullableDecimal();

            var expected = 1m;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Null()
        {
            var actual = "a".ToNullableDecimal();

            decimal? expected = null;

            Assert.AreEqual(expected, actual);
        }
    }
}
