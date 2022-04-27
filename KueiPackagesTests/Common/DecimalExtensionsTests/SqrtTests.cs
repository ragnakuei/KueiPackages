namespace KueiPackagesTests.Common.DecimalExtensionsTests
{
    public class SqrtTests
    {
        [Test]
        public void 開根號_4_To_2()
        {
            var actual = 4m.Sqrt();

            var expected = 2m;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void 開根號_小於0_OverflowException()
        {
            Assert.Catch<OverflowException>(() => (-1m).Sqrt());
        }
    }
}
