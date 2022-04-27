namespace KueiPackagesTests.Common.StringExtensionsTests
{
    public class ToNullableIntTests
    {
        [Test]
        public void One()
        {
            var actual = "1".ToNullableInt();

            var expected = 1;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Null()
        {
            var actual = "a".ToNullableInt();

            var expected = (int?)null;

            Assert.AreEqual(expected, actual);
        }
    }
}
