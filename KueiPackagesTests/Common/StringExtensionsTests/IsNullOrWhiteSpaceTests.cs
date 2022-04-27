namespace KueiPackagesTests.Common.StringExtensionsTests
{
    public class IsNullOrWhiteSpaceTests
    {
        [Test]
        public void 符合Guid()
        {
            var actual = "a".IsNullOrWhiteSpace();

            var expected = false;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void 不符合Guid()
        {
            var actual = ((string)null).IsNullOrWhiteSpace();

            var expected = true;

            Assert.AreEqual(expected, actual);
        }
    }
}
