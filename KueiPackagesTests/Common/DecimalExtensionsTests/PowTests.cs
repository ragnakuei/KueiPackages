namespace KueiPackagesTests.Common.DecimalExtensionsTests
{
    public class PowTests
    {
        [Test]
        public void 次方_2_pow3_8()
        {
            var actual = 2m.Pow(3);

            var expected = 8m;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void 次方_1point1_pow2_1point21()
        {
            var actual = 1.1m.Pow(2);

            var expected = 1.21m;

            Assert.AreEqual(expected, actual);
        }
    }
}
