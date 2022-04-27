namespace KueiPackagesTests.Common.StringExtensionsTests
{
    public class ToNullableGuidTests
    {
        [Test]
        public void 符合Guid()
        {
            var actual = "5bca7510-f34c-4411-a832-619ab3c633b8".ToNullableGuid();

            var expected = new Guid("5bca7510-f34c-4411-a832-619ab3c633b8");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void 不符合Guid()
        {
            var actual = "0".ToNullableGuid();

            Guid? expected = null;

            Assert.AreEqual(expected, actual);
        }
    }
}
