namespace KueiPackagesTests.Common.StringExtensionsTests
{
    public class ToNullableLongTests
    {
        [Test]
        public void One()
        {
            var actual = "1".ToNullableLong();

            var expected = 1;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Null()
        {
            var actual = "a".ToNullableLong();

            var expected = (long?)null;

            Assert.AreEqual(expected, actual);
        }
    }
}
