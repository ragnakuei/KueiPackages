namespace KueiPackagesTests.Common.DecimalExtensionsTests
{
    public class ToFixAndFillTailZeroTests
    {
        [Test]
        public void 傳入0點1()
        {
            var actual = 0.1m.ToFixAndFillTailZero(4).ToString();

            var expected = "0.1000";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void 傳入0點0()
        {
            var actual = 0.0m.ToFixAndFillTailZero(2).ToString();

            var expected = "0.00";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void 傳入0()
        {
            var actual = 0m.ToFixAndFillTailZero(10).ToString();

            var expected = "0.0000000000";

            Assert.AreEqual(expected, actual);
        }
    }
}
