namespace KueiPackagesTests.Common.StringExtensionsTests
{
    public class ToNullableTimeSpanTests
    {
        [Test]
        public void 符合_格式()
        {
            var actual = "01:02:03".ToNullableTimeSpan("hh\\:mm\\:ss");

            var expected = new TimeSpan(01, 02, 03);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void 不符合_格式()
        {
            var actual = "99:99:99".ToNullableTimeSpan("hh\\:mm\\:ss");

            DateTime? expected = null;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void 符合()
        {
            var actual = "01:02:03".ToNullableTimeSpan();

            var expected = new TimeSpan(01, 02, 03);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void 不符合()
        {
            var actual = "99:99:99".ToNullableTimeSpan();

            DateTime? expected = null;

            Assert.AreEqual(expected, actual);
        }
    }
}
