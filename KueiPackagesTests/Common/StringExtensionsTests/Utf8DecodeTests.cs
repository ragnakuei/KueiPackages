namespace KueiPackagesTests.Common.StringExtensionsTests
{
    public class Utf8DecodeTests
    {
        [Test]
        public void Case01()
        {
            var actual = @"\u0041\u0042\u0043".Utf8Decode();

            var expected = "ABC";

            Assert.AreEqual(expected, actual);
        }
    }
}
