namespace KueiPackagesTests.Common.StructExtensionsTests
{
    public class NullableEnumGetValueNullTests
    {
        private enum Test
        {
            A = 0,
            B = 1,
        }

        [Test]
        public void 回傳Null()
        {
            Test? t = null;

            var actual = t.GetValueOrNull();

            var expected = (Test?)null;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void 回傳符合項目()
        {
            Test? t = Test.A;

            var actual = t.GetValueOrNull();

            var expected = Test.A;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void 回傳不符合項目()
        {
            Test? t = (Test?)2;

            var actual = t.GetValueOrNull();

            var expected = (Test?)2;

            Assert.AreEqual(expected, actual);
        }
    }
}
