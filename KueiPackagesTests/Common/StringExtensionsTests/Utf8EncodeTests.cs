namespace KueiPackagesTests.Common.StringExtensionsTests
{
    public class Utf8EncodeTests
    {
        [Test]
        public void Case01()
        {
            var actual = "ABC".Utf8Encode();

            var expected = @"\u0041\u0042\u0043";

            Assert.AreEqual(expected, actual);
        }
    }
}
