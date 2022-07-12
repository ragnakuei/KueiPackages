using System.Security;

namespace KueiPackagesTests.Common.SecureStringExtensionsTests
{
    public class ContentEqualTests
    {
        [Test]
        public void ContentEqual_True()
        {
            var targetString = "1234";
            var target1      = targetString.ToSecureString();
            var target2      = targetString.ToSecureString();

            var actual = target1.ContentEqual(target2);

            Assert.True(actual);
        }

        [Test]
        public void ContentEqual_False()
        {
            var target1 = "1234".ToSecureString();
            var target2 = "123".ToSecureString();

            var actual = target1.ContentEqual(target2);

            Assert.False(actual);
        }

        [Test]
        public void ContentEqual_False_FromEmpty()
        {
            var target1 = new SecureString();
            var target2 = "123".ToSecureString();

            var actual = target1.ContentEqual(target2);

            Assert.False(actual);
        }

        [Test]
        public void ContentEqual_False_CompareEmpty()
        {
            var target1 = "1234".ToSecureString();
            var target2 = new SecureString();

            var actual = target1.ContentEqual(target2);

            Assert.False(actual);
        }
    }
}
