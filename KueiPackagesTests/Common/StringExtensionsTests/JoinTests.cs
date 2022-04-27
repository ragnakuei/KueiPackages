namespace KueiPackagesTests.Common.StringExtensionsTests
{
    public class JoinTests
    {
        [Test]
        public void A_Join_B()
        {
            var actual = new []{ "A", "B" }.Join("、");

            var expected = "A、B";

            Assert.AreEqual(expected, actual);
        }
    }
}
