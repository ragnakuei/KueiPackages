namespace KueiPackagesTests.Common.StringExtensionsTests
{
    public class ToDecimalTests
    {
        [Test]
        public void One()
        {
            var actual = "1".ToDecimal();

            var expected = 1m;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Null()
        {
            var actual = "a".ToDecimal();

            var expected = 0m;

            Assert.AreEqual(expected, actual);
        }
    }
}
