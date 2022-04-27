namespace KueiPackagesTests.Common.DecimalExtensionsTests
{
    public class StDevTests
    {
        [Test]
        public void StDev_1_2()
        {
            var actual = new []{ 1m,2m }.StDev();

            var expected = 0.7071067811865475244008443621m;

            Assert.AreEqual(expected, actual);
        }
    }
}
