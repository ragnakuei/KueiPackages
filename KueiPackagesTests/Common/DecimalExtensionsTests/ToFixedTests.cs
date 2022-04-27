namespace KueiPackagesTests.Common.DecimalExtensionsTests
{
    public class ToFixedTests
    {
        [Test]
        public void 四捨五入_小數點下1位_1point14_To_1point1()
        {
            var actual = 1.14m.ToFix(1);

            var expected = 1.1m;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void 四捨五入_小數點下1位_1point15_To_1point2()
        {
            var actual = 1.15m.ToFix(1);

            var expected = 1.2m;

            Assert.AreEqual(expected, actual);
        }
    }
}
